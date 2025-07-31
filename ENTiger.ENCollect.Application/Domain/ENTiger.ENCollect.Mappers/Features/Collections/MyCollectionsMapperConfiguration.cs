using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MyCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MyCollectionsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, MyCollectionsAccountsDto>();
        }
    }
}