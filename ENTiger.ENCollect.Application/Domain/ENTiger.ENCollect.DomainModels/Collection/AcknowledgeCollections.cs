using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Collection : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Collection AcknowledgeCollections(string UserId)
        {
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<CollectionAcknowledged>().SetStateChangedBy(UserId).SetTFlexId(this.Id);
            this.SetAckingAgent(UserId);
            this.AcknowledgedDate = (DateTime.Now);
            this.LastModifiedBy = UserId;
            this.Status = CollectionStatusEnum.withAgency_Or_Branch.Value;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}