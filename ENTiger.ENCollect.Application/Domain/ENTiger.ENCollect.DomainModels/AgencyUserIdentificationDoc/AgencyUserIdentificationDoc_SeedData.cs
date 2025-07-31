namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserIdentificationDoc : TFlexIdentificationDoc<AgencyUserIdentification, AgencyUserIdentificationDoc, AgencyUser>
    {
        public static IEnumerable<AgencyUserIdentificationDoc> GetSeedData()
        {
            ICollection<AgencyUserIdentificationDoc> seedData = new List<AgencyUserIdentificationDoc>()
            {
                //add your object collection seed here:

                new AgencyUserIdentificationDoc()
                {
                }
            };

            return seedData;
        }
    }
}