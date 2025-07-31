namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Department : DomainModelBridge
    {
        public static IEnumerable<Department> GetSeedData()
        {
            ICollection<Department> seedData = new List<Department>()
            {
                //add your object collection seed here:

                new Department()
                {
                }
            };

            return seedData;
        }
    }
}