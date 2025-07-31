namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchBaseBranchDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string NickName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}