using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public abstract class TFlexIdentificationDoc<TTFlexIdentification, TTFlexIdentificationDoc, TTFlex> : DomainModelBridge
        where TTFlexIdentification : TFlexIdentification<TTFlexIdentification, TTFlexIdentificationDoc, TTFlex>, new()
        where TTFlexIdentificationDoc : TFlexIdentificationDoc<TTFlexIdentification, TTFlexIdentificationDoc, TTFlex>, new()
        where TTFlex : TFlex, new()
    {
        [StringLength(32)]
        public string? TFlexIdentificationId { get; set; }

        public TTFlexIdentification TFlexIdentification { get; set; }

        [StringLength(500)]
        public string? Path { get; set; }

        [StringLength(100)]
        public string? FileName { get; set; }

        public long? FileSize { get; set; }
    }
}