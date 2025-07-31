namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class EnabledPermissionSchemeDto : DtoBridge
    {
        public string Id { get; set; }

        public string PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
    }
}
