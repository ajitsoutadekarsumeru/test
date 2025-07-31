namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserDesignation : DomainModelBridge
    {
        public static IEnumerable<AgencyUserDesignation> GetSeedData()
        {
            ICollection<AgencyUserDesignation> seedData = new List<AgencyUserDesignation>()
            {
                //add your object collection seed here:

                new AgencyUserDesignation()
                {
                }
            };

            return seedData;
        }
    }
}