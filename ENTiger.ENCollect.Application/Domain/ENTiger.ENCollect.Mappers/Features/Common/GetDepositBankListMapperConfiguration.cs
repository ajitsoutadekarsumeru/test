using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepositBankListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDepositBankListMapperConfiguration() : base()
        {
            CreateMap<DepositBankMaster, GetDepositBankListDto>();
        }
    }
}