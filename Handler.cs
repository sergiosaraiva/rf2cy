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
        private static string GetParsedParameter(string line, int index, string paramSeparator)
        {
            string[] p = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length == 0 || p.Length < index)
            {
                return string.Empty;
            }

            if(string.IsNullOrEmpty(paramSeparator))
            {
                return p[index];
            }

            string[] r = p[index].Split(paramSeparator);
            if (r.Length == 0)
            {
                return p[index];
            }

            return r[1];
        }
        private static string GetLastParameter(string line, string reLine)
        {
            Regex regex = new(reLine, RegexOptions.None);
            return regex!.Replace(line, " ").Trim();
        }

        private static string[] GetParameters(string line, string reParam)
        {
            Regex regex = new(reParam, RegexOptions.None);
            List<string> p = new();
            foreach(Match m in regex.Matches(line))
            {
                p.Add(m.Value.Trim());
            }
            return p.ToArray();
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

        public static string Click(string line, string reLine, string reParam)
        {
            return string.Concat(SingleLineComment(line, reLine, reParam), "\n",
                "\t\tcy.get('", GetParsedParameter(line, 1, "="), "').click()\n");
        }

        public static string WaitForElementsState(string line, string reLine, string reParam)
        {
            return string.Concat(SingleLineComment(line, reLine, reParam), "\n",
                "\t\tcy.get('", GetParsedParameter(line, 4, "="), "').click()\n");
        }

        public static string FillText(string line, string reLine, string reParam)
        {
            return string.Concat(SingleLineComment(line, reLine, reParam), "\n",
                "\t\tcy.get('", GetParsedParameter(line, 1, "="), "').click()\n");
        }

        public static string GetText(string line, string reLine, string reParam)
        {
            return string.Concat(SingleLineComment(line, reLine, reParam), "\n",
                "\t\tcy.get('", GetParsedParameter(line, 1, "="), "').click()\n");
        }

        public static string VerifyBreadcrumbs(string line, string reLine, string reParam)
        {
            string[] p = GetParameters(line, reParam);
            return string.Concat(SingleLineComment(line, reLine, reParam),
            "\n\t\tit('VerifyBreadcrumbs', () => {\n",
            "\t\t\tcy.get('.breadcrumbTitle').should('include.text', ", p.Length > 0 ? p[0]: string.Empty, ")\n",
            "\t\t})\n");
        }

        public static string CompareGetTitle(string line, string reLine, string reParam)
        {
            string[] p = GetParameters(line, reParam);
            return string.Concat(SingleLineComment(line, reLine, reParam),
            "\n\t\tit('verify the title of the screen', () => {\n",
            "\t\t\tcy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))\n",
            "\t\t\tcy.visit(", p.Length > 0 ? p[0] : string.Empty, ")\n",
            "\t\t\tcy.title().should('eq',", p.Length > 0 ? p[0] : string.Empty, ")\n",
            "\t\t})\n");
        }
    }
}
