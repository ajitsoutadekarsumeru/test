namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchStateDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }

        //public string PrimaryLanguage { get; set; }
        //public string SecondaryLanguage { get; set; }

        public string RegionId { get; set; }

        public string RegionName { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}