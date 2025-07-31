namespace ENTiger.ENCollect
{
    public class PermissionSchemeChangeLogDto : DtoBridge
    {
        public string? PermissionSchemeId { get; set; }
        public string? AddedPermissions { get; set; }
        public string? RemovedPermissions { get; set; }
        public string? ChangeType { get; set; }
        public string? Remarks { get; set; }
    }
}
