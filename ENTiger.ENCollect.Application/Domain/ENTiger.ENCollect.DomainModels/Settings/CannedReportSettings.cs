
namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the configuration settings for generating canned reports.
    /// </summary>
    public class CannedReportSetting
    {
        /// <summary>
        /// Gets or sets the list of email addresses to which the report will be sent.
        /// Multiple email addresses should be separated by a comma.
        /// </summary>
        public string EmailIds { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Tenant ID associated with the report.
        /// </summary>
        public string TenantId { get; set; } = "1";

        /// <summary>
        /// Gets or sets the name of the report being generated.
        /// </summary>
        public string ReportName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the stored procedure name used to fetch the report data.
        /// </summary>
        public string StoredProcedureName { get; set; } = string.Empty;
    }

}
