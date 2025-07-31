namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationsByDepartmentDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DesignationTypeId { get; set; }
        public string DesignationTypeName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}