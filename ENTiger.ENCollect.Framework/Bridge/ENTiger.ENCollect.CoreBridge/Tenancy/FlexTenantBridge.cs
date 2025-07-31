using System;
using System.ComponentModel.DataAnnotations;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FlexTenantBridge : FlexTenant
    {
        [StringLength(32)]
        public override string Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }

        [StringLength(50)]
        public new string Name { get; set; }
        [StringLength(50)]
        public new string HostName { get; set; }
        [StringLength(50)]
        public new string TenantDbType { get; set; }
        [StringLength(500)]
        public new string DefaultWriteDbConnectionString { get; set; }
        [StringLength(500)]
        public new string DefaultReadDbConnectionString { get; set; }
        [StringLength(50)]
        public string? Color { get; set; }
        [StringLength(50)]
        public string? Logo { get; set; }
    }
}
