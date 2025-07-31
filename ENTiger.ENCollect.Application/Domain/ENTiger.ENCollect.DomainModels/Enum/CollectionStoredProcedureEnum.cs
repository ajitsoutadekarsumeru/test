using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CollectionStoredProcedureEnum : FlexEnum
    {
        public CollectionStoredProcedureEnum()
        { }

        public CollectionStoredProcedureEnum(string value, string displayName) : base(value, displayName)
        {
        }

        public static readonly CollectionStoredProcedureEnum IntermediateTable = new CollectionStoredProcedureEnum("CollectionBulkUploadIntermediateTable", "CollectionBulkUploadIntermediateTable");

        public static readonly CollectionStoredProcedureEnum Validation = new CollectionStoredProcedureEnum("CollectionBulkUploadValidations", "CollectionBulkUploadValidations");
        public static readonly CollectionStoredProcedureEnum Insert = new CollectionStoredProcedureEnum("CollectionBulkInsert", "CollectionBulkInsert");
        public static readonly CollectionStoredProcedureEnum UpdateFileStatus = new CollectionStoredProcedureEnum("CollectionBulkUploadFileStatus", "CollectionBulkUploadFileStatus");
        public static readonly CollectionStoredProcedureEnum RecordsCleanUp = new CollectionStoredProcedureEnum("CollectionBulkUploadRecordsCleanUp", "CollectionBulkUploadRecordsCleanUp");

        public static readonly CollectionStoredProcedureEnum UploadDetails = new CollectionStoredProcedureEnum("GetCollectionBulkUploadDetails", "GetCollectionBulkUploadDetails");

    }
}