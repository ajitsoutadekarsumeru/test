using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbBaseConfiguration : NsbDefaultEndpointConfiguration
    {
        private string SERVICE_CONTROL_METRICS_ADDRESS = "particular-monitoring";
        private string SERVICE_CONTROL_QUEUE_ADDRESS = "particular-queue";
        private string SERVICE_CONTROL_AUDIT = "particular-audit";

        public NsbBaseConfiguration(string endpointName, IConfiguration configuration, IHostEnvironment env, string errorQueueName = "error") : base(endpointName, errorQueueName)
        {
            SERVICE_CONTROL_METRICS_ADDRESS = configuration.GetSection("ServiceControlSettings")["MonitoringName"] ?? "particular-monitoring";
            SERVICE_CONTROL_QUEUE_ADDRESS = configuration.GetSection("ServiceControlSettings")["QueueName"] ?? "particular-queue";
            SERVICE_CONTROL_AUDIT = configuration.GetSection("ServiceControlSettings")["AuditName"] ?? "particular-audit";
            bool enableMetrics = Convert.ToBoolean(configuration.GetSection("ServiceControlSettings")["EnableMetrics"] ?? "false");
            int maxconcurrency = Convert.ToInt16(configuration.GetSection("ConcurrencySettings")["Maximum"] ?? "4");

            this.LimitMessageProcessingConcurrencyTo(maxconcurrency);
            System.Diagnostics.Trace.WriteLine("Started with MaxConcurrency : " + maxconcurrency);
            this.EnableInstallers();
            //if (env.IsDevelopment())
            //{
            //    this.EnableInstallers();
            //}
            //else if (env.IsStaging())
            //{
            //    var runInstallers = Environment.GetCommandLineArgs().Any(x => string.Equals(x, "/runInstallers", StringComparison.OrdinalIgnoreCase));

            //    if (runInstallers)
            //    {
            //        this.EnableInstallers();
            //    }
            //}
            //else if (env.IsProduction())
            //{
            //    var runInstallers = Environment.GetCommandLineArgs().Any(x => string.Equals(x, "/runInstallers", StringComparison.OrdinalIgnoreCase));

            //    if (runInstallers)
            //    {
            //        this.EnableInstallers();
            //    }
            //}

            this.DefineCriticalErrorAction(OnCriticalError);

            var endPointSection = configuration.GetSection("EndPoint");
            var isWebRequestEndPointValue = endPointSection?["IsWebRequestEndPoint"];

            if (!string.IsNullOrEmpty(isWebRequestEndPointValue))
            {
                if (bool.TryParse(isWebRequestEndPointValue, out bool isWebRequestEndPoint) && isWebRequestEndPoint)
                {
                    this.ConfigureFlexCorrelationForHttpRequest();
                }
            }

            //Enable Flex Correlation with Serilog
            this.EnableFlexCorrelationWithSerilog();

            if (enableMetrics)
            {
                var metrics = this.EnableMetrics();

                metrics.SendMetricDataToServiceControl(
                    serviceControlMetricsAddress: SERVICE_CONTROL_METRICS_ADDRESS,
                    interval: TimeSpan.FromMinutes(1),
                    instanceId: "INSTANCE_ID_OPTIONAL");
                System.Diagnostics.Trace.WriteLine("SendMetricDataTo : " + SERVICE_CONTROL_METRICS_ADDRESS + " - Initialized");

                this.SendHeartbeatTo(
                    serviceControlQueue: SERVICE_CONTROL_QUEUE_ADDRESS,
                    frequency: TimeSpan.FromSeconds(15),
                    timeToLive: TimeSpan.FromSeconds(30));
                System.Diagnostics.Trace.WriteLine("SendHeartbeatTo : " + SERVICE_CONTROL_QUEUE_ADDRESS + " - Initialized");

                this.AuditProcessedMessagesTo(SERVICE_CONTROL_AUDIT);
                System.Diagnostics.Trace.WriteLine("AuditProcessedMessagesTo : " + SERVICE_CONTROL_AUDIT + " - Initialized");
            }
        }

        private async Task OnCriticalError(ICriticalErrorContext context, CancellationToken cancellationToken)
        {
            try
            {
                // To leave the process active, stop the endpoint.
                // When it is stopped, attempts to send messages will cause an ObjectDisposedException.
                await context.Stop(cancellationToken).ConfigureAwait(false);

                // Perform custom actions here
            }
            finally
            {
                var failMessage = $"Critical error shutting down:'{context.Error}'.";
                Environment.FailFast(failMessage, context.Exception);
            }
        }
    }
}