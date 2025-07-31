namespace ENTiger.ENCollect
{
    public class ViewTreatmentByRuleDto
    {
        public string Id { get; set; }
        public string TreatmentId { get; set; }
        public string Rule { get; set; }

        public List<ViewTreatmentRules> TreatmentRules { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }

        public bool? IsDeleted { get; set; }
    }

    public class ViewTreatmentRules
    {
        public string RuleId { get; set; }
        public string RuleName { get; set; }

        public string RuleOperator { get; set; }
    }
}