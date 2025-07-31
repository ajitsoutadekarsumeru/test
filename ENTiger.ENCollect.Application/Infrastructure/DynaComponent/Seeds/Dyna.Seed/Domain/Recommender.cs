namespace ENCollect.Dyna.Seed;
public class Recommender
{
    public string Branch { get; set; } = string.Empty;
    public string Product { get; set; } = String.Empty;
    public string Region { get; set; } = String.Empty;
    public decimal ThresholdRecommendationAmount { get; set; } = 0;

    /// <summary>
    /// A list of states that this Recommender covers 
    /// (testing our "Has" operator for a simple list-of-strings).
    /// </summary>
    public List<string> States { get; set; } = [];

    /// <summary>
    /// A list of detailed scope-of-work items, 
    /// each with a City or other fields. 
    /// We want to test "ScopeOfWorks.City" => .Any(...) logic.
    /// </summary>
    public List<ScopeOfWork> ScopeOfWorks { get; set; } = [];
}