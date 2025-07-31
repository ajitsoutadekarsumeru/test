using Sumeru.Flex;
using ENTiger.ENCollect.CollectionsModule;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CollectionUploadFile : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual CollectionUploadFile CollectionBulkUpload(CollectionBulkUploadCommand cmd)
        {
            Guard.AgainstNull("CollectionUploadFile command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.CustomId = cmd.Dto.CustomId;
            this.FileName = cmd.Dto.FileName;
            this.Status = FileStatusEnum.Uploaded.Value;
            this.FileUploadedDate = DateTime.Now;

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here



            return this;
        }
#endregion


        #region "Private Methods"
        #endregion

    }
}
