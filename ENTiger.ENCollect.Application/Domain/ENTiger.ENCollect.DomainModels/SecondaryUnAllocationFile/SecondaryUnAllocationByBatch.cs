using ENTiger.ENCollect.AllocationModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryUnAllocationFile : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual SecondaryUnAllocationFile SecondaryUnAllocationByBatch(SecondaryUnAllocationByBatchCommand cmd)
        {
            Guard.AgainstNull("SecondaryUnAllocationFile command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.CustomId = cmd.CustomId;
            this.UploadType = cmd.Dto.Type;
            this.FileName = cmd.Dto.Name;
            this.FilePath = cmd.Dto.Name;
            this.Description = cmd.Dto.UnAllocationType != null ? cmd.Dto.UnAllocationType : "Account Level";
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