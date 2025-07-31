namespace ENCollect.DynaFilter.Tests.Stubs
{
    public class TestRecommender
    {
        public string Branch { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal ThresholdRecommendationAmount { get; set; } = 0;

        // For the plain "Has" scenario: 
        public List<string> States { get; set; } = [];

        // For the dot-based "Has" scenario:
        public List<TestScopeOfWork>? ScopeOfWorks { get; set; }
    }
}