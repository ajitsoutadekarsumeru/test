namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DesignationType : DomainModelBridge
    {
        public static IEnumerable<DesignationType> GetSeedData()
        {
            ICollection<DesignationType> seedData = new List<DesignationType>()
            {
                //add your object collection seed here:

                new DesignationType()
                {
                }
            };

            return seedData;
        }
    }
}