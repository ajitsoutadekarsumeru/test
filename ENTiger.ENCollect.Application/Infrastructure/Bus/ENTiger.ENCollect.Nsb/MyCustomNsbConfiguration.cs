namespace ENTiger.ENCollect
{
    //Example custom cunfiguration to configure NServiceBus from scratch
    //You can make different copy of this configuration using the name suitable for your as you want in your application and configure it according to your needs
    public class MyCustomNsbConfiguration : EndpointConfiguration
    {
        public MyCustomNsbConfiguration(string endpointName, string errorQueueName = "error") : base(endpointName)
        {
            this.SendFailedMessagesTo(errorQueueName);

            //Add all other NSB Configuration here
        }
    }
}