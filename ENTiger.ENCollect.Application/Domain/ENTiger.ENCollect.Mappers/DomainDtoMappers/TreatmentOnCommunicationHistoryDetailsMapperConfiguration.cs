using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnCommunicationHistoryDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentOnCommunicationHistoryDetailsMapperConfiguration() : base()
        {
            CreateMap<TreatmentOnCommunicationHistoryDetailsDto, TreatmentOnCommunicationHistoryDetails>();
            CreateMap<TreatmentOnCommunicationHistoryDetails, TreatmentOnCommunicationHistoryDetailsDto>();
            CreateMap<TreatmentOnCommunicationHistoryDetailsDtoWithId, TreatmentOnCommunicationHistoryDetails>();
            CreateMap<TreatmentOnCommunicationHistoryDetails, TreatmentOnCommunicationHistoryDetailsDtoWithId>();
        }
    }
}