using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.DomainModels.Enum
{
    public class ReportsEnum : FlexEnum
    {
        private ReportsEnum() { }
        private ReportsEnum(string value, string displayName) : base(value, displayName) { }

        public static readonly ReportsEnum NoFilterPresent = new ReportsEnum("NoFilterPresent", "NoFilterPresent");


    }
}
