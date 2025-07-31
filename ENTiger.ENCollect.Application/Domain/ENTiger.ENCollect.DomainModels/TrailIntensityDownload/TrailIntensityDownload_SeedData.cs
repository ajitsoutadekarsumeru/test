namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TrailIntensityDownload : DomainModelBridge
    {
        public static IEnumerable<TrailIntensityDownload> GetSeedData()
        {
            ICollection<TrailIntensityDownload> seedData = new List<TrailIntensityDownload>()
            {
                //add your object collection seed here:

                new TrailIntensityDownload()
                {
                }
            };

            return seedData;
        }
    }
}