using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentHistoryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentHistoryMapperConfiguration() : base()
        {
            CreateMap<TreatmentHistoryDto, TreatmentHistory>();
            CreateMap<TreatmentHistory, TreatmentHistoryDto>();
            CreateMap<TreatmentHistoryDtoWithId, TreatmentHistory>();
            CreateMap<TreatmentHistory, TreatmentHistoryDtoWithId>();
        }
    }
}