namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Language : DomainModelBridge
    {
        public static IEnumerable<Language> GetSeedData()
        {
            ICollection<Language> seedData = new List<Language>()
            {
                //add your object collection seed here:

                new Language()
                {
                }
            };

            return seedData;
        }
    }
}