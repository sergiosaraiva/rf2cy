using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rf2cy
{
    internal class Settings
    {
        private IConfiguration Config { get; }
        public string HandlerDefault { get; }
        public string ReSpaceClean { get; }
        public string ReNotTestCaseLine { get; }
        public string ReTestCaseLine { get; }
        public string TestCaseNamePlaceholder { get; }
        public string TestStepPlaceholder { get; }
        public string InputExtension { get; }
        public string OutputExtension { get; }
        public List<TestStepConfig> TestStepConfigs { get; }

        public class TestStepConfig
        {
            public string Re { get; }
            public string Handler { get; }

            public TestStepConfig()
            {
                Re = string.Empty;
                Handler = string.Empty;
            }

            public TestStepConfig(string re, string handler)
            {
                Re = re;
                Handler = handler;
            }
        }

        public Settings()
        {
            Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
            HandlerDefault = (string)Config.GetValue(typeof(string), "handlerDefault");
            ReSpaceClean = (string)Config.GetValue(typeof(string), "reSpaceClean");
            ReNotTestCaseLine = (string)Config.GetValue(typeof(string), "reNotTestCaseLine");
            ReTestCaseLine = (string)Config.GetValue(typeof(string), "reTestCaseLine");
            TestCaseNamePlaceholder = (string)Config.GetValue(typeof(string), "testCaseNamePlaceholder");
            TestStepPlaceholder = (string)Config.GetValue(typeof(string), "testStepPlaceholder");
            InputExtension = (string)Config.GetValue(typeof(string), "inputExtension");
            OutputExtension = (string)Config.GetValue(typeof(string), "outputExtension");
            TestStepConfigs = new List<TestStepConfig>();

            foreach (var test in Config.GetSection("testStepConfig").GetChildren())
            {
                string re = string.Empty;
                string handler = string.Empty;
                foreach (var sa in test.GetChildren().ToDictionary(x => x.Key, x => x.Value))
                {
                    if (sa.Key == "re")
                    {
                        re = sa.Value;
                    }
                    if (sa.Key == "handler")
                    {
                        handler = sa.Value;
                    }
                }
                TestStepConfigs.Add(new TestStepConfig(re,handler));
            }
        }
    }
}
