namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AreaPinCodeMapping : DomainModelBridge
    {
        public static IEnumerable<AreaPinCodeMapping> GetSeedData()
        {
            ICollection<AreaPinCodeMapping> seedData = new List<AreaPinCodeMapping>()
            {
                //add your object collection seed here:

                new AreaPinCodeMapping()
                {
                }
            };

            return seedData;
        }
    }
}