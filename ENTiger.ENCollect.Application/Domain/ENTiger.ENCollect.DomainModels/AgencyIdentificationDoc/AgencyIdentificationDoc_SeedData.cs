namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyIdentificationDoc : TFlexIdentificationDoc<AgencyIdentification, AgencyIdentificationDoc, Agency>
    {
        public static IEnumerable<AgencyIdentificationDoc> GetSeedData()
        {
            ICollection<AgencyIdentificationDoc> seedData = new List<AgencyIdentificationDoc>()
            {
                //add your object collection seed here:

                new AgencyIdentificationDoc()
                {
                }
            };

            return seedData;
        }
    }
}