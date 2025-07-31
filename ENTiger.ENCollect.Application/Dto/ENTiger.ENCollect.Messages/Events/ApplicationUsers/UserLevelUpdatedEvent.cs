using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class UserDesignationChangedEvent : FlexEventBridge<FlexAppContextBridge>
    { 
        /// <summary>
        /// The user whose level has changed.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        ///  all designations.
        /// </summary>
        public List<string> DesignationIds { get; set; } 
    }

    
}
