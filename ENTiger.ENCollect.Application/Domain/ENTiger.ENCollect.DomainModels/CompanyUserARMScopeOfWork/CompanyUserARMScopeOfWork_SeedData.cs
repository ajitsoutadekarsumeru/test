namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserARMScopeOfWork : DomainModelBridge
    {
        public static IEnumerable<CompanyUserARMScopeOfWork> GetSeedData()
        {
            ICollection<CompanyUserARMScopeOfWork> seedData = new List<CompanyUserARMScopeOfWork>()
            {
                //add your object collection seed here:

                new CompanyUserARMScopeOfWork()
                {
                }
            };

            return seedData;
        }
    }
}