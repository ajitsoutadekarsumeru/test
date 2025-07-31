
namespace ENTiger.ENCollect
{
    public class CronJobSettings
    {
        /// <summary>
        /// Gets or sets the cron string for the cron background worker.
        /// </summary>
        public string CustomerConsentExpiry { get; set; } = "0 8 * * *";//08h00 daily - https://crontab.guru/examples.html
        public string CustomerConsentExpiryTenantId { get; set; } = "";
        public int CustomerConsentExpiryCronTimeout { get; set; } = 30; //in seconds - default 30s
        public string BPTPFeedbackCron { get; set; } = "0 23 * * *";//23h00 daily
        public string UserDormantStatusCheck { get; set; } = "0 8 * * *";//08h00 daily - https://crontab.guru/examples.html
        public string CronTenantId { get; set; } = "";
        public int CronTimeout { get; set; } = 30; //in seconds - default 30s
        public string DailySummaryCron { get; set; } = "0 23 * * *";//23h00 daily
        public int FileRetentionDays { get; set; } = 30;
        public string ArchiveScheduleCron { get; set; } = "0 0 * * *"; // Run daily at midnight, filter in code for "last day"=
        public string GeoScheduleCron { get; set; } = "* * * * *";

        public string PaymentStatusCron { get; set; } = "0 1 * * *";
    }
}
