namespace ENCollect.Dyna.Filters;
/// <summary>
/// A settlement request. Could have a requestor name plus the amount that needs recommending.
/// </summary>
public class Settlement
{
    public string Requestor { get; set; } = String.Empty;
    public decimal SettlementAmount { get; set; } = 0;
}