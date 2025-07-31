using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveAgencyWithDeferralMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApproveAgencyWithDeferralMapperConfiguration() : base()
        {
            CreateMap<ApproveAgencyWithDeferralDto, Agency>();
        }
    }
}