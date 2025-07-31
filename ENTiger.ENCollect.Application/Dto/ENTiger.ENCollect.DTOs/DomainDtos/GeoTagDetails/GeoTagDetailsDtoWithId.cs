using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class GeoTagDetailsDtoWithId : GeoTagDetailsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}