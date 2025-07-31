using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentDesignationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentDesignationMapperConfiguration() : base()
        {
            CreateMap<TreatmentDesignationDto, TreatmentDesignation>();
            CreateMap<TreatmentDesignation, TreatmentDesignationDto>();
            CreateMap<TreatmentDesignationDtoWithId, TreatmentDesignation>();
            CreateMap<TreatmentDesignation, TreatmentDesignationDtoWithId>();
        }
    }
}