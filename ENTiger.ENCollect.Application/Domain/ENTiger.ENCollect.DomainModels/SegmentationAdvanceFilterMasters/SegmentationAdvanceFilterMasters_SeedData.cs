namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationAdvanceFilterMasters : DomainModelBridge
    {
        public static IEnumerable<SegmentationAdvanceFilterMasters> GetSeedData()
        {
            ICollection<SegmentationAdvanceFilterMasters> seedData = new List<SegmentationAdvanceFilterMasters>()
            {
                //add your object collection seed here:

                new SegmentationAdvanceFilterMasters()
                {
                }
            };

            return seedData;
        }
    }
}