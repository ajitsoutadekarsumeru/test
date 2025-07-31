using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnPOSMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentOnPOSMapperConfiguration() : base()
        {
            CreateMap<TreatmentOnPOSDto, TreatmentOnPOS>();
            CreateMap<TreatmentOnPOS, TreatmentOnPOSDto>();
            CreateMap<TreatmentOnPOSDtoWithId, TreatmentOnPOS>();
            CreateMap<TreatmentOnPOS, TreatmentOnPOSDtoWithId>();
        }
    }
}