using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchTreatmentsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchTreatmentsMapperConfiguration() : base()
        {
            CreateMap<Treatment, SearchTreatmentOutputDto>()
                 .ForMember(vm => vm.CreatedOn, dm => dm.MapFrom(dModel => dModel.CreatedDate));
        }
    }
}