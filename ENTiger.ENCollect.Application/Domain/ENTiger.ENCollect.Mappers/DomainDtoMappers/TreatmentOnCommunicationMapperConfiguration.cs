using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnCommunicationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentOnCommunicationMapperConfiguration() : base()
        {
            CreateMap<TreatmentOnCommunicationDto, TreatmentOnCommunication>();
            CreateMap<TreatmentOnCommunication, TreatmentOnCommunicationDto>();
            CreateMap<TreatmentOnCommunicationDtoWithId, TreatmentOnCommunication>();
            CreateMap<TreatmentOnCommunication, TreatmentOnCommunicationDtoWithId>();
        }
    }
}