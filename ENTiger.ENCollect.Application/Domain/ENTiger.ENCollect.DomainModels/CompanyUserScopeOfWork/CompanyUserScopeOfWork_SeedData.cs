namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserScopeOfWork : DomainModelBridge
    {
        public static IEnumerable<CompanyUserScopeOfWork> GetSeedData()
        {
            ICollection<CompanyUserScopeOfWork> seedData = new List<CompanyUserScopeOfWork>()
            {
                //add your object collection seed here:

                new CompanyUserScopeOfWork()
                {
                }
            };

            return seedData;
        }
    }
}