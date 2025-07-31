namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepartmentType : DomainModelBridge
    {
        public static IEnumerable<DepartmentType> GetSeedData()
        {
            ICollection<DepartmentType> seedData = new List<DepartmentType>()
            {
                //add your object collection seed here:

                new DepartmentType()
                {
                }
            };

            return seedData;
        }
    }
}