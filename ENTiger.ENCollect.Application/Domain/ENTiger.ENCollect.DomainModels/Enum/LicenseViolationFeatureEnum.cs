
namespace ENTiger.ENCollect.DomainModels.Enum
{
    public class LicenseViolationFeatureEnum : FlexEnum
    {
        public LicenseViolationFeatureEnum()
        { }

        public LicenseViolationFeatureEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly LicenseViolationFeatureEnum FreeCollections = new LicenseViolationFeatureEnum("FreeCollections", "Free collections");
        public static readonly LicenseViolationFeatureEnum FreeTrails = new LicenseViolationFeatureEnum("FreeTrails", "Free trails");
    }
}
