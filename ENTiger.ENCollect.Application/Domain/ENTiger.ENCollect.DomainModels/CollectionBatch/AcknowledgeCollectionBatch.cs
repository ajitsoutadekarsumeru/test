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
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CollectionBatch AcknowledgeCollectionBatch(AcknowledgeCollectionBatchCommand cmd, string userid)
        {
            Guard.AgainstNull("CollectionBatch model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            this.CollectionBatchWorkflowState = _flexHost.GetFlexStateInstance<CollectionBatchAcknowledged>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}