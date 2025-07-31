namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TrailGapDownload : DomainModelBridge
    {
        public static IEnumerable<TrailGapDownload> GetSeedData()
        {
            ICollection<TrailGapDownload> seedData = new List<TrailGapDownload>()
            {
                //add your object collection seed here:

                new TrailGapDownload()
                {
                }
            };

            return seedData;
        }
    }
}