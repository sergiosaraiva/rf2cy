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
        private static string GetParameters(string line, string re)
        {
            Regex regex = new(re, RegexOptions.None);
            return regex.Replace(line, " ").Trim();
        }
        public static string SingleLineComment(string line, string re)
        {
            return string.Concat("// ", line);
        }
        public static string TitleShouldBe(string line, string re)
        {
            return string.Concat(SingleLineComment(line, re),
                "\n\t\tit('TitleShouldBe', () => {\n",
                "\t\t\tcy.title().should('eq', '", GetParameters(line, re), "')\n",
                "\t\t})\n");
        }

        public static string GoTo(string line, string re)
        {
            return string.Concat(SingleLineComment(line, re), "\n",
                "\t\tcy.visit('", GetParameters(line, re), "')\n");
        }
    }
}
