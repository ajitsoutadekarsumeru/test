using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUserMapperConfiguration() : base()
        {
            CreateMap<AgencyUserDto, AgencyUser>();
            CreateMap<AgencyUser, AgencyUserDto>();
            CreateMap<AgencyUserDtoWithId, AgencyUser>();
            CreateMap<AgencyUser, AgencyUserDtoWithId>();
            CreateMap<AgencyUser, AgentEmailDto>()
                 .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                 .ForMember(dest => dest.AuthorizationCardExpiry, opt => opt.MapFrom(src => src.AuthorizationCardExpiryDate))
                 .ForMember(dest => dest.AgencyName, Dm => Dm.MapFrom(opt => opt.Agency.FirstName + " " + opt.Agency.LastName))
                 .ForMember(dest => dest.AgentCode, Dm => Dm.MapFrom(opt => opt.CustomId))
                 .ForMember(dest => dest.AgentEmail, Dm => Dm.MapFrom(opt => opt.PrimaryEMail))
                 .ForMember(dest => dest.PhoneNumber, Dm => Dm.MapFrom(opt => opt.PrimaryMobileNumber))
                 .ForMember(dest => dest.Status, Dm => Dm.MapFrom(opt => opt.AgencyUserWorkflowState.Name))
                 .ForMember(dest => dest.Roles, Dm => Dm.MapFrom(o => o.Designation.Where(a => !a.IsDeleted)));
            CreateMap<AgencyUser, AgentMobileDto>()
                 .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                 .ForMember(dest => dest.AuthorizationCardExpiry, opt => opt.MapFrom(src => src.AuthorizationCardExpiryDate))
                 .ForMember(dest => dest.AgencyName, Dm => Dm.MapFrom(opt => opt.Agency.FirstName + " " + opt.Agency.LastName))
                 .ForMember(dest => dest.AgentCode, Dm => Dm.MapFrom(opt => opt.CustomId))
                 .ForMember(dest => dest.AgentEmail, Dm => Dm.MapFrom(opt => opt.PrimaryEMail))
                 .ForMember(dest => dest.PhoneNumber, Dm => Dm.MapFrom(opt => opt.PrimaryMobileNumber))
                 .ForMember(dest => dest.Status, Dm => Dm.MapFrom(opt => opt.AgencyUserWorkflowState.Name))
                 .ForMember(dest => dest.Roles, Dm => Dm.MapFrom(o => o.Designation.Where(a => !a.IsDeleted)));
            CreateMap<AgencyUser, AgentDto>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.AuthorizationCardExpiry, opt => opt.MapFrom(src => src.AuthorizationCardExpiryDate))
                .ForMember(dest => dest.AgencyName, Dm => Dm.MapFrom(opt => opt.Agency.FirstName + " " + opt.Agency.LastName))
                .ForMember(dest => dest.AgentCode, Dm => Dm.MapFrom(opt => opt.CustomId))
                .ForMember(dest => dest.AgentEmail, Dm => Dm.MapFrom(opt => opt.PrimaryEMail))
                .ForMember(dest => dest.PhoneNumber, Dm => Dm.MapFrom(opt => opt.PrimaryMobileNumber))
                .ForMember(dest => dest.Status, Dm => Dm.MapFrom(opt => opt.AgencyUserWorkflowState.Name))
                .ForMember(dest => dest.Roles, Dm => Dm.MapFrom(o => o.Designation.Where(a => !a.IsDeleted)));
        }
    }
}