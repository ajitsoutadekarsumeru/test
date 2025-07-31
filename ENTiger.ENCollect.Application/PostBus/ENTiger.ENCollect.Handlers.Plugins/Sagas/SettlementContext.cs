

using ENCollect.Dyna.Cascading;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// A domain context class implementing <see cref="IContextDataPacket"/>,
    /// used by settlement workflow steps to determine if a step is needed
    /// and to apply aggregator-based conditions (via actor flow).
    /// </summary>
    public class SettlementContext : IContextDataPacket
    {
        /// <summary>
        /// EXAMPLE FIELD. 
        /// Indicates whether the account is NPA (non-performing asset).
        /// Some workflows only require steps for NPA settlements.
        /// </summary>
        public string IsNpa { get; set; }
        public int RequestorLevel { get; set; }
        public string RequestorId { get; set; }

        /// <summary>
        /// EXAMPLE FIELD. 
        /// The total principal waiver requested for this settlement.
        /// Typically used by steps that do .IsNeeded(...) checks, e.g.,
        /// "if (PrincipalWaiver &gt; 0) => require a recommendation step."
        /// </summary>
        public decimal PrincipalWaiverPercentage { get; set; }

        /// <summary>
        /// EXAMPLE FIELD. 
        /// The total interest waiver requested for this settlement.
        /// Similarly used by steps that do interest-based approvals.
        /// </summary>
        public decimal InterestAndChargesWaiver { get; set; }
    }
}