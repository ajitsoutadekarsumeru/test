using ENTiger.ENCollect.DesignationsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetDesignationDetailsBySchemeIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetDesignationDetailsBySchemeIdMapperConfiguration() : base()
        {
            CreateMap<PermissionSchemes, GetDesignationDetailsBySchemeIdDto>();

            CreateMap<Designation, GetDesignationByPermissionSchemeIdDto>()
                 .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
