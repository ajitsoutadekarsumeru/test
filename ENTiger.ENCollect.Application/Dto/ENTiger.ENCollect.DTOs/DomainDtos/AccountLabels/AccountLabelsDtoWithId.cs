using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AccountLabelsDtoWithId : AccountLabelsDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}