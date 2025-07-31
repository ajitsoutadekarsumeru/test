using ENTiger.ENCollect.AllocationModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationFile : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual SecondaryAllocationFile SecondaryAllocationByBatch(SecondaryAllocationByBatchCommand cmd, string path)
        {
            Guard.AgainstNull("SecondaryAllocationFile command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.CustomId = cmd.Dto.Customid;
            this.UploadType = cmd.Dto.FileType;
            this.FileName = cmd.Dto.AllocationFileName;
            this.Description = cmd.Dto.AllocationMethod;
            this.Status= FileStatusEnum.Uploaded.Value;
            this.FileUploadedDate = DateTime.Now;
            this.FilePath = path;

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}