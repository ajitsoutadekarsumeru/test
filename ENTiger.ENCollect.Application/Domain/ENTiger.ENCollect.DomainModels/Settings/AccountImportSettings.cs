using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.DomainModels.Settings
{
    /// <summary>
    /// Represents account import settings for controlling import behavior.
    /// </summary>
    public class AccountImportSettings
    {
        /// <summary>
        /// The maximum number of accounts that can be imported at once.
        /// </summary>
        public int MaximumCount { get; set; } = 1000;
    }
}
