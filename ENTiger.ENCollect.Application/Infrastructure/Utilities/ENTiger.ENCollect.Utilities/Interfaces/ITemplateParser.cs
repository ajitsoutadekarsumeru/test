using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ITemplateParser
    {
        List<string> ExtractPlaceholders(string template);
    }
}
