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
        public virtual Collection AddCollection(AddCollectionCommand cmd)
        {
            Guard.AgainstNull("Collection command cannot be empty", cmd);
            string userid = cmd.Dto.GetAppContext()?.UserId;
            var model = cmd.Dto;

            this.Convert(cmd.Dto);

            if (cmd.Dto.CollectionDocs != null)
            {
                foreach (var uploadRF in cmd.Dto.CollectionDocs)
                {
                    this.ChangeRequestImageName = uploadRF.FileName;
                }
            }

            this.CustomId = model.ReceiptNo;
            this.ReceiptId = cmd.ReceiptId;
            this.CustomerName = model?.CustomerName;
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.SetAdded(cmd.Dto.GetGeneratedId());

            if (string.Equals(model?.CollectionMode,CollectionModeEnum.EncerdibleOnline.Value, StringComparison.OrdinalIgnoreCase))
            {
                this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<CollectionInitiated>().SetTFlexId(this.Id).SetStateChangedBy(userid);//FlexOpus.GetFlexStateInstance<CollectionInitiated>();
            }
            if (string.Equals(model?.CollectionMode, CollectionModeEnum.Wallet.Value, StringComparison.OrdinalIgnoreCase))
            {
                this.MarkAsSuccess(userid);
            }
            else
            {
                this.SetInitialCollectionState(userid);
            }

            this.Cash?.SetAdded();
            this.Cheque?.SetAdded();
            this.Status = CollectionStatusEnum.withAgent.Value;

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}