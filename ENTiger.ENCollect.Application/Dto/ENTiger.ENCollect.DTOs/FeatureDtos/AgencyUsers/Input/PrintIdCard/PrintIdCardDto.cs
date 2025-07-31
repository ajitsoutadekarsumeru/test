using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class PrintIdCardDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}