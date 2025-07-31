namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CommunicationTemplate : DomainModelBridge
    {
        public static IEnumerable<CommunicationTemplate> GetSeedData()
        {
            ICollection<CommunicationTemplate> seedData = new List<CommunicationTemplate>()
            {
                //add your object collection seed here:

                new CommunicationTemplate()
                {
                }
            };

            return seedData;
        }
    }
}