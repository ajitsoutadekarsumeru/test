using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class RegisterDeviceDto : DtoBridge
    {
        [Required]
        public string Email { get; set; }

        public string? IMEI { get; set; }
        public string? MobileNumberSim1 { get; set; }
        public string? MobileNumberSim2 { get; set; }
        public string? ReferenceId { get; set; }
    }
}