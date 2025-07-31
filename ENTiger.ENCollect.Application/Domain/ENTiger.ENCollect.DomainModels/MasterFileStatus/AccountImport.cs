using ENTiger.ENCollect.AccountsModule;
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
        public virtual MasterFileStatus AccountImport(AccountImportCommand cmd, string path)
        {
            Guard.AgainstNull("MasterFileStatus command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.FileUploadedDate = DateTime.Now;
            this.CustomId = cmd.Dto.customid;
            this.Status = FileStatusEnum.Uploaded.Value;
            this.FileName = cmd.Dto.filename;
            this.FilePath = path;
            this.UploadType = UploadTypeEnum.ImportAccountByFile.Value;
            this.Description = FileStatusEnum.Uploaded.Value;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}