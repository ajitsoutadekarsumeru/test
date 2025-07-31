using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetFeaturesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetFeaturesMapperConfiguration() : base()
        {
            CreateMap<FeatureMaster, GetFeaturesDto>()
            .ForMember(cm => cm.Name, Dm => Dm.MapFrom(dModel => dModel.Parameter));
        }
    }
}