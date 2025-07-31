using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetEmailFromAccountContactHistoryDto : DtoBridge
    {
        public string Email { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ContactSource { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
