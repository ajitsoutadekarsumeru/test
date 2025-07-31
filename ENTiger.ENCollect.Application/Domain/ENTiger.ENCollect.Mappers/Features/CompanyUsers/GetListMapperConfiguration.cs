using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetListMapperConfiguration : FlexMapperProfile
    {
        bool DisplayDomainId = Convert.ToBoolean(AppConfigManager.AppSettings["DisplayDomainId"] ?? "false");
        /// <summary>
        ///
        /// </summary>
        public GetListMapperConfiguration() : base()
        {
            CreateMap<CompanyUser, GetListsDto>()
                .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => DisplayDomainId == true ? o.DomainId : o.CustomId));
        }
    }
}