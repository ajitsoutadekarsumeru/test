namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionValidationMaster : DomainModelBridge
    {
        public static IEnumerable<DispositionValidationMaster> GetSeedData()
        {
            ICollection<DispositionValidationMaster> seedData = new List<DispositionValidationMaster>()
            {
                //add your object collection seed here:

                new DispositionValidationMaster()
                {
                }
            };

            return seedData;
        }
    }
}