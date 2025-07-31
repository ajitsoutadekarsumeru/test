using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class TreatmentByRuleDtoWithId : TreatmentByRuleDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}