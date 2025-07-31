namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyPlaceOfWork : DomainModelBridge
    {
        public static IEnumerable<AgencyPlaceOfWork> GetSeedData()
        {
            ICollection<AgencyPlaceOfWork> seedData = new List<AgencyPlaceOfWork>()
            {
                //add your object collection seed here:

                new AgencyPlaceOfWork()
                {
                }
            };

            return seedData;
        }
    }
}