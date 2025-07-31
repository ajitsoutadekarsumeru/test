namespace ENTiger.ENCollect.CommonModule
{
    public partial class GetTriggerTypesDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EntryPoint { get; set; }
        public bool RequiresDaysOffset { get; set; }
    }
}
