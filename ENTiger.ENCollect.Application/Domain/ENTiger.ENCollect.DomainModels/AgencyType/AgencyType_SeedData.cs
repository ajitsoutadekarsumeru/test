namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyType : DomainModelBridge
    {
        public static IEnumerable<AgencyType> GetSeedData()
        {
            ICollection<AgencyType> seedData = new List<AgencyType>()
            {
                //add your object collection seed here:

                new AgencyType()
                {
                    Id = "27d4c2e0ce1a438cb44cd7fb8ed552b9",
                    MainType = "Collections",
                    SubType = AgencySubTypeEnum.TeleCalling.Value
                },
                new AgencyType()
                {
                    Id = "ff379ce22f7b4aca9e74d0dadccb3739",
                    MainType = "Collections",
                    SubType = AgencySubTypeEnum.FieldAgent.Value
                }
            };

            return seedData;
        }
    }
}