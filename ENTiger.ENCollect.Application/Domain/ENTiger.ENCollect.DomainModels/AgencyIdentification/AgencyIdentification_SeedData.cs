namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyIdentification : TFlexIdentification<AgencyIdentification, AgencyIdentificationDoc, Agency>
    {
        public static IEnumerable<AgencyIdentification> GetSeedData()
        {
            ICollection<AgencyIdentification> seedData = new List<AgencyIdentification>()
            {
                //add your object collection seed here:

                new AgencyIdentification()
                {
                }
            };

            return seedData;
        }
    }
}