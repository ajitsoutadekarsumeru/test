using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FileTypeEnum : FlexEnum
    {
        private FileTypeEnum()
        { }

        private FileTypeEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly FileTypeEnum CSV = new FileTypeEnum(".csv", "CSV");

        public static readonly FileTypeEnum XLS = new FileTypeEnum(".xls", "XLS");

        public static readonly FileTypeEnum XLSX = new FileTypeEnum(".xlsx", "XLSX");

        public static readonly FileTypeEnum PDF = new FileTypeEnum(".pdf", "PDF");

        public static readonly FileTypeEnum DOC = new FileTypeEnum(".doc", "DOC");

        public static readonly FileTypeEnum DOCX = new FileTypeEnum(".docx", "DOCX");

        public static readonly FileTypeEnum JPG = new FileTypeEnum(".jpg", "JPG");
        public static readonly FileTypeEnum JPEG = new FileTypeEnum(".jpeg", "JPEG");
        public static readonly FileTypeEnum PNG = new FileTypeEnum(".png", "PNG");
    }
}
