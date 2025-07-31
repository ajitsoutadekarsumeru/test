namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MyCollectionsDto : DtoBridge
    {
        public int count { get; set; }
        public ICollection<MyCollectionFlowModel> MyCollectionFlow { get; set; }
        public ICollection<MyCollectionRecoModel> MyCollectionReco { get; set; }
    }

    public partial class MyCollectionsAccountsDto : DtoBridge
    {
        public string? Id { get; set; }
        public decimal? BOM_POS { get; set; }

        public string? PAYMENTSTATUS { get; set; }

        public string? CURRENT_POS { get; set; }
    }

    public class MyCollectionFlowModel
    {
        public string Flows { get; set; }
        public string Unit { get; set; }
        public string POS { get; set; }
        public string POSPercentage { get; set; }
    }

    public class MyCollectionRecoModel
    {
        public string Reco { get; set; }
        public string Unit { get; set; }
        public string POS { get; set; }
        public string RORPercentage { get; set; }
    }
}