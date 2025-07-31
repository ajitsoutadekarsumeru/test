namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionCodeMaster : DomainModelBridge
    {
        public static IEnumerable<DispositionCodeMaster> GetSeedData()
        {
            ICollection<DispositionCodeMaster> seedData = new List<DispositionCodeMaster>()
            {
                //add your object collection seed here:

                new DispositionCodeMaster()
                {
                }
            };

            return seedData;
        }
    }
}