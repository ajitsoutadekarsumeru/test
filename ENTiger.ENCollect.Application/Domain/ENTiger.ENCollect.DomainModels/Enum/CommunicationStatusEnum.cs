using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CommunicationStatusEnum : FlexEnum
    {
        public CommunicationStatusEnum()
        { }

        public CommunicationStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly CommunicationStatusEnum AwaitingDispatch = new CommunicationStatusEnum("AwaitingDispatch", "AwaitingDispatch");

        public static readonly CommunicationStatusEnum Dispatched = new CommunicationStatusEnum("Dispatched", "Dispatched");
        public static readonly CommunicationStatusEnum Delivered = new CommunicationStatusEnum("Delivered", "Delivered");
        public static readonly CommunicationStatusEnum Read = new CommunicationStatusEnum("Read", "Read");
        public static readonly CommunicationStatusEnum Failed = new CommunicationStatusEnum("Failed", "Failed");


    }
}