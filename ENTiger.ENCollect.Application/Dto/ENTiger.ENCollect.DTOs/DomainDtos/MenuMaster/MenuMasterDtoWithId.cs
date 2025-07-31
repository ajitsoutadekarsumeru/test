using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class MenuMasterDtoWithId : MenuMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}