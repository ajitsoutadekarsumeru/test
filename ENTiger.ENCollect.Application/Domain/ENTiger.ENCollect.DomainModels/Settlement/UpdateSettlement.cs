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
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Settlement UpdateSettlement(UpdateSettlementCommand cmd)
        {
            Guard.AgainstNull("Settlement model cannot be empty", cmd);
            UpdateSettlementDto dto = cmd.Dto as UpdateSettlementDto;

            this.SettlementAmount = dto.SettlementAmount;
            this.RenegotiationAmount = dto.RenegotiateAmount;
            this.SettlementRemarks = dto.SettlementRemarks;
            this.LastModifiedBy = dto.GetAppContext().UserId;
            this.ChangeStatus(SettlementStatusEnum.Updated.Value, LastModifiedBy);
            this.SetModified();


            //Edit existing waivers and add new ones
            UpdateWaiverDetails(dto);

            //Edit existing installments and add new ones
            UpdateInstallmentDetails(dto);


            //Edit existing Documents and add new ones
            UpdateDocumentDetails(dto);

           
            return this;
        }

        private void UpdateDocumentDetails(UpdateSettlementDto dto)
        {
            foreach (var document in this.Documents)
            {
                var documentDto = dto.Documents.FirstOrDefault(d => d.DocumentType == document.DocumentType);
                if (documentDto != null)
                {
                    document.Update(documentDto);
                }
                else
                {
                    //Add new document if it doesn't exist
                    this.AddDocument(document.DocumentType, document.DocumentName, document.FileName);
                }
            }
        }

        private void UpdateInstallmentDetails(UpdateSettlementDto dto)
        {
            foreach (var installment in this.Installments)
            {
                var installmentDto = dto.Installments.FirstOrDefault(i => i.Id == installment.Id);
                if (installmentDto != null)
                {
                    installment.Update(installmentDto);
                }
                else
                {
                    //Add new installment if it doesn't exist
                    this.AddInstallment(installment.InstallmentAmount, installment.InstallmentDueDate);
                }
            }
        }

        private void UpdateWaiverDetails(UpdateSettlementDto dto)
        {
            foreach (var waiverDetail in this.WaiverDetails)
            {
                var waiverDto = dto.WaiverDetails.FirstOrDefault(w => w.ChargeType == waiverDetail.ChargeType);
                if (waiverDto != null)
                {
                    waiverDetail.Update(waiverDto);
                }
                else
                {
                    //Add new waiver detail if it doesn't exist
                    this.AddWaiverDetail(waiverDetail.ChargeType, waiverDetail.AmountAsPerCBS, waiverDetail.ApportionmentAmount, waiverDetail.WaiverAmount);
                }
            }
        }
        #endregion

        #region "Private Methods"
        #endregion

    }
}
