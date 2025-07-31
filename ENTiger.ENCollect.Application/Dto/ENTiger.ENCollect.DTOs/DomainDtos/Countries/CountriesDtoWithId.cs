using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class CountriesDtoWithId : CountriesDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}