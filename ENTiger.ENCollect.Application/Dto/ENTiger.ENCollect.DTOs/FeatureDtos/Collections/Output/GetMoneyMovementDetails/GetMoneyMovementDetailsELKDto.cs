using System;
using System.Text.Json.Serialization;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class GetMoneyMovementDetailsELKDto
    {
        public string? loanaccounts_productgroup { get; set; }
        public string? loanaccounts_product { get; set; }
        public string? loanaccounts_subproduct { get; set; }
        public string? loanaccounts_customerid_customerid { get; set; }
        public string? loanaccounts_agreementid_agreementid  { get; set; }
        public string? collections_customername_customername  { get; set; }
        public string? loanaccounts_branch_branchname  { get; set; }
        public string? loanaccounts_branch_code_branchid { get; set; }
        public string? loanaccounts_region_region { get; set; }
        public string? loanaccounts_state_state { get; set; }
        public string? loanaccounts_city_city { get; set; }
        public string? applicationuser_agent_agentname { get; set; }
        public string? applicationuser_customid_agentid { get; set; }
        public string? collections_customid_receiptno { get; set; }
        public string? collections_physicalreceiptnumber_physicalreceiptno { get; set; }
        public string? collections_collectiondate_collectiondate { get; set; }
        public string? loanaccounts_current_bucket_currentbucket  { get; set; }
        public string? collections_yoverdueamount_emiamt { get; set; }
        public string? collections_amountbreakup1_amountbreakup1 { get; set; } 
        public string? collections_yforeclosureamount_foreclosureamount  { get; set; }

       
        public string? settlementamount { get; set; }

        public string? collections_ypenalinterest_latepaymentpenalty { get; set; }
        public string? collections_othercharges_othercharges { get; set; }

        public decimal? collections_amount_totalreceiptamount { get; set; }

        public string? collections_collectionmode_paymentmode { get; set; }

        public string? cheques_instrumentdate_instrumentdate  { get; set; }
        public string? cheques_instrumentno_instrumentno { get; set; }
        public decimal? collections_amount_instrumentamount { get; set; }
        public string? cheques_micrcode_micrcode { get; set; }
        public string? collectionbatches_customid_batchid { get; set; }
        public string? collectionbatches_createddate_batchidcreateddate { get; set; }
        public string? payinslips_createddate_depositdate { get; set; }
        public decimal? collectionbatches_amount_batchamount { get; set; }
        public string? paymentstatus { get; set; }
        public int? loanaccounts_bucket_bombucket { get; set; }
        public string? loanaccounts_npa_stageid_npa_stageid { get; set; }
        public string? loanaccounts_latestlatitude_lat { get; set; }
        public string? loanaccounts_latestlongitude_long { get; set; }
        public string? loanaccounts_primary_card_number_primary_card_number { get; set; }
        public string? applicationuser_primaryemail_agentemail { get; set; }
        public string? paymenttowards { get; set; }
        public string? collections_ybouncecharges_bouncecharges { get; set; }
        public string? excess { get; set; }
        public string? imd { get; set; }
        public string? procfee { get; set; }
        public string? swap { get; set; }
        public string? ebccharge { get; set; }
        public string? collectionpickupcharge { get; set; }
        public string? payinslips_encollectpayinslipid { get; set; }
        public string? payinslips_cmspayinslipno_cmspayinslipid { get; set; }
        public string? payinslips_bankaccountno_depositaccountnumber { get; set; }
        public string? payinslips_bankname_depositebankname { get; set; }
        public decimal? payinslips_amount_depositamount { get; set; } 
        public string? merchantreferencenumber { get; set; }
        public string? banktransactionid { get; set; }
        public string? bankid { get; set; }
        public decimal? collections_amount_amount { get; set; } 
        public string? statuscode { get; set; }
        public string? collections_createddate_receiptdate { get; set; }
        public string? rrn { get; set; }
        public string? cardholdername { get; set; }
        public string? merchantid { get; set; }
        public string? merchanttransactionid { get; set; }
        public string? applicationorg_agency_agencyname { get; set; }

        public string? applicationorg_customid_agencyid { get; set; }
        public string? applicationuser_stafforagent { get; set; }

    }
}
