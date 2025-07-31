using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetAccountBalanceDetailsDto : DtoBridge
    {
        [Required]
        public string? AccountNumber { get; set; }

        [Required]
        public string? Referenceid { get; set; }
    }

}
