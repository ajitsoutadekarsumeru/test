using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentByRuleMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public TreatmentByRuleMapperConfiguration() : base()
        {
            CreateMap<TreatmentByRuleDto, TreatmentByRule>();
            CreateMap<TreatmentByRule, TreatmentByRuleDto>();
            CreateMap<TreatmentByRuleDtoWithId, TreatmentByRule>();
            CreateMap<TreatmentByRule, TreatmentByRuleDtoWithId>();
        }
    }
}