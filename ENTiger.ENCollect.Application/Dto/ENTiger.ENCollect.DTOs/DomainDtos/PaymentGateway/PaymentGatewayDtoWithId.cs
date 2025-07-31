using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayDtoWithId : PaymentGatewayDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}