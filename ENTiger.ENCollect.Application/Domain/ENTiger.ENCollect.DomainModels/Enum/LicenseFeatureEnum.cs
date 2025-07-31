
namespace ENTiger.ENCollect.DomainModels.Enum
{
    public class LicenseFeatureEnum : FlexEnum
    {
        public LicenseFeatureEnum()
        { }

        public LicenseFeatureEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly LicenseFeatureEnum CompanyUserLimitOnApproval = new LicenseFeatureEnum("CompanyUserLimitOnApproval", "CompanyUserLimitOnApproval");
        public static readonly LicenseFeatureEnum AgencyUserLimitOnApproval = new LicenseFeatureEnum("AgencyUserLimitOnApproval", "AgencyUserLimitOnApproval");

        public static readonly LicenseFeatureEnum CompanyUserLimitOnCreate = new LicenseFeatureEnum("CompanyUserLimitOnCreate", "CompanyUserLimitOnCreate");
        public static readonly LicenseFeatureEnum AgencyUserLimitOnCreate = new LicenseFeatureEnum("AgencyUserLimitOnCreate", "AgencyUserLimitOnCreate");

        public static readonly LicenseFeatureEnum UserCollectionLimit = new LicenseFeatureEnum("UserCollectionLimit", "UserCollectionLimit");
        public static readonly LicenseFeatureEnum UserFeedbackLimit = new LicenseFeatureEnum("UserFeedbackLimit", "UserFeedbackLimit");

    }
}
