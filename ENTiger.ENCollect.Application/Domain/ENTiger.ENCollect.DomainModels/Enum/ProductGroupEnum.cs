using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ProductGroupEnum : FlexEnum
    {
        public ProductGroupEnum()
        { }

        public ProductGroupEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly ProductGroupEnum All = new ProductGroupEnum("all", "all");
        public static readonly ProductGroupEnum HFC = new ProductGroupEnum("HFC", "HFC");

        public static readonly ProductGroupEnum CreditCard = new ProductGroupEnum("creditcard", "creditcard");

        
    }
}