namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchDepartmentDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DepartmentTypeId { get; set; }
        public string DepartmentTypeName { get; set; }
        public string DepartmentAcronym { get; set; }
        public string DepartmentCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}