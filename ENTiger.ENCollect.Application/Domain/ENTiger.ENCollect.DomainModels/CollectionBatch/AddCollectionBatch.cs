using ENTiger.ENCollect.CollectionBatchesModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CollectionBatch : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual CollectionBatch AddCollectionBatch(AddCollectionBatchCommand cmd)
        {
            Guard.AgainstNull("CollectionBatch command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.SetAdded();
            this.CustomId = cmd.CustomId;
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}