using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserVerificationCodesDtoWithId : UserVerificationCodesDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}