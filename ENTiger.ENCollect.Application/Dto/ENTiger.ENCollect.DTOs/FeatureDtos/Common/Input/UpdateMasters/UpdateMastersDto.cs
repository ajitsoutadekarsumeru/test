using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class UpdateMastersDto : DtoBridge
    {
        [StringLength(32)]
        public string? Id { get; set; }

        public string? MasterName { get; set; }
        public bool IsDisable { get; set; }
    }
}