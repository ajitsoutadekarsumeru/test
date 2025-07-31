using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TenantSMSConfiguration
    {
        [StringLength(32)]
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }

        [StringLength(32)]
        public string TenantId { get; set; }
        public FlexTenantBridge Tenant { get; set; }

        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
