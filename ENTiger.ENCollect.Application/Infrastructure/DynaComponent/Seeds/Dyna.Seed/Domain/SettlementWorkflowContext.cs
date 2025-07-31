namespace ENCollect.Dyna.Seed;

/// <summary>
/// A multi-domain context data packet that will be passed through steps (in workflows), conditions (in cascading flows)
/// The individual steps and conditions can use the data in the packet as they see fit.
/// </summary>
public class SettlementWorkflowContext : IContextDataPacket
{
    public decimal PrincipalWaiver { get; set; } = 0;
    public decimal InterestWaiver { get; set; } = 0;
    public bool IsNpa { get; set; } = false;
    public string Product { get; set; } = string.Empty;
}

/// <summary>
/// An example entity that we want to filter:
/// e.g. an "ApprovalGrid" with range constraints for principal, interest, etc.
/// </summary>
public class ApprovalGrid
{
    public string Name { get; set; } = String.Empty;
    public bool NpaAllowed { get; set; } = true;
    public decimal MinPrincipalWaiver { get; set; } = 0;
    public decimal MaxPrincipalWaiver { get; set; } = 0;
    public decimal MinInterestWaiver { get; set; } = 0;
    public decimal MaxInterestWaiver { get; set; } = 0;
    public string Product { get; set; } = String.Empty;
    public string Designation { get; set; } = String.Empty;
}