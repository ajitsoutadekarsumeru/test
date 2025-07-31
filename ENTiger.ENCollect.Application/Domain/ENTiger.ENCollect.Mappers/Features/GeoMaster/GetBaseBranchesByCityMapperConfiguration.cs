using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetBaseBranchesByCityMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetBaseBranchesByCityMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, GetBaseBranchesByCityDto>()
                .ForMember(d => d.Id, s => s.MapFrom(s => s.BaseBranchId))
                .ForMember(d => d.Name, s => s.MapFrom(s => s.BaseBranch.FirstName))
                .ForMember(d => d.Code, s => s.MapFrom(s => s.BaseBranch.CustomId));
        }
    }
}