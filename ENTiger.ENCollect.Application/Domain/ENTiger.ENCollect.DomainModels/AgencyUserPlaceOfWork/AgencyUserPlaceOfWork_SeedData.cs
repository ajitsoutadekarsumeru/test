namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserPlaceOfWork : DomainModelBridge
    {
        public static IEnumerable<AgencyUserPlaceOfWork> GetSeedData()
        {
            ICollection<AgencyUserPlaceOfWork> seedData = new List<AgencyUserPlaceOfWork>()
            {
                //add your object collection seed here:

                new AgencyUserPlaceOfWork()
                {
                }
            };

            return seedData;
        }
    }
}