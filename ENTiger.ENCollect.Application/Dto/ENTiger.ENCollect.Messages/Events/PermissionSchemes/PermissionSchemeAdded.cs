using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public class PermissionSchemeAdded : FlexEventBridge<FlexAppContextBridge>
    {
        public PermissionSchemeChangeLogDto PermissionSchemeChangeLog { get; set; }
    }



}
