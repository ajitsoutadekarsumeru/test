using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents password settings related to expiry and notification.
    /// </summary>
    public class PasswordSettings
    {
        /// <summary>
        /// The number of days before password expiry to send a notification to the user.
        /// </summary>
        public int ExpiryNotificationInDays { get; set; } = 5;

        /// <summary>
        /// The number of days after which the password will expire.
        /// </summary>
        public int ExpiryInDays { get; set; } = 90;
    }
}
