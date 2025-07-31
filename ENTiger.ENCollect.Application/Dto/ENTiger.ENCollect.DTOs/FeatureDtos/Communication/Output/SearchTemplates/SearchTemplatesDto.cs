namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class SearchTemplatesDto : DtoBridge
    {
        public string Id { get; set; }
        public string TemplateName { get; set; }
        public string Channel { get; set; }
        public string[] Languages { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public List<string> ConnectedTriggers { get; set; }
        public string Status { get; set; }
        public string EntryPoint { get; set; }
        public string RecipientType { get; set; }
    }
}