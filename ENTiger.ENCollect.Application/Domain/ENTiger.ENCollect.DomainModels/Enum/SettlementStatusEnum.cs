using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class SettlementStatusEnum : FlexEnum
    {
        /// <summary>
        /// UI category for grouping (e.g. "Open", "Closed").
        /// </summary>
        public string Group { get; private set; }
        public SettlementStatusEnum()
        { }

        public SettlementStatusEnum(string value, string displayName, string group) 
            : base(value, displayName)
        {
            Group = group;
        }
        public static readonly SettlementStatusEnum Requested = new SettlementStatusEnum("Requested", "Requested", "Open");
        public static readonly SettlementStatusEnum Updated = new SettlementStatusEnum("Updated", "Updated", "Open");
        public static readonly SettlementStatusEnum UnderEvaluation = new SettlementStatusEnum("Under Evaluation", "Under Evaluation", "Open");

        public static readonly SettlementStatusEnum Negotiation = new SettlementStatusEnum("Under Negotiation", "Under Negotiation", "Open");
        public static readonly SettlementStatusEnum PendingCustomerAcceptance = new SettlementStatusEnum("Pending Customer Acceptance", "Pending Customer Acceptance", "Open");


        public static readonly SettlementStatusEnum RequestRejected = new SettlementStatusEnum("Request Rejected", "Request Rejected", "Closed");
        public static readonly SettlementStatusEnum CustomerRejectedOffer = new SettlementStatusEnum("Customer Rejected Offer", "Customer Rejected Offer", "Closed");
        public static readonly SettlementStatusEnum CustomerAcceptedOffer = new SettlementStatusEnum("Customer Accepted Offer", "Customer Accepted Offer", "Closed");

        public static readonly SettlementStatusEnum PendingApproval = new SettlementStatusEnum("Pending Approval", "Pending Approval", "Open");

        public static readonly SettlementStatusEnum Declined = new SettlementStatusEnum("Declined", "Declined", "Closed");
        public static readonly SettlementStatusEnum Cancelled = new SettlementStatusEnum("Cancelled", "Cancelled", "Closed");


        /// <summary>
        /// Returns all defined settlement statuses.
        /// </summary>
        public static IEnumerable<SettlementStatusEnum> List() =>
            GetAll<SettlementStatusEnum>();

        /// <summary>
        /// Returns all statuses in the given UI group.
        /// </summary>
        public static IEnumerable<SettlementStatusEnum> GetAll() =>
            List();
        public static IEnumerable<SettlementStatusEnum> ByGroup(string group) =>
            List().Where(s => s.Group.Equals(group, StringComparison.OrdinalIgnoreCase));

        public static SettlementStatusEnum ByValue(string value) =>
            List().Where(s => s.Value.Equals(value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        public static SettlementStatusEnum ByDisplayName(string displayName) =>
            List().Where(s => s.DisplayName.Equals(displayName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
    }
}