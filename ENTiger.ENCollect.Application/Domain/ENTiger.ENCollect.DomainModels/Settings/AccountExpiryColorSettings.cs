
namespace ENTiger.ENCollect.DomainModels.Settings
{
    /// <summary>
    /// If the allocation expires with a certain amout of days then return a color indication of possible priority
    /// </summary>
    public class AccountExpiryColorSettings
    {
        //2025-03-31 - change class name to be more specific to the function
        public int AllocationExpiryRedDays { get; set; } = 0; //if allocations expires within 0-5 days then color is red
        public int AllocationExpiryAmberDays { get; set; } = 6; //if allocations expires within 6-15 days then color is amber - MUST FOLLOW ON AllocationExpiryRedDays - NO OVERLAP
        public int AllocationExpiryGreenDays { get; set; } = 16; //if allocations expires in more than 15 days then color is green - MUST FOLLOW ON AllocationExpiryAmberDays - NO OVERLAP
    }
}
