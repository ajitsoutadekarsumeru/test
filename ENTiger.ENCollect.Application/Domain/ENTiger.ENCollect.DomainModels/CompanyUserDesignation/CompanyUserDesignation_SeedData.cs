namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserDesignation : DomainModelBridge
    {
        public static IEnumerable<CompanyUserDesignation> GetSeedData()
        {
            ICollection<CompanyUserDesignation> seedData = new List<CompanyUserDesignation>()
            {
                //add your object collection seed here:

                new CompanyUserDesignation()
                {
                }
            };

            return seedData;
        }
    }
}