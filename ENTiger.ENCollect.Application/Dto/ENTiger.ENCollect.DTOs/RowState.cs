using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public enum RowState
    {
        Unchanged = 0,
        Added = 1,
        Modified = 2,
        Deleted = 3,
        Selected = 4
    }

    public class CommunicationTemplateTypeEnum : FlexEnum
    {
        public CommunicationTemplateTypeEnum()
        { }

        public CommunicationTemplateTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly CommunicationTemplateTypeEnum Email = new CommunicationTemplateTypeEnum("0", "Email");
        public static readonly CommunicationTemplateTypeEnum SMS = new CommunicationTemplateTypeEnum("1", "SMS");
        public static readonly CommunicationTemplateTypeEnum Letter = new CommunicationTemplateTypeEnum("2", "Letter");


        /// <summary>
        /// returns all the defined template types
        /// </summary>
        public static IEnumerable<CommunicationTemplateTypeEnum> List() =>
            GetAll<CommunicationTemplateTypeEnum>();

        public static IEnumerable<CommunicationTemplateTypeEnum> GetAll() => List();
    }
}