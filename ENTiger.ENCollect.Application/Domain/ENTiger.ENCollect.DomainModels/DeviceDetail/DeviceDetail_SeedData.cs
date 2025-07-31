namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeviceDetail : DomainModelBridge
    {
        public static IEnumerable<DeviceDetail> GetSeedData()
        {
            ICollection<DeviceDetail> seedData = new List<DeviceDetail>()
            {
                //add your object collection seed here:

                new DeviceDetail()
                {
                }
            };

            return seedData;
        }
    }
}