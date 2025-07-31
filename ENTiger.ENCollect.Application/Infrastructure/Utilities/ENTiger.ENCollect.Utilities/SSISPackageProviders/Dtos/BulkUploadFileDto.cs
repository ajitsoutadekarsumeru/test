namespace ENTiger.ENCollect
{
    public class BulkUploadFileDto : DtoBridge
    {
        public string? FileName { get; set; }
        public string? CustomId { get; set; }
        public string? FileType { get; set; }
        public string? AllocationMethod { get; set; }
    }
}