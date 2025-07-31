namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepartmentByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DepartmentTypeId { get; set; }
        public string DepartmentTypeName { get; set; }
        public string DepartmentAcronym { get; set; }
        public string DepartmentCode { get; set; }
    }
}