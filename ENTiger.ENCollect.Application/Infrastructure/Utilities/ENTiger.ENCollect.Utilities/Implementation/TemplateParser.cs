using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class TemplateParser : ITemplateParser
    {
        private static readonly Regex PlaceholderRegex = new(@"\{([\w\.]+)\}", RegexOptions.Compiled);

        public List<string> ExtractPlaceholders(string template)
        {
            return PlaceholderRegex.Matches(template)
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToList();
        }
    }

}
