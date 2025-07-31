using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveCollectionAgencyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApproveCollectionAgencyMapperConfiguration() : base()
        {
            CreateMap<ApproveCollectionAgencyDto, Agency>();
        }
    }
}