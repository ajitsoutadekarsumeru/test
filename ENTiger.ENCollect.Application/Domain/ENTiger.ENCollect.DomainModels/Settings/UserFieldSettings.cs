using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents user field settings for managing field updates.
    /// </summary>
    public class UserFieldSettings
    {
        /// <summary>
        /// Indicates whether disabled fields can be updated.
        /// </summary>
        public bool EnableUpdateMobileNo { get; set; } = false;

        /// <summary>
        /// Indicates whether disabled fields can be updated.
        /// </summary>
        public bool EnableUpdateEmail { get; set; } = false;
    }
}
