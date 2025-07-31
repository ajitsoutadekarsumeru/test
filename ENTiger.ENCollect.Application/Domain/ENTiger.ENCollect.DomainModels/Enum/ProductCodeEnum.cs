using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ProductCodeEnum : FlexEnum
    {
        public ProductCodeEnum()
        { }

        public ProductCodeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly ProductCodeEnum CreditCard = new ProductCodeEnum("CreditCard", "CreditCard");
        public static readonly ProductCodeEnum All = new ProductCodeEnum("all", "all");
    }
}