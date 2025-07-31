using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepositBankMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DepositBankMasterMapperConfiguration() : base()
        {
            CreateMap<DepositBankMasterDto, DepositBankMaster>();
            CreateMap<DepositBankMaster, DepositBankMasterDto>();
            CreateMap<DepositBankMasterDtoWithId, DepositBankMaster>();
            CreateMap<DepositBankMaster, DepositBankMasterDtoWithId>();
        }
    }
}