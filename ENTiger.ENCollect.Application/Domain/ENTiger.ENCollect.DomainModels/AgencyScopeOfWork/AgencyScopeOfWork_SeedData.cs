namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyScopeOfWork : DomainModelBridge
    {
        public static IEnumerable<AgencyScopeOfWork> GetSeedData()
        {
            ICollection<AgencyScopeOfWork> seedData = new List<AgencyScopeOfWork>()
            {
                //add your object collection seed here:

                new AgencyScopeOfWork()
                {
                }
            };

            return seedData;
        }
    }
}