namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetIdentificationTypesDto : DtoBridge
    {
        public string Id { get; set; }

        public string IdentificationType { get; set; }

        public ICollection<IdentificationDoctypeOutputModel> IdentificationDocTypes { get; set; }
    }

    public class IdentificationDoctypeOutputModel
    {
        public string Id { get; set; }

        public string IdentificationDoc { get; set; }
    }
}