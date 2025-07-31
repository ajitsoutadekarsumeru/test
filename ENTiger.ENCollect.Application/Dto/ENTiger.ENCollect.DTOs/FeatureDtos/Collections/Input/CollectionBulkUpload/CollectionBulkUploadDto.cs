using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionBulkUploadDto : DtoBridge
    {
        public string? FileName { get; set; }

        public string? CustomId { get; set; }
    }

}
