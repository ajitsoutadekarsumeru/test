using ENTiger.ENCollect.PublicModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class MasterFileStatus : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual MasterFileStatus ImportAccounts(ImportAccountsCommand cmd)
        {
            Guard.AgainstNull("MasterFileStatus command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.Status = FileStatusEnum.PayloadReceived.Value;
            this.FileName = UploadTypeEnum.ImportAccountByAPI.Value;
            this.UploadType = UploadTypeEnum.ImportAccountByAPI.Value;
            this.Description = FileStatusEnum.PayloadReceived.Value;
            this.FileUploadedDate = DateTime.Now;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"

        #region "Private Methods"

        public virtual MasterFileStatus UpdateImportAccounts(MasterFileStatus model)
        {
            model.LastModifiedDate = DateTime.Now;
            return model;
        }

        #endregion "Private Methods"
    }
}