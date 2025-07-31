using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAzureMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAzureMapperConfiguration() : base()
        {
            CreateMap<GetAzureDto, FeatureMaster>();
        }
    }
}