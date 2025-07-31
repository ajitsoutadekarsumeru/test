using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersByNameMapperConfiguration : FlexMapperProfile
    {
        bool DisplayDomainId = Convert.ToBoolean(AppConfigManager.AppSettings["DisplayDomainId"] ?? "false");
        /// <summary>
        ///
        /// </summary>
        public GetUsersByNameMapperConfiguration() : base()
        {
            CreateMap<CompanyUser, GetUserByNameDto>()
               .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => DisplayDomainId == true ? o.DomainId : o.CustomId))
                .ForMember(o => o.SecondName, opt => opt.MapFrom(o => o.LastName));
        }
    }
}