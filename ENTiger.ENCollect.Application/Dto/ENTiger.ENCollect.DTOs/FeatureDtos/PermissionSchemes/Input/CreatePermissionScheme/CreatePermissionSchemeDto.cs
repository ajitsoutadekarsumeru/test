namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class CreatePermissionSchemeDto : DtoBridge
    {
        public string Name { get; set; }
        public string Remarks { get; set; } // needs to discuss with Product Team for Remarks mandatory
        public List<string> EnabledPermissionIds { get; set; }
    }

}
