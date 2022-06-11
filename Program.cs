using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace rf2cy
{
    class Program
    {
        private class TestStepConfig
        {
            private readonly string _re;
            private readonly string _handler;
            public TestStepConfig(string re, string handler)
            {
                _re = re;
                _handler = handler;
            }

            public string GetRe()
            {
                return _re;
            }

            public string GetHandler()
            {
                return _handler;
            }
        }
        private static readonly TestStepConfig[] reTestStep = new TestStepConfig[] { 
            new TestStepConfig(@"^(\bTitle\sShould\sBe\s\b)", "rf2cy.Handler.TitleShouldBe" ),
            new TestStepConfig(@"^(\bGo\sTo\b)", "rf2cy.Handler.GoTo" )
        };
        private const string handlerDefault = "rf2cy.Handler.SingleLineComment";
        private const string reSpaceClean = @"(\s{2,})|(\t{1,})";
        private const string reNotTestCaseLine = @"^((\s)|(\t)|(\b\*\*\*\b)|(\b\.\.\.\b)|(\b${\b)|(\bDocumentation\b)|(\bLibrary\b))";
        private const string testCaseNamePlaceholder = @"//##TEST_CASE_NAME##";
        private const string testStepPlaceholder = @"//##TEST_STEPS##";
        private const string inputExtension = ".robot";
        private const string outputExtension = ".spec.js";

        private static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length < 4)
                {
                    Console.WriteLine("Please use: {0} <source file> <test suite template> <test case template> <test step template>", AppDomain.CurrentDomain.FriendlyName);
                    return;
                }

                string[] lines = File.ReadAllLines(args[0]);
                string outputFile = string.Concat(args[0].Replace(inputExtension,string.Empty), outputExtension);
                string testSuiteTemplate = File.ReadAllText(args[1]);
                string testCaseTemplate = File.ReadAllText(args[2]);
                string testStepTemplate = File.ReadAllText(args[3]);

                string testCaseSection = string.Empty;
                string testStepSection = string.Empty;

                string testSuiteDocument = string.Empty;
                string testCaseDocument = string.Empty;
                string testStepDocument = string.Empty;

                string testCase = string.Empty;
                string testCaseCurrent = string.Empty;
                string testLine = string.Empty;

                int i = 0;
                int len = lines.Length;

                foreach (string line in lines)
                {
                    testCase = ExtractTestCase(line);
                    if (testCase != string.Empty)
                    {                        
                        if (testCaseCurrent != string.Empty)
                        {
                            testCaseSection = testCaseSection.Replace(testStepPlaceholder, testStepSection);
                        }

                        testSuiteDocument += testCaseSection;
                        testCaseCurrent = testCase;
                        testCaseSection = testCaseTemplate.Replace(testCaseNamePlaceholder, testCaseCurrent);
                        testStepSection = string.Empty;
                        Console.WriteLine("Case:\t[{0}]", testCaseCurrent);
                        i++;
                        continue;
                    }

                    testLine = ExtractTestStep(line);
                    if (testLine != string.Empty && testCaseCurrent != string.Empty)
                    {
                        testStepSection += "\t\t" + GenerateTestStep(testLine) + "\n";
                        Console.WriteLine("\tLine:\t[{0}]", testLine);
                    }

                    if (++i == len)
                    {
                        if (testCaseCurrent != string.Empty)
                        {
                            testCaseSection = testCaseSection.Replace(testStepPlaceholder, testStepSection);
                            testSuiteDocument += testCaseSection;
                        }
                    }
                }
                Console.WriteLine("\n{0}", testSuiteDocument);
                File.WriteAllText(outputFile, testSuiteDocument);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static string ExtractTestCase(string line)
        {
            if (line == null)
            {
                return string.Empty;
            };
            Regex regex = new(reNotTestCaseLine, RegexOptions.None);
            if (regex.IsMatch(line))
            {
                return string.Empty;
            }
            regex = new(reSpaceClean, RegexOptions.None);
            return regex.Replace(line, " ").Trim();
        }

        private static string ExtractTestStep(string line)
        {
            if (line == null || string.IsNullOrEmpty(line) || ExtractTestCase(line) != string.Empty)
            {
                return string.Empty;
            }
            Regex regex = new(reSpaceClean, RegexOptions.None);
            return regex.Replace(line, " ").Trim();
        }

        private static string ExecuteHandler(string handler, string line, string re)
        {
            string[] h = handler!.Split('.');
            if (h.Length > 0)
            {
                string methodName = h.Last();
                string className = string.Join('.', h.SkipLast(1));
                Console.WriteLine("\tHandler: Class: [{0}]; Method: [{1}]", className, methodName);
                if (!(string.IsNullOrEmpty(className) || string.IsNullOrEmpty(methodName)))
                {
                    return Type.GetType(className)!.GetMethod(methodName)!.Invoke(null, new object[] { line, re })!.ToString() ?? string.Empty;
                }
            }
            return string.Empty;
        }

        private static string GenerateTestStep(string line)
        {
            try
            {
                foreach(TestStepConfig re in reTestStep)
                {
                    Regex regex = new(re.GetRe(), RegexOptions.None);
                    if (regex.IsMatch(line))
                    {
                        string newLine = ExecuteHandler(re.GetHandler(), line, re.GetRe());
                        if(!string.IsNullOrEmpty(newLine))
                        {
                            return newLine;
                        }

                    }
                }
            }
            catch(Exception)
            {
                return ExecuteHandler(handlerDefault, line, string.Empty);
            }
            return ExecuteHandler(handlerDefault, line, string.Empty);
        }
    }
}