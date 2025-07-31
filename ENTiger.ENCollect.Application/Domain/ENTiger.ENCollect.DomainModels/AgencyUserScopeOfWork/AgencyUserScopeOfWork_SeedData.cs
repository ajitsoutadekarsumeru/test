namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserScopeOfWork : DomainModelBridge
    {
        public static IEnumerable<AgencyUserScopeOfWork> GetSeedData()
        {
            ICollection<AgencyUserScopeOfWork> seedData = new List<AgencyUserScopeOfWork>()
            {
                //add your object collection seed here:

                new AgencyUserScopeOfWork()
                {
                }
            };

            return seedData;
        }
    }
}