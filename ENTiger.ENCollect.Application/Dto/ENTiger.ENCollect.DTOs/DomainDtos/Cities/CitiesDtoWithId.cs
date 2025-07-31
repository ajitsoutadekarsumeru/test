using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CitiesDtoWithId : CitiesDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}