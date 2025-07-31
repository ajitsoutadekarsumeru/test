namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetStateByIdDto : DtoBridge
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        //public string? PrimaryLanguage { get; set; }
        //public string? SecondaryLanguage { get; set; }

        public string RegionId { get; set; }

        public string RegionName { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}