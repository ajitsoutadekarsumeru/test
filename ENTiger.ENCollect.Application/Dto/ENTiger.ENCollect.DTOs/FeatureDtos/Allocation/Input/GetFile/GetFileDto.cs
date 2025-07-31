namespace ENTiger.ENCollect.AllocationModule
{
    public partial class GetFileDto : DtoBridge
    {
        public string TransactionId { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

    }
}