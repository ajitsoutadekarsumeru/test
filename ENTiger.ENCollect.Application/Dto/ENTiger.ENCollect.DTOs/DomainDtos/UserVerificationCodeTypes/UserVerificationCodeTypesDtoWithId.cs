using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserVerificationCodeTypesDtoWithId : UserVerificationCodeTypesDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}