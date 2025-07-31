using Sumeru.Flex;
using ENTiger.ENCollect.SettlementModule;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Settlement : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual Settlement RequestSettlement(RequestSettlementCommand cmd)
        {
            Guard.AgainstNull("Settlement command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CustomId = cmd.CustomId;
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
             

            // map WaiverDetails, Installments, Documents
            foreach (var waiver in cmd.Dto.WaiverDetails)
                this.AddWaiverDetail(waiver.ChargeType, waiver.AmountAsPerCBS, waiver.ApportionmentAmount, waiver.WaiverAmount);

            foreach (var i in cmd.Dto.Installments)
                this.AddInstallment(i.InstallmentAmount, i.InstallmentDueDate);

            foreach (var document in cmd.Dto.Documents)
                this.AddDocument(document.DocumentType, document.DocumentName, document.FileName);
           
            this.AddStatus(SettlementStatusEnum.UnderEvaluation.Value, cmd.Dto.GetAppContext()?.UserId, this.SettlementRemarks);
            this.SettlementDate = DateTime.Now;

            this.SetAdded(cmd.Dto.GetGeneratedId());

            return this;
        }
#endregion


        #region "Private Methods"
        #endregion

    }
}
