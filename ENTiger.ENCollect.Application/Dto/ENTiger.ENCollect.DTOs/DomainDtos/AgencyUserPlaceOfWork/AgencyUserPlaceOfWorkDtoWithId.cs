using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyUserPlaceOfWorkDtoWithId : AgencyUserPlaceOfWorkDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}