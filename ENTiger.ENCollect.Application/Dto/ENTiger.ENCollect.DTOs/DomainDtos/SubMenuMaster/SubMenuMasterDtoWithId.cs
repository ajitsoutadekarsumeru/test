using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SubMenuMasterDtoWithId : SubMenuMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}