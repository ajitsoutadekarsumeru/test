namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectorsDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? CustomId { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }
        public string? BaseBranchId { get; set; }
    }
}