namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class State : DomainModelBridge
    {
        public static IEnumerable<State> GetSeedData()
        {
            ICollection<State> seedData = new List<State>()
            {
                //add your object collection seed here:

                new State()
                {
                }
            };

            return seedData;
        }
    }
}