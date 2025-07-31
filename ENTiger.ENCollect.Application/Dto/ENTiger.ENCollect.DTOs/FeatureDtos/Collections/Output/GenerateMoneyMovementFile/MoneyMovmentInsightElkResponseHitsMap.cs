using CsvHelper.Configuration;

namespace ENTiger.ENCollect.CollectionsModule;


public sealed class MoneyMovementInsightElkResponseHitsMap : ClassMap<ENTiger.ENCollect.CollectionsModule.MoneyMovementInsightDownloadRoot.Hits>
{
    public MoneyMovementInsightElkResponseHitsMap()
    {


        Map(m => m._source.loanaccounts_productgroup).Index(1).Name("ProductGroup");
        Map(m => m._source.loanaccounts_product).Index(2).Name("Product");
        Map(m => m._source.loanaccounts_subproduct).Index(3).Name("SubProduct");
        Map(m => m._source.loanaccounts_customerid_customerid).Index(4).Name("AccountCustomerId");
        Map(m => m._source.loanaccounts_agreementid_agreementid).Index(5).Name("AccountAgreementNumber");
        Map(m => m._source.collections_customername_customername).Index(6).Name("CustomerName");
        Map(m => m._source.loanaccounts_branch_branchname).Index(7).Name("BranchName");

        Map(m => m._source.loanaccounts_branch_code_branchid).Index(8).Name("BranchCode");
        Map(m => m._source.loanaccounts_region_region).Index(9).Name("Region");

        Map(m => m._source.loanaccounts_state_state).Index(10).Name("State");
        Map(m => m._source.loanaccounts_city_city).Index(11).Name("City");
        Map(m => m._source.applicationuser_agent_agentname).Index(12).Name("AgentName");
        Map(m => m._source.applicationuser_customid_agentid).Index(13).Name("AgentCode");
        Map(m => m._source.collections_customid_receiptno).Index(14).Name("ReceiptNumber");
        Map(m => m._source.collections_physicalreceiptnumber_physicalreceiptno).Index(15).Name("PhysicalReceiptNumber");
        Map(m => m._source.collections_collectiondate_collectiondate).Index(16).Name("CollectionDate");
        Map(m => m._source.loanaccounts_current_bucket_currentbucket).Index(17).Name("CurrentBucket");
        Map(m => m._source.collections_yoverdueamount_emiamt).Index(18).Name("OverdueAmountEmiAmount");
        Map(m => m._source.collections_amountbreakup1_amountbreakup1).Name("AmountBreakupOne");
        Map(m => m._source.collections_yforeclosureamount_foreclosureamount).Name("ForeClosureAmount");
        Map(m => m._source.settlementamount).Name("SettlementAmount");
        Map(m => m._source.collections_ypenalinterest_latepaymentpenalty).Name("LatePaymentPenalty");
        Map(m => m._source.collections_othercharges_othercharges).Name("OtherCharges");
        Map(m => m._source.cheques_instrumentdate_instrumentdate).Name("InstrumentDate");
        Map(m => m._source.cheques_instrumentno_instrumentno).Name("InstrumentNumber");
        Map(m => m._source.collections_amount_instrumentamount).Name("InstrumentAmount");
        Map(m => m._source.cheques_micrcode_micrcode).Name("MicrCode");
        Map(m => m._source.collectionbatches_customid_batchid).Name("BatchId");
        Map(m => m._source.collectionbatches_createddate_batchidcreateddate).Name("BatchIdCreatedDate");
        Map(m => m._source.payinslips_createddate_depositdate).Name("DepositDate");
        Map(m => m._source.collectionbatches_amount_batchamount).Name("BatchAmount");

        Map(m => m._source.paymentstatus).Name("PaymentStatus");
        Map(m => m._source.loanaccounts_bucket_bombucket).Name("BomBucket");
        Map(m => m._source.loanaccounts_npa_stageid_npa_stageid).Name("NPAStageId");
        Map(m => m._source.loanaccounts_latestlatitude_lat).Name("LatestLatitude");
        Map(m => m._source.loanaccounts_latestlongitude_long).Name("LatestLongitude");
        Map(m => m._source.loanaccounts_primary_card_number_primary_card_number).Name("PrimaryCardNumber");
        Map(m => m._source.applicationuser_primaryemail_agentemail).Name("AgentEmail");
        Map(m => m._source.paymenttowards).Name("PaymentTowards");
        Map(m => m._source.collections_ybouncecharges_bouncecharges).Name("BounceCharges");
        Map(m => m._source.excess).Name("Excess");
        Map(m => m._source.imd).Name("Imd");
        Map(m => m._source.procfee).Name("ProcFee");
        Map(m => m._source.swap).Name("Swap");
        Map(m => m._source.ebccharge).Name("EbcCharge");
        Map(m => m._source.collectionpickupcharge).Name("CollectionPickupCharge");
        Map(m => m._source.payinslips_encollectpayinslipid).Name("EncollectPayInSlipId");
        Map(m => m._source.payinslips_cmspayinslipno_cmspayinslipid).Name("CmsPayinSlipNumber");
        Map(m => m._source.payinslips_bankaccountno_depositaccountnumber).Name("DepositAccountNumber");
        Map(m => m._source.payinslips_bankname_depositebankname).Name("DepositBankName");

        Map(m => m._source.payinslips_amount_depositamount).Name("DepositAmount");
        Map(m => m._source.merchantreferencenumber).Name("MerchantReferenceNumber");
        Map(m => m._source.banktransactionid).Name("BankTransactionId");
        Map(m => m._source.bankid).Name("DepositBankName");
        Map(m => m._source.payinslips_bankname_depositebankname).Name("BankId");
        Map(m => m._source.collections_amount_amount).Name("CollectionsAmount");
        Map(m => m._source.statuscode).Name("StatusCode");
        Map(m => m._source.collections_createddate_receiptdate).Name("CreatedDate");
        Map(m => m._source.rrn).Name("Rrn"); 
        Map(m => m._source.cardholdername).Name("CardHolderName");
        Map(m => m._source.merchantid).Name("MerchantId");
        Map(m => m._source.merchanttransactionid).Name("MerchantTransactionId");
        Map(m => m._source.applicationorg_agency_agencyname).Name("AgencyName");
        Map(m => m._source.applicationorg_customid_agencyid).Name("AgencyCode");
        Map(m => m._source.applicationuser_stafforagent).Name("StaffOrAgent");  

    }
}




