using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RenewAgencyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RenewAgencyMapperConfiguration() : base()
        {
            CreateMap<RenewAgencyDto, Agency>();
        }
    }
}