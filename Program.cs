using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace rf2cy
{

    static class Program
    {

        private static readonly Settings s = new();

        private static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length < 4)
                {
                    Console.WriteLine("Please use: {0} <source file> <test suite template> <test case template> <test step template>", AppDomain.CurrentDomain.FriendlyName);
                    return;
                }

                string testSuiteTemplate = File.ReadAllText(args[1]);
                string testCaseTemplate = File.ReadAllText(args[2]);
                string testStepTemplate = File.ReadAllText(args[3]);


                string[] files = Directory.GetFiles(".", args[0]);

                foreach (string file in files)
                {
                    string outputFile = string.Concat(file.Replace(s.InputExtension, string.Empty), s.OutputExtension);
                    Console.WriteLine("\n***** Input file: [{0}], output file: [{1}] *****\n", file, outputFile);

                    string[] lines = File.ReadAllLines(file);
                    string testCaseSection = string.Empty;
                    string testStepSection = string.Empty;
                    string testSuiteDocument = string.Empty;
                    string testCaseDocument = string.Empty;
                    string testStepDocument = string.Empty;
                    string testCase = string.Empty;
                    string testCaseCurrent = string.Empty;
                    string testLine = string.Empty;

                    int i = 0, len = lines.Length;

                    foreach (string line in lines)
                    {
                        testCase = ExtractTestCase(line);
                        if (testCase != string.Empty)
                        {
                            if (testCaseCurrent != string.Empty)
                            {
                                testCaseSection = testCaseSection.Replace(s.TestStepPlaceholder, testStepSection);
                            }

                            testSuiteDocument += testCaseSection;
                            testCaseCurrent = testCase;
                            testCaseSection = testCaseTemplate.Replace(s.TestCaseNamePlaceholder, testCaseCurrent);
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
                                testCaseSection = testCaseSection.Replace(s.TestStepPlaceholder, testStepSection);
                                testSuiteDocument += testCaseSection;
                            }
                        }
                    }
                    File.WriteAllText(outputFile, testSuiteDocument);
                }
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
            Regex regex = new(s.ReNotTestCaseLine, RegexOptions.None);
            if (regex.IsMatch(line))
            {
                return string.Empty;
            }
            regex = new(s.ReSpaceClean, RegexOptions.None);
            return regex.Replace(line, " ").Trim();
        }

        private static string ExtractTestStep(string line)
        {
            if (line == null || string.IsNullOrEmpty(line) || ExtractTestCase(line) != string.Empty)
            {
                return string.Empty;
            }
            Regex regex = new(s.ReSpaceClean, RegexOptions.None);
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
                if (s.TestStepConfigs == null)
                {
                    return ExecuteHandler(s.HandlerDefault, line, string.Empty);
                }
                foreach (Settings.TestStepConfig re in s.TestStepConfigs)
                {
                    Regex regex = new(re.Re, RegexOptions.None);
                    if (regex.IsMatch(line))
                    {
                        string newLine = ExecuteHandler(re.Handler, line, re.Re);
                        if (!string.IsNullOrEmpty(newLine))
                        {
                            return newLine;
                        }

                    }
                }
            }
            catch (Exception)
            {
                return ExecuteHandler(s.HandlerDefault, line, string.Empty);
            }
            return ExecuteHandler(s.HandlerDefault, line, string.Empty);
        }
    }
}