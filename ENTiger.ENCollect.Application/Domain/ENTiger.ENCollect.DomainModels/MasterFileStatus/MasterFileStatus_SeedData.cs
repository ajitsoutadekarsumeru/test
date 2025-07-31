namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class MasterFileStatus : DomainModelBridge
    {
        public static IEnumerable<MasterFileStatus> GetSeedData()
        {
            ICollection<MasterFileStatus> seedData = new List<MasterFileStatus>()
            {
                //add your object collection seed here:

                new MasterFileStatus()
                {
                }
            };

            return seedData;
        }
    }
}