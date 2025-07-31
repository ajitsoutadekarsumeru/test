namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchACMDto : DtoBridge
    {
        public string Id { get; set; }

        public string? Menu { get; set; }

        public string? SubMenu { get; set; }

        public string? Scope { get; set; }

        public bool HasAccess { get; set; }
    }
}