namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DesignationTypeId { get; set; }
        public string DesignationTypeName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string IsDeleted { get; set; }
        public string DesignationAcronym { get; set; }
        public int Level { get; set; }
        public string ReportsTo { get; set; }
    }
}