using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnAccountMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentOnAccountMapperConfiguration() : base()
        {
            CreateMap<TreatmentOnAccountDto, TreatmentOnAccount>();
            CreateMap<TreatmentOnAccount, TreatmentOnAccountDto>();
            CreateMap<TreatmentOnAccountDtoWithId, TreatmentOnAccount>();
            CreateMap<TreatmentOnAccount, TreatmentOnAccountDtoWithId>();
        }
    }
}