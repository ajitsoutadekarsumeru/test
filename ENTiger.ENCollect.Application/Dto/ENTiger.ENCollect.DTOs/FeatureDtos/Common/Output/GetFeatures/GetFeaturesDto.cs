namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetFeaturesDto : DtoBridge
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool? IsEnabled { get; set; }
    }
}