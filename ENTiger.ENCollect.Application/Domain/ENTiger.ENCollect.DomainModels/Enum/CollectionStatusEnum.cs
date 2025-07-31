using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class CollectionStatusEnum : FlexEnum    
    {
        public CollectionStatusEnum()
        { }

        public CollectionStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly CollectionStatusEnum withAgent = new CollectionStatusEnum("With Agent", "With Agent");
        public static readonly CollectionStatusEnum withAgency_Or_Branch = new CollectionStatusEnum("With Agency_Or_Branch", "With Agency_Or_Branch");
        public static readonly CollectionStatusEnum withBank = new CollectionStatusEnum("With Bank", "With Bank");
        public static readonly CollectionStatusEnum cancelled = new CollectionStatusEnum("Cancelled", "Cancelled");
        public static readonly CollectionStatusEnum failed = new CollectionStatusEnum("Failed", "failed");
    }
}
