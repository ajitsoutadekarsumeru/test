using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public class PermissionSchemeUpdated : FlexEventBridge<FlexAppContextBridge>
    {
        public PermissionSchemeChangeLogDto PermissionSchemeChangeLog { get; set; }
    }

    
}
