using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class TemplateProcessor : ITemplateProcessor
    {
        private static readonly Regex PlaceholderRegex = new(@"\{([\w\.]+)\}", RegexOptions.Compiled);

        public string FillTemplate(string template, Dictionary<string, object?> data)
        {
            return PlaceholderRegex.Replace(template, match =>
            {
                var key = match.Groups[1].Value;
                return data.TryGetValue(key, out var value) ? value?.ToString() ?? "" : match.Value;
            });
        }
    }


}
