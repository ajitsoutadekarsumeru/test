namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Segmentation : DomainModelBridge
    {
        public static IEnumerable<Segmentation> GetSeedData()
        {
            ICollection<Segmentation> seedData = new List<Segmentation>()
            {
                //add your object collection seed here:

                new Segmentation()
                {
                }
            };

            return seedData;
        }
    }
}