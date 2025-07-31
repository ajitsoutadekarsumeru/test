namespace ENTiger.ENCollect
{
    public class SettlementDocumentEnum : FlexEnum
    {
        public SettlementDocumentEnum()
        { }

        public SettlementDocumentEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly SettlementDocumentEnum RequestLetter = new SettlementDocumentEnum("RequestLetter", "Request Letter");
        public static readonly SettlementDocumentEnum DeathCertificate = new SettlementDocumentEnum("DeathCertificate", "Death Certificate");
        public static readonly SettlementDocumentEnum OtherDocument = new SettlementDocumentEnum("OtherDocument", "Other Document");
    }
}
