using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents settings related to device validation for the mobile app.
    /// </summary>
    public class DeviceValidationSettings
    {
        /// <summary>
        /// Whether to validate the registration of the device based on the mobile number.
        /// </summary>
        public bool EnableRegisterDeviceBasedOnMobileNo { get; set; }
    }

}
