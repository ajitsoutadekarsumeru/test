namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings for service control, including metrics and monitoring configurations.
    /// </summary>
    public class ServiceControlSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether metrics are enabled for service control.
        /// Default is false.
        /// </summary>
        public bool EnableMetrics { get; set; } = false;

        /// <summary>
        /// Gets or sets the monitoring configuration string for service control.
        /// Default is "Particular.Monitoring".
        /// </summary>
        public string MonitoringName { get; set; } = "Particular.Monitoring";

        /// <summary>
        /// Gets or sets the queue configuration string for service control.
        /// Default is "Particular.Servicecontrol".
        /// </summary>
        public string QueueName { get; set; } = "Particular.Servicecontrol";

        /// <summary>
        /// Gets or sets the audit configuration string for service control.
        /// Default is "Particular.Servicecontrol.Audit".
        /// </summary>
        public string AuditName { get; set; } = "Particular.Servicecontrol.Audit";
    }
}