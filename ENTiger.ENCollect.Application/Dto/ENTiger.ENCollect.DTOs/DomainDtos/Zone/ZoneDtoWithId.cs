using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class ZoneDtoWithId : ZoneDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}