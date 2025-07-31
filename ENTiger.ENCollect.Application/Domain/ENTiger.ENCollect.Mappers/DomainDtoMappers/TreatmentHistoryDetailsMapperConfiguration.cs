using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentHistoryDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentHistoryDetailsMapperConfiguration() : base()
        {
            CreateMap<TreatmentHistoryDetailsDto, TreatmentHistoryDetails>();
            CreateMap<TreatmentHistoryDetails, TreatmentHistoryDetailsDto>();
            CreateMap<TreatmentHistoryDetailsDtoWithId, TreatmentHistoryDetails>();
            CreateMap<TreatmentHistoryDetails, TreatmentHistoryDetailsDtoWithId>();
        }
    }
}