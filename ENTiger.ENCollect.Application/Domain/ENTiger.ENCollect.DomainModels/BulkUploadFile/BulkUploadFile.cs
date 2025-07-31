using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.DomainModels.BulkUploadFile
{
    public class BulkUploadFile : DomainModelBridge
    {
        #region constructor

        public BulkUploadFile()
        {
        }

        public BulkUploadFile(string fileName, string createdBy, string customId, string md5Hash, string fileType, string allocationType)
        {
            FileName = fileName;
            this.CreatedBy = createdBy;
            CustomId = customId;
            MD5Hash = md5Hash;
            this.CreatedDate = DateTime.Now;
            Status = "Started";
            FileType = fileType;
            AllocationType = allocationType;
        }

        #endregion constructor

        #region Attributes

        [StringLength(50)]
        public string CustomId { get; set; }

        [StringLength(200)]
        public string Description { get; private set; }

        [StringLength(250)]
        public string FileName { get; set; }

        [StringLength(250)]
        public string FilePath { get; set; }

        public DateTime FileUploadedDate { get; set; }
        public DateTime FileProcessedDateTime { get; set; }

        [StringLength(200)]
        public string Status { get; set; }

        [StringLength(250)]
        public string StatusFileName { get; set; }

        [StringLength(250)]
        public string StatusFilePath { get; set; }

        [StringLength(200)]
        public string MD5Hash { get; private set; }

        public bool? IsUploadstatus { get; private set; }
        public int? RowsError { get; set; }
        public int? RowsProcessed { get; set; }
        public int? RowsSuccess { get; set; }

        [StringLength(200)]
        public string FileType { get; set; }

        [StringLength(200)]
        public string AllocationType { get; set; }

        # endregion

        #region Public

        public void UpdateStatus(string status)
        {
            Status = status;
        }

        public void ProcessResult(int? rowsProcessed, int? rowsSuccess, int? rowsError)
        {
            RowsProcessed = rowsProcessed;
            RowsSuccess = rowsSuccess;
            RowsError = rowsError;
            Status = "Processed";
        }

        public void SetDestinationFile(string destinationFilePath, string destinationFileName)
        {
            StatusFilePath = destinationFilePath;
            StatusFileName = destinationFileName;
        }

        # endregion
    }
}