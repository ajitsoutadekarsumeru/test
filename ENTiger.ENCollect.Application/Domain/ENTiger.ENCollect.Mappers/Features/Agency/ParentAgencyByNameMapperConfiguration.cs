using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ParentAgencyByNameMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ParentAgencyByNameMapperConfiguration() : base()
        {
            CreateMap<Agency, ParentAgencyByNameDto>()
                 .ForMember(vm => vm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                 .ForMember(vm => vm.AgencyName, Dm => Dm.MapFrom(dModel => dModel.FirstName));
        }
    }
}