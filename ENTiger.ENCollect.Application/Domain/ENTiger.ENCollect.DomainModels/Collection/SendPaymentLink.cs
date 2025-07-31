using ENTiger.ENCollect.CollectionsModule;
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
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual Collection SendPaymentLink(SendPaymentLinkCommand cmd, string accountId, string name)
        {
            Guard.AgainstNull("Collection command cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.CustomerName = name;
            this.CollectionMode = CollectionModeEnum.Online.Value;
            this.AccountId = accountId;
            this.CollectorId = this.CreatedBy;
            this.AckingAgentId = this.CreatedBy;
            this.AcknowledgedDate = DateTime.Now;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<CollectionInitiated>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");

            //Set your appropriate SetAdded for the inner object here
            this.Cash?.SetAddedOrModified();
            this.Cheque?.SetAddedOrModified();
            return this;
        }

        #endregion "Public Methods"
    }
}