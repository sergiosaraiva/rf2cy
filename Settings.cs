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
        public string TestStepParameter { get; }
        public List<TestStepConfig> TestStepConfigs { get; }

        public class TestStepConfig
        {
            public string ReLine { get; }
            public string ReParam { get; }
            public string Handler { get; }

            public TestStepConfig()
            {
                ReLine = string.Empty;
                ReParam = string.Empty;
                Handler = string.Empty;
            }

            public TestStepConfig(string reLine, string reParam, string handler)
            {
                ReLine = reLine;
                Handler = handler;
                ReParam = reParam;
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
            TestStepParameter = (string)Config.GetValue(typeof(string), "testStepParameter");
            TestStepConfigs = new List<TestStepConfig>();

            foreach (var test in Config.GetSection("testStepConfig").GetChildren())
            {
                string reLine = string.Empty;
                string reParam = string.Empty;
                string handler = string.Empty;
                foreach (var sa in test.GetChildren().ToDictionary(x => x.Key, x => x.Value))
                {
                    if (sa.Key == "reLine")
                    {
                        reLine = sa.Value;
                    }
                    if (sa.Key == "reParam")
                    {
                        reParam = sa.Value;
                    }
                    if (sa.Key == "handler")
                    {
                        handler = sa.Value;
                    }
                }
                TestStepConfigs.Add(new TestStepConfig(reLine, reParam, handler));
            }
        }
    }
}
