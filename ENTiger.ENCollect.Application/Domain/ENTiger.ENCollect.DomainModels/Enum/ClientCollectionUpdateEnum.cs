using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ClientCollectionUpdateTypeEnum : FlexEnum
    {
        public ClientCollectionUpdateTypeEnum()
        { }

        public ClientCollectionUpdateTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly ClientCollectionUpdateTypeEnum receptPosting = new ClientCollectionUpdateTypeEnum("receiptposting", "receiptposting");

    }
}
