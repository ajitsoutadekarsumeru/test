namespace ENTiger.ENCollect.PermissionsModule
{
    public class GetPermissionSchemeChangeLogDto : DtoBridge
    {
        public string Id { get; set; }
        public string? AddedPermissions { get; set; }
        public string? RemovedPermissions { get; set; }
        public string? ChangeType { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UserId { get; set; }
    }
}
