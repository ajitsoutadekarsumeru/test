using ENTiger.ENCollect.CommonModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UsersCreateFile : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual UsersCreateFile CreateUsersByBatch(CreateUsersByBatchCommand cmd)
        {
            Guard.AgainstNull("UsersCreateFile command cannot be empty", cmd);

            this.Convert(cmd.Dto);

            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //this.CreatedBy = "systemadmin";
            //this.LastModifiedBy = "systemadmin";

            this.CustomId = cmd.CustomId;
            this.UploadType = cmd.Dto.Type;
            this.FileName = cmd.Dto.Name;
            this.FilePath = cmd.Dto.Name;
            this.Description = "Users Creation Dump";
            this.UpdateStatus(FileStatusEnum.Uploaded.Value);
            this.UploadedDate = DateTime.Now;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here
            return this;
        }

        #endregion "Public Methods"
    }
}