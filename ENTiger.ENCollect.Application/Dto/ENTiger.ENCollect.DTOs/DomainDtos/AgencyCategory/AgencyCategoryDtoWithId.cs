using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyCategoryDtoWithId : AgencyCategoryDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}