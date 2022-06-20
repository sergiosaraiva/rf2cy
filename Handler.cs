using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace rf2cy
{
    public static class Handler
    {
        private static string GetLastParameter(string line, string reLine)
        {
            Regex regex = new(reLine, RegexOptions.None);
            return regex!.Replace(line, " ").Trim();
        }

        private static string[] GetParameters(string line, string reParam)
        {
            Regex regex = new(reParam, RegexOptions.None);
            List<string> parameters = new();
            foreach(Match m in regex.Matches(line))
            {
                parameters.Add(m.Value.Trim());
            }
            return parameters.ToArray();
        }
        public static string SingleLineComment(string line, string reLine, string reParam)
        {
            return string.Concat("// ", line);
        }
        public static string TitleShouldBe(string line, string reLine, string reParam)
        {
            return string.Concat(SingleLineComment(line, reLine, reParam),
                "\n\t\tit('TitleShouldBe', () => {\n",
                "\t\t\tcy.title().should('eq', '", GetLastParameter(line, reParam), "')\n",
                "\t\t})\n");
        }

        public static string GoTo(string line, string reLine, string reParam)
        {
            return string.Concat(SingleLineComment(line, reLine, reParam), "\n",
                "\t\tcy.visit('", GetLastParameter(line, reLine), "')\n");
        }

        public static string VerifyBreadcrumbs(string line, string reLine, string reParam)
        {
            string[] p = GetParameters(line, reParam);
            return string.Concat(SingleLineComment(line, reLine, reParam),
            "\n\t\tit('VerifyBreadcrumbs', () => {\n",
            "\t\t\tcy.get('.breadcrumbTitle').should('include.text', ", p.Length > 0 ? p[0]: string.Empty, ")\n",
            "\t\t})\n");
        }
    }
}
