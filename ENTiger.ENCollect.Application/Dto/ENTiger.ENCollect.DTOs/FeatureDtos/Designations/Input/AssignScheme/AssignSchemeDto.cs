using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class AssignSchemeDto : DtoBridge
    {       
        public string Id { get; set; }

        public string PermissionSchemeId { get; set; }
    }

}
