namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCodesByGroupIdDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? DispositionCode { get; set; }
        public string? Permissibleforfieldagent { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
    }
}