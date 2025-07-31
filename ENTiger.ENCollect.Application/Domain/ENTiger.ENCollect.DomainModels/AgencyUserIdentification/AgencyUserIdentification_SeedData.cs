namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserIdentification : TFlexIdentification<AgencyUserIdentification, AgencyUserIdentificationDoc, AgencyUser>
    {
        public static IEnumerable<AgencyUserIdentification> GetSeedData()
        {
            ICollection<AgencyUserIdentification> seedData = new List<AgencyUserIdentification>()
            {
                //add your object collection seed here:

                new AgencyUserIdentification()
                {
                }
            };

            return seedData;
        }
    }
}