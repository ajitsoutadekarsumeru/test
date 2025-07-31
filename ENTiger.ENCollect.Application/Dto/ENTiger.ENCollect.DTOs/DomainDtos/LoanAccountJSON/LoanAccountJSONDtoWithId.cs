using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class LoanAccountJSONDtoWithId : LoanAccountJSONDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}