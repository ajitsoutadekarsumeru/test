using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionModeEnum : FlexEnum
    {
        public CollectionModeEnum()
        { }

        public CollectionModeEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly CollectionModeEnum Online = new CollectionModeEnum("online payment", "online payment");
        public static readonly CollectionModeEnum Cash = new CollectionModeEnum("cash", "cash");
        public static readonly CollectionModeEnum Cheque = new CollectionModeEnum("cheque", "cheque");

        public static readonly CollectionModeEnum Wallet = new CollectionModeEnum("wallet", "wallet");
        public static readonly CollectionModeEnum Card = new CollectionModeEnum("card payment", "card payment");
        public static readonly CollectionModeEnum EncerdibleOnline = new CollectionModeEnum("encerdible online", "encerdible online");
    }
}