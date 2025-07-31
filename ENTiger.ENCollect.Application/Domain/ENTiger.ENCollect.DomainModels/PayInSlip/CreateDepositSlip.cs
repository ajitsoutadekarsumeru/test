using ENTiger.ENCollect.PayInSlipsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PayInSlip : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual PayInSlip CreateDepositSlip(CreateDepositSlipCommand cmd)
        {
            Guard.AgainstNull("PayInSlip command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //this.Amount = (cmd.Dto.Amount);
            //this.ModeOfPayment = (cmd.Dto.ModeOfPayment);
            this.DateOfDeposit = (DateTime.Now);
            //this.CMSPayInSlipNo = (cmd.Dto.CMSPayInSlipNo);
            //this.SetBankName(cmd.Dto.BankName);
            //this.SetBranchName(cmd.Dto.BranchName);
            //this.SetBankAccountNo(cmd.Dto.BankAccountNo);
            //this.SetAccountHolderName(cmd.Dto.AccountHolderName);
            this.CustomId = cmd.CustomId;
            this.SetAdded(cmd.Dto.GetGeneratedId());
            //need to remove Lattitude param from dto once new app apk is released for all clients
            this.Lattitude = string.IsNullOrEmpty(cmd.Dto.Lattitude) ? cmd.Dto.Latitude : this.Lattitude;

            this.PayInSlipWorkflowState = _flexHost.GetFlexStateInstance<PayInSlipCreated>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");
            //Map any other field not handled by Automapper config

            //Set your appropriate SetAdded for the inner object here
            return this;
        }

        #endregion "Public Methods"
    }
}