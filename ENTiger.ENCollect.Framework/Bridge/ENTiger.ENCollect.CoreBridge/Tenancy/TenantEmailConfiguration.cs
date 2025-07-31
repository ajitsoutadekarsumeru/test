using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public  class TenantEmailConfiguration
    {
        [StringLength(32)]
        public string Id {  get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }

        [StringLength(32)]
        public string TenantId { get; set; }
        public FlexTenantBridge Tenant { get; set; }

        [StringLength(500)]
        public string? Mailcc { get; set; }

        [StringLength(100)]
        public string MailFrom { get; set; }

        [StringLength(200)]
        public string? EmailLogPath { get; set; }

        [StringLength(100)]
        public string? MailTo { get; set; }

        [StringLength(100)]
        public string? MailSignature { get; set; }

        [StringLength(100)]
        public string? SmtpServer { get; set; }

        [StringLength(20)]
        public string? SmtpPort { get; set; }

        [StringLength(100)]
        public string? SmtpUser { get; set; }

        [StringLength(100)]
        public string? SmtpPassword { get; set; }

        [StringLength(50)]
        public string? EnableSsl { get; set; }

        
    }
}
