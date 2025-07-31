namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAppVersionDetailsDto : DtoBridge
    {
        public bool IsVersionCheck { get; set; }
        public string Message { get; set; }
        public string AppUrl { get; set; }
    }
}