using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAddressFromAccountContactHistoryDto : DtoBridge
    {
        public string Address { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ContactSource { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
