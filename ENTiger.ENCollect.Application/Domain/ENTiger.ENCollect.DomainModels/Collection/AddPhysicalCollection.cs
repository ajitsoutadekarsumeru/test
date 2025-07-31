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
        public virtual Collection AddPhysicalCollection(AddPhysicalCollectionCommand cmd, LoanAccount account)
        {
            Guard.AgainstNull("Collection command cannot be empty", cmd);

            foreach (var uploadRF in cmd.Dto.CollectionDocs)
            {
                this.ChangeRequestImageName = uploadRF.FileName;
            }
            string userid = cmd.Dto.GetAppContext()?.UserId;
            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.CollectionDate = DateTime.Now;

            //Map any other field not handled by Automapper config
            this.SetAdded(cmd.Dto.GetGeneratedId());

            this.AccountId = account.Id;
            //this.CollectionOrgId = userid;
            this.CollectorId = userid;
            this.AckingAgentId = userid;
            this.AcknowledgedDate = DateTime.Now;
            this.Cheque.SetAdded();
            if (this.Cash != null)
            {
                this.Cash.SetAdded();
            }

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}