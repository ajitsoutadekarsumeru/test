using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountLabelsDto : DtoBridge
    {
        public List<UpdateAccountLabels> Labels { get; set; }
    }

    public partial class UpdateAccountLabels
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Label { get; set; }
    }
}