using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPermissionSchemesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetPermissionSchemesMapperConfiguration() : base()
        {
            CreateMap<PermissionSchemes, GetPermissionSchemesDto>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.DateTime));
            
        }
    }
}
