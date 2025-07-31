namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDesignationsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public string? DesignationAcronym { get; set; }
    }
}