namespace ENTiger.ENCollect
{
    public partial class LanguageDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}