using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUserPersonasMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUserPersonasMapperConfiguration() : base()
        {
            CreateMap<UserPersonaMaster, GetUserPersonasDto>()
                          .ForMember(cm => cm.Name, Dm => Dm.MapFrom(dModel => dModel.Name));
        }
    }
}