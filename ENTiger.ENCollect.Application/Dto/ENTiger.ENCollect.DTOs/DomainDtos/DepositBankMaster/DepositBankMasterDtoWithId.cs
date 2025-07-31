using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DepositBankMasterDtoWithId : DepositBankMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}