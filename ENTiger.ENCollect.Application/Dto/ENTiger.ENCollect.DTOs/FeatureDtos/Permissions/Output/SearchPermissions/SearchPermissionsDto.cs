namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchPermissionsDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Section { get; set; }
        public string? PermissionSchemeName { get; set; }
        public string? DesignationNames { get; set; }
    }
}
