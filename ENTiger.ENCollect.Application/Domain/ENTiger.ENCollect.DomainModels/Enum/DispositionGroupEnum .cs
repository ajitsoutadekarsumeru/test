using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents disposition group classifications used in account summaries.
    /// </summary>
    public class DispositionGroupEnum : FlexEnum
    {
        public DispositionGroupEnum() { }

        public DispositionGroupEnum(string value, string displayName) : base(value, displayName) { }

        public static readonly DispositionGroupEnum PTP = new DispositionGroupEnum("PTP", "Promise To Pay");
        public static readonly DispositionGroupEnum Contacted = new DispositionGroupEnum("Contact", "Contact");
        public static readonly DispositionGroupEnum NoContact = new DispositionGroupEnum("NC", "NC");
        public static readonly DispositionGroupEnum Completed = new DispositionGroupEnum("Completed", "Completed");
    }
}
