namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserPlaceOfWork : DomainModelBridge
    {
        public static IEnumerable<CompanyUserPlaceOfWork> GetSeedData()
        {
            ICollection<CompanyUserPlaceOfWork> seedData = new List<CompanyUserPlaceOfWork>()
            {
                //add your object collection seed here:

                new CompanyUserPlaceOfWork()
                {
                }
            };

            return seedData;
        }
    }
}