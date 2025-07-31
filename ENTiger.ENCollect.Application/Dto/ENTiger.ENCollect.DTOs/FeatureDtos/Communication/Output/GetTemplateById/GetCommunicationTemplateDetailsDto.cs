namespace ENTiger.ENCollect.CommunicationModule
{
    public class GetCommunicationTemplateDetailsDto : DtoBridge
    {
        public string Id { get; set; }
        public string CommunicationTemplateId { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Language { get; set; }
    }
}