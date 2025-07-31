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
        public virtual PayInSlip CreatePayInSlip(CreatePayInSlipCommand cmd)
        {
            Guard.AgainstNull("PayInSlip command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //this.SetAmount(cmd.Dto.Amount);
            //this.SetModeOfPayment(cmd.Dto.ModeOfPayment);
            this.DateOfDeposit = (DateTime.Now);
            //this.SetCMSPayInSlipNo(cmd.Dto.CMSPayInSlipNo);
            //this.SetBankName(cmd.Dto.BankName);
            //this.SetBranchName(cmd.Dto.BranchName);
            //this.SetBankAccountNo(cmd.Dto.BankAccountNo);
            //this.SetAccountHolderName(cmd.Dto.AccountHolderName);
            this.CustomId = cmd.CustomId;
            this.Lattitude = cmd.Dto.Latitude;
            this.PayInSlipImageName = cmd.Dto.DepositSlipImageName;

            this.SetAdded(cmd.Dto.GetGeneratedId());

            this.PayInSlipWorkflowState = _flexHost.GetFlexStateInstance<PayInSlipCreated>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");

            //Map any other field not handled by Automapper config

            //Set your appropriate SetAdded for the inner object here
            return this;
        }

        #endregion "Public Methods"
    }
}