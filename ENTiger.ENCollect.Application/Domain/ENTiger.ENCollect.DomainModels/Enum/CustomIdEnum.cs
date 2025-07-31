using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CustomIdEnum : FlexEnum
    {
        public CustomIdEnum()
        { }

        public CustomIdEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly CustomIdEnum EReceipt = new CustomIdEnum("ereceipt", "ereceipt");
        public static readonly CustomIdEnum Agency = new CustomIdEnum("collectionagency", "collectionagency");
        public static readonly CustomIdEnum AgencyUser = new CustomIdEnum("agencyuser", "agencyuser");
        public static readonly CustomIdEnum CompanyUser = new CustomIdEnum("companyuser", "companyuser");
        public static readonly CustomIdEnum CollectionBatch = new CustomIdEnum("collectionbatch", "collectionbatch");
        public static readonly CustomIdEnum PayinSlip = new CustomIdEnum("payinslip", "payinslip");
        public static readonly CustomIdEnum IDCardNumber = new CustomIdEnum("idcardnumber", "idcardnumber");
        public static readonly CustomIdEnum Settlement = new CustomIdEnum("Settlement", "Settlement");
    }
}