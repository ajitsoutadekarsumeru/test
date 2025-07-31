namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Treatment : DomainModelBridge
    {
        public static IEnumerable<Treatment> GetSeedData()
        {
            ICollection<Treatment> seedData = new List<Treatment>()
            {
                //add your object collection seed here:

                new Treatment()
                {
                }
            };

            return seedData;
        }
    }
}