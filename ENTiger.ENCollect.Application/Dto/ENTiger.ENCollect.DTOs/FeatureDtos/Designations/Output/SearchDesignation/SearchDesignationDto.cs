namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchDesignationDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DesignationTypeId { get; set; }
        public string DesignationTypeName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string IsDeleted { get; set; }
        public string DesignationAcronym { get; set; }
        public string Level { get; set; }
        public string ReportsTo { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public string AccountabilityTypeId { get; set; }
    }
}