namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationAdvanceFilter : DomainModelBridge
    {
        public static IEnumerable<SegmentationAdvanceFilter> GetSeedData()
        {
            ICollection<SegmentationAdvanceFilter> seedData = new List<SegmentationAdvanceFilter>()
            {
                //add your object collection seed here:

                new SegmentationAdvanceFilter()
                {
                }
            };

            return seedData;
        }
    }
}