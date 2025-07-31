namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BulkTrailUploadFile : DomainModelBridge
    {
        public static IEnumerable<BulkTrailUploadFile> GetSeedData()
        {
            ICollection<BulkTrailUploadFile> seedData = new List<BulkTrailUploadFile>()
            {
                //add your object collection seed here:

                new BulkTrailUploadFile()
                {
                }
            };

            return seedData;
        }
    }
}