using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchTreatmentsByNameMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchTreatmentsByNameMapperConfiguration() : base()
        {
            CreateMap<Treatment, SearchTreatmentsByNameDto>();
        }
    }
}