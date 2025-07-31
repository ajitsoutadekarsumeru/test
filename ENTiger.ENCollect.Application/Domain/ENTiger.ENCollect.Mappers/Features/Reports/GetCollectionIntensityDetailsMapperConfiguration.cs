using Sumeru.Flex;

namespace ENTiger.ENCollect.ReportsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetCollectionIntensityDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetCollectionIntensityDetailsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetCollectionIntensityDetailsDto>();

        }
    }
}
