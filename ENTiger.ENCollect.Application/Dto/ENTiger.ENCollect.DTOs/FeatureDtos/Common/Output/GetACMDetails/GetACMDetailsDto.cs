namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetACMDetailsDto : DtoBridge
    {
        public string? MenuMasterName { get; set; }
        public string? SubMenuMasterName { get; set; }
        public string? ActionMasterName { get; set; }
        public bool HasAccess { get; set; }
        public string? AccountabilityTypeId { get; set; }
    }
}