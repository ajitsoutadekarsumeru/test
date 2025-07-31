namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCountryByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
    }
}