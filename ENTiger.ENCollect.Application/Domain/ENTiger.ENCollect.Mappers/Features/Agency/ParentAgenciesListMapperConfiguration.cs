using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ParentAgenciesListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ParentAgenciesListMapperConfiguration() : base()
        {
            CreateMap<Agency, ParentAgenciesListDto>()
                 .ForMember(vm => vm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                 .ForMember(vm => vm.AgencyName, Dm => Dm.MapFrom(dModel => dModel.FirstName));
        }
    }
}