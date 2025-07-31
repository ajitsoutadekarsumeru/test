namespace ENTiger.ENCollect
{
    public partial class FeatureMasterDto : DtoBridge
    {
        public string Parameter { get; protected set; }
        public string Value { get; protected set; }
        public bool IsEnabled { get; protected set; }
    }
}