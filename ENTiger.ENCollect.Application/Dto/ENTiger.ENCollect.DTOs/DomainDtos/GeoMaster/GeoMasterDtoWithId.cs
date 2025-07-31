using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class GeoMasterDtoWithId : GeoMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}