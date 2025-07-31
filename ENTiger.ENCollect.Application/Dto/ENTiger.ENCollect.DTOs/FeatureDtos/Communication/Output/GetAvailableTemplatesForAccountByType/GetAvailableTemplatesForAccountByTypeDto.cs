using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAvailableTemplatesForAccountByTypeDto : DtoBridge
    {
        public string Id { get; set; }
        public string TemplateName { get; set; }
    }
}
