using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetAccountBalanceOutputDetailsDto : DtoBridge
    {
        public string? CBSBalance { get; set; }
        public string? LastUpdatedOn { get; set; }
    }

}
