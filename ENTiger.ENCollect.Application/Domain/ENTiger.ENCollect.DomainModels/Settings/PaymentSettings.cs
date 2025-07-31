using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents payment settings related to processing payments.
    /// </summary>
    public class PaymentSettings
    {
        /// <summary>
        /// The Static URL for processing payments (e.g., payment gateway URL).
        /// </summary>
        public string StaticPaymentLink { get; set; } = "";
    }
}
