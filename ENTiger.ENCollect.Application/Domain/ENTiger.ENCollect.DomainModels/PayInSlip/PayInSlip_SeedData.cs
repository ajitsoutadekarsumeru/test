namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PayInSlip : DomainModelBridge
    {
        public static IEnumerable<PayInSlip> GetSeedData()
        {
            ICollection<PayInSlip> seedData = new List<PayInSlip>()
            {
                //add your object collection seed here:

                new PayInSlip()
                {
                }
            };

            return seedData;
        }
    }
}