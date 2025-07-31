using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DeviceDetailDtoWithId : DeviceDetailDto
    {
        [StringLength(32)]
        public string? Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}