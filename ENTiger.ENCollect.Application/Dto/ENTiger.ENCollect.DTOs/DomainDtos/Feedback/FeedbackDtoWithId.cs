using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class FeedbackDtoWithId : FeedbackDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}