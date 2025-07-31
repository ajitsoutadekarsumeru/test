using NServiceBus;

namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// Minimal saga data for a dynamic approval workflow,
    /// using a single WorkflowInstanceId for NServiceBus correlation,
    /// and a DomainId for direct domain usage (repo lookups, aggregator usage, etc.).
    /// </summary>
    public class DynaWorkflowSagaData : IContainSagaData
    {
        // Required by NServiceBus saga infrastructure
        public Guid Id { get; set; }
        public string Originator { get; set; } = string.Empty;
        public string OriginalMessageId { get; set; } = string.Empty;

        /// <summary>
        /// A single property used by NServiceBus for saga correlation.
        /// Typically formed by concatenating or hashing domain info 
        /// (e.g. "Settlement::1111-2222-...").
        /// </summary>
        public string WorkflowInstanceId { get; set; } = string.Empty;

        /// <summary>
        /// The actual domain entity ID (e.g. SettlementId), 
        /// for aggregator logic, etc.
        /// </summary>
        public string DomainId { get; set; }

        /// <summary>
        /// Indicates if the workflow saga is complete.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// The current step index in the linear approval flow.
        /// </summary>
        public int CurrentStepIndex { get; set; }

        /// <summary>
        /// Current active step, or <c>null</c> until the workflow selects its first
        /// needed node.
        /// </summary>
        public string? CurrentStepName { get; set; }
    }
}