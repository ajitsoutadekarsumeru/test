namespace ENTiger.ENCollect
{
    public class ViewTreatmentOnAccountDto
    {
        public string Id { get; set; }
        public string TreatmentId { get; set; }
        public string Percentage { get; set; }
        public string AllocationId { get; set; }
        public string AllocationName { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}