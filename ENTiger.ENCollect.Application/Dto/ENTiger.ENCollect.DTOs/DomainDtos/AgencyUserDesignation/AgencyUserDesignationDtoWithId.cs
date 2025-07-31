using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyUserDesignationDtoWithId : AgencyUserDesignationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}