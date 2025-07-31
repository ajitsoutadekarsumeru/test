namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CommunicationTemplateDetail : DomainModelBridge
    {
        public static IEnumerable<CommunicationTemplateDetail> GetSeedData()
        {
            ICollection<CommunicationTemplateDetail> seedData = new List<CommunicationTemplateDetail>()
            {
                //add your object collection seed here:

                new CommunicationTemplateDetail()
                {
                }
            };

            return seedData;
        }
    }
}