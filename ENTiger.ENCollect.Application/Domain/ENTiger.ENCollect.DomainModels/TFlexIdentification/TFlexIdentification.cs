using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public abstract class TFlexIdentification<TTFlexIdentification, TTFlexIdentificationDoc, TTFlex> : DomainModelBridge
        where TTFlexIdentification : TFlexIdentification<TTFlexIdentification, TTFlexIdentificationDoc, TTFlex>, new()
        where TTFlexIdentificationDoc : TFlexIdentificationDoc<TTFlexIdentification, TTFlexIdentificationDoc, TTFlex>, new()
        where TTFlex : TFlex, new()
    {
        [StringLength(32)]
        public string TFlexId { get; set; }

        public TTFlex TFlex { get; set; }

        [StringLength(32)]
        public string TFlexIdentificationTypeId { get; set; }

        public TFlexIdentificationType TFlexIdentificationType { get; set; }

        [StringLength(32)]
        public string TFlexIdentificationDocTypeId { get; set; }

        public TFlexIdentificationDocType TFlexIdentificationDocType { get; set; }
        public ICollection<TTFlexIdentificationDoc> TFlexIdentificationDocs { get; set; }
        public bool? IsDeferred { get; set; }
        public DateTime? DeferredTillDate { get; set; }
        public bool? IsWavedOff { get; set; }

        [StringLength(500)]
        public string? Value { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(200)]
        public string? Remarks { get; set; }
    }
}