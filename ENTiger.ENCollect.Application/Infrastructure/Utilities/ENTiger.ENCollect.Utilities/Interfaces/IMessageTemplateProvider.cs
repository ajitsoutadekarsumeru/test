namespace ENTiger.ENCollect
{
    public interface IMessageTemplate
    {
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string SMSMessage { get; set; }
    }
}