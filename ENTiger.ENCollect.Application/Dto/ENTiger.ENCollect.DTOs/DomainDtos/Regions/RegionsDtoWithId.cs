using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class RegionsDtoWithId : RegionsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}