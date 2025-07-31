using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersByBaseBranchIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUsersByBaseBranchIdMapperConfiguration() : base()
        {
            CreateMap<CompanyUser, GetUsersByBaseBranchIdDto>()
                  .ForMember(o => o.StaffName, opt => opt.MapFrom(o => $"{o.FirstName} {o.LastName}"));
        }
    }
}