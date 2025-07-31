namespace ENTiger.ENCollect.CategoryModule
{
    public partial class SearchCategoryItemsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ParentName { get; set; }
        public string? ParentId { get; set; }
    }
}