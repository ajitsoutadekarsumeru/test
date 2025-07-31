using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class FileStatusEnum : FlexEnum
    {
        private FileStatusEnum()
        { }

        private FileStatusEnum(string value, string displayName) : base(value, displayName)
        {
        }
        public static readonly FileStatusEnum PayloadReceived = new FileStatusEnum("Payload Received", "Payload Received");

        public static readonly FileStatusEnum Uploaded = new FileStatusEnum("Uploaded", "Uploaded");
        public static readonly FileStatusEnum Processing = new FileStatusEnum("Processing", "Processing");

        public static readonly FileStatusEnum Processed = new FileStatusEnum("Processed", "Processed");

        public static readonly FileStatusEnum Failed = new FileStatusEnum("Failed", "Failed");

        public static readonly FileStatusEnum Partial = new FileStatusEnum("Partially Processed", "Partially Processed");

        public static readonly FileStatusEnum InvalidFileFormat = new FileStatusEnum("Invalid File Format", "Invalid File Format");

        public static readonly FileStatusEnum Error = new FileStatusEnum("Error", "Error");
    }
}