namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class USGetListDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? FilterName { get; set; }
        public string? UseCaseName { get; set; }
        public dynamic? FilterValues { get; set; }
    }
}