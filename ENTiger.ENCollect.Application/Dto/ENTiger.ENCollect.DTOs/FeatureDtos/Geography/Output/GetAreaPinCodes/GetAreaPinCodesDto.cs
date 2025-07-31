namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAreaPinCodesDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? PinCodeId { get; set; }
        public string? pinCode { get; set; }
        public string? AreaId { get; set; }
        public string? AreaName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}