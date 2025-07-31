namespace ENTiger.ENCollect.CollectionsModule;

/// <summary>
/// Retrieves paged money movement detail records by constructing an Elasticsearch query with filters and parsing the response.
/// </summary>
public class GetMoneyMovementDetails : FlexiQueryPagedListBridgeAsync<GetMoneyMovementDetailsParams, GetMoneyMovementDetailsDto, FlexAppContextBridge>
{
    protected readonly ILogger<GetMoneyMovementDetails> _logger;
    protected GetMoneyMovementDetailsParams _params;

    protected readonly RepoFactory _repoFactory;
    protected FlexAppContextBridge? _flexAppContext;
    public IElasticSearchService _elasticSearchService { get; set; }
    private readonly ElasticSearchIndexSettings _elasticIndexConfig;
    public string NoFilterPresent = "NoFilterPresent";

    IEnumerable<GetMoneyMovementDetailsDto> queryResult = new List<GetMoneyMovementDetailsDto>();
    /// <summary>
    /// Initializes a new instance of the <see cref="GetMoneyMovementDetails"/> class.
    /// </summary>
    /// <param name="logger"></param>
    public GetMoneyMovementDetails(ILogger<GetMoneyMovementDetails> logger, IElasticSearchService elasticSearchService, RepoFactory repoFactory, IOptions<ElasticSearchIndexSettings> ElasticSearchIndexSettings)
    {
        Guard.AgainstNull(nameof(logger), logger);
        Guard.AgainstNull(nameof(repoFactory), repoFactory);
        Guard.AgainstNull(nameof(elasticSearchService), elasticSearchService);

        _logger = logger;
        _repoFactory = repoFactory;
        _elasticSearchService = elasticSearchService;
        _elasticIndexConfig = ElasticSearchIndexSettings.Value;
    }
    /// <summary>
    /// Assigns query parameters from external input.
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public virtual GetMoneyMovementDetails AssignParameters(GetMoneyMovementDetailsParams @params)
    {
        _params = @params;
        return this;
    }

    /// <summary>
    /// Main entry point to fetch filtered data from Elasticsearch and return a paginated result.
    /// </summary>
    /// <returns></returns>
    public override async Task<FlexiPagedList<GetMoneyMovementDetailsDto>> Fetch()
    {
        IEnumerable<GetMoneyMovementDetailsDto> queryResult = null;

        _flexAppContext = _params.GetAppContext();

        Guard.AgainstNullAndEmpty("_flexAppContext?.TenantId", _flexAppContext?.TenantId);
        _repoFactory.Init(_params);

        var config = _elasticIndexConfig.IndexNames.Where(a => a.TenantId == _flexAppContext?.TenantId).FirstOrDefault();
        Guard.AgainstNull(nameof(config), config);

        string path = config.MoneyMovementInsightIndex;

        //Build DSL Query
        string dslquery = BuildDslQueryForMoneyMovementInsighDetail(_params);

        //Send request to elastic search
        var response = await _elasticSearchService.SendPostRequestToElasticSearchAsync(path, dslquery);
        Guard.AgainstNull(nameof(response), response);

        // Parse response
        var output = ParseResponse(response);

        return output;
    }

    /// <summary>
    /// Parses the raw object response into a strongly typed FlexiPagedList.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetMoneyMovementDetailsDto> ParseResponse(object response)
    {
        string elasticresponse = response.ToString();

        Guard.AgainstNullAndEmpty(nameof(elasticresponse), elasticresponse);
        //Parse the response
        var result = ParseResponseWithJsonDocument(elasticresponse);

        return result;

    }

    /// <summary>
    /// Parses the response JSON using JsonDocument and maps each hit to DTO.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private FlexiPagedList<GetMoneyMovementDetailsDto> ParseResponseWithJsonDocument(string response)
    {
        using (JsonDocument doc = JsonDocument.Parse(response))
        {
            // Extract total count from the parsed JSON
            int TotalCount = doc.RootElement.GetProperty("hits")
                                            .GetProperty("total")
                                            .GetProperty("value")
                                            .GetInt32();

            // List to hold the final output
            List<GetMoneyMovementDetailsDto> detailoutput = new List<GetMoneyMovementDetailsDto>();

            // Iterate over each hit in the "hits" array
            foreach (JsonElement jsonproperties in doc.RootElement.GetProperty("hits").GetProperty("hits").EnumerateArray())
            {
                try
                {
                    // Deserialize the "_source" field into GetPrimaryAllocationDetailsELKDto
                    GetMoneyMovementDetailsELKDto outputModelForELK = JsonSerializer.Deserialize<GetMoneyMovementDetailsELKDto>(jsonproperties.GetProperty("_source").GetRawText());

                    // Create and populate the GetPrimaryAllocationDetailsDto object
                    GetMoneyMovementDetailsDto obj = new GetMoneyMovementDetailsDto
                    {
                        ProductGroup = outputModelForELK.loanaccounts_productgroup,
                        Product = outputModelForELK.loanaccounts_product,
                        SubProduct = outputModelForELK.loanaccounts_subproduct,
                        AccountCustomerId = outputModelForELK.loanaccounts_customerid_customerid,
                        AccountAgreementNumber = outputModelForELK.loanaccounts_agreementid_agreementid,
                        CustomerName = outputModelForELK.collections_customername_customername,
                        BranchName = outputModelForELK.loanaccounts_branch_branchname,
                        BranchCode = outputModelForELK.loanaccounts_branch_code_branchid,
                        Region = outputModelForELK.loanaccounts_region_region,
                        State = outputModelForELK.loanaccounts_state_state,
                        City = outputModelForELK.loanaccounts_city_city,
                        AgentName = outputModelForELK.applicationuser_agent_agentname,
                        AgentCode = outputModelForELK.applicationuser_customid_agentid,
                        ReceiptNumber = outputModelForELK.collections_customid_receiptno,
                        PhysicalReceiptNumber = outputModelForELK.collections_physicalreceiptnumber_physicalreceiptno,
                        CollectionDate = outputModelForELK.collections_collectiondate_collectiondate,
                        CurrentBucket = outputModelForELK.loanaccounts_current_bucket_currentbucket,
                        OverdueAmountEmiAmount = outputModelForELK.collections_yoverdueamount_emiamt,
                        AmountBreakupOne = outputModelForELK.collections_amountbreakup1_amountbreakup1,
                        ForeClosureAmount = outputModelForELK.collections_yforeclosureamount_foreclosureamount,
                        SettlementAmount = outputModelForELK.settlementamount,
                        LatePaymentPenalty = outputModelForELK.collections_ypenalinterest_latepaymentpenalty,
                        OtherCharges = outputModelForELK.collections_othercharges_othercharges,
                        InstrumentDate = outputModelForELK.cheques_instrumentdate_instrumentdate,
                        InstrumentNumber = outputModelForELK.cheques_instrumentno_instrumentno,
                        InstrumentAmount = outputModelForELK.collections_amount_instrumentamount,
                        MicrCode = outputModelForELK.cheques_micrcode_micrcode,
                        BatchId = outputModelForELK.collectionbatches_customid_batchid,
                        BatchIdCreatedDate = outputModelForELK.collectionbatches_createddate_batchidcreateddate,
                        DepositDate = outputModelForELK.payinslips_createddate_depositdate,
                        BatchAmount = outputModelForELK.collectionbatches_amount_batchamount,
                        PaymentStatus = outputModelForELK.paymentstatus,
                        BomBucket = outputModelForELK.loanaccounts_bucket_bombucket,
                        NPAStageId = outputModelForELK.loanaccounts_npa_stageid_npa_stageid,
                        LatestLatitude = outputModelForELK.loanaccounts_latestlatitude_lat,
                        LatestLongitude = outputModelForELK.loanaccounts_latestlongitude_long,
                        PrimaryCardNumber = outputModelForELK.loanaccounts_primary_card_number_primary_card_number,
                        AgentEmail = outputModelForELK.applicationuser_primaryemail_agentemail,
                        PaymentTowards = outputModelForELK.paymenttowards,
                        BounceCharges = outputModelForELK.collections_ybouncecharges_bouncecharges,
                        Excess = outputModelForELK.excess,
                        Imd = outputModelForELK.imd,
                        ProcFee = outputModelForELK.procfee,
                        Swap = outputModelForELK.swap,
                        EbcCharge = outputModelForELK.ebccharge,
                        CollectionPickupCharge = outputModelForELK.collectionpickupcharge,
                        EncollectPayInSlipId = outputModelForELK.payinslips_encollectpayinslipid,
                        CmsPayinSlipNumber = outputModelForELK.payinslips_cmspayinslipno_cmspayinslipid,
                        DepositAccountNumber = outputModelForELK.payinslips_bankaccountno_depositaccountnumber,
                        DepositBankName = outputModelForELK.payinslips_bankname_depositebankname,
                        DepositAmount = outputModelForELK.payinslips_amount_depositamount,
                        MerchantReferenceNumber = outputModelForELK.merchantreferencenumber,
                        BankTransactionId = outputModelForELK.banktransactionid,
                        BankId = outputModelForELK.bankid,
                        CollectionsAmount = outputModelForELK.collections_amount_amount,
                        StatusCode = outputModelForELK.statuscode,
                        CreatedDate = outputModelForELK.collections_createddate_receiptdate,
                        Rrn = outputModelForELK.rrn,
                        CardHolderName = outputModelForELK.cardholdername,
                        MerchantId = outputModelForELK.merchantid,
                        MerchantTransactionId = outputModelForELK.merchanttransactionid,
                        AgencyName = outputModelForELK.applicationorg_agency_agencyname,
                        AgencyCode= outputModelForELK.applicationorg_customid_agencyid,
                        StaffOrAgent = outputModelForELK.applicationuser_stafforagent
                    };

                    // Add the object to the detailoutput list
                    detailoutput.Add(obj);
                }
                catch(Exception ex)
                {

                }
            }

            queryResult = detailoutput;

            this.FlexiPagedList = new FlexiPagedList<GetMoneyMovementDetailsDto>(TotalCount, _params.PageNumber ?? 0, _params.PageSize);
            this.FlexiPagedList.AddRange(queryResult);

            return FlexiPagedList;

        }


    }


    /// <summary>
    /// Constructs a DSL query string dynamically based on filters such as region, product, agent, and dates.
    /// </summary>
    /// <param name="_params"></param>
    /// <returns></returns>
    private string BuildDslQueryForMoneyMovementInsighDetail(GetMoneyMovementDetailsParams _params)
    {
        //int year = DateTime.Now.Year;
        //int month = DateTime.Now.Month;

        int? from = (_params.PageNumber) * _params.PageSize;

        string moneymovementproductgroupfilter = _elasticSearchService.FormatArrayFilter(_params.ProductGroup);
        string moneymovementproductfilter = _elasticSearchService.FormatArrayFilter(_params.Product);
        string moneymovementsubproductfilter = _elasticSearchService.FormatArrayFilter(_params.SubProduct);
        string moneymovementcurrentbucketfilter = _elasticSearchService.FormatArrayFilter(_params.CurrentBucket);
        string moneymovementbombucketfilter = _elasticSearchService.FormatArrayFilter(_params.BOMBucket);
        string moneymovementregionfilter = _elasticSearchService.FormatArrayFilter(_params.Region);
        string moneymovementstatefilter = _elasticSearchService.FormatArrayFilter(_params.State);
        string moneymovementcityfilter = _elasticSearchService.FormatArrayFilter(_params.City);
        string moneymovementbranchfilter = _elasticSearchService.FormatArrayFilter(_params.Branch);
        string moneymovementagencyfilter = _elasticSearchService.FormatArrayFilter(_params.Agency);
        string moneymovementagentfilter = _elasticSearchService.FormatArrayFilter(_params.Agent);
        string moneymovementpaymentmodefilter = _elasticSearchService.FormatArrayFilter(_params.paymentMode);

        string moneymovementpaymentstatusfilter = _elasticSearchService.FormatArrayFilter(_params.paymentStatus);
        string moneymovementmonth = _elasticSearchService.GetFilterTextForElasticSearch(_params.receiptMonth);
        string moneymovementyear = _elasticSearchService.GetFilterTextForElasticSearch(_params.receiptYear);
        // Get Current Year and Month if the variables are null or empty
        // Check if the year and month filters are provided
        bool isYearMonthProvided = (!string.IsNullOrEmpty(moneymovementyear)  && moneymovementyear != "NoFilterPresent") && (!string.IsNullOrEmpty(moneymovementmonth) && moneymovementmonth != "NoFilterPresent");

        DateTime startDate, endDate;

        if (isYearMonthProvided)
        {
            // Use provided Year and Month
            int year = int.Parse(moneymovementyear);
            int month = int.Parse(moneymovementmonth);

            // First and last day of the selected month
            startDate = new DateTime(year, month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month
        }
        else
        {
            DateTime currentDate = DateTime.Now;
            DateTime fiveMonthsAgo = currentDate.AddMonths(-5);
            DateTime firstDayOfFiveMonthAgo = new DateTime(fiveMonthsAgo.Year, fiveMonthsAgo.Month, 1); // Go back 5 months to include current month


            // Default: Load last 6 months including the current month
            startDate = firstDayOfFiveMonthAgo; 
            endDate = DateTime.Now; // Current date
        }

        string fromDate = startDate.ToString("yyyy-MM-dd");
        string toDate = endDate.ToString("yyyy-MM-dd");

        

        string dslquerywithFilters = $@"
{{
  ""from"": {from},
  ""size"": {_params.PageSize},
  ""track_total_hits"": true,
  ""query"": {{
    ""bool"": {{
      ""must"": [
        {{
          ""range"": {{
            ""collections_collectiondate_collectiondate"": {{
               ""gte"": ""{fromDate}"",
              ""lte"": ""{toDate}""
            }}
          }}
        }},";

        // Dynamically adding filters only if values are present
        if (!string.IsNullOrEmpty(moneymovementproductgroupfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_productgroup"": [{moneymovementproductgroupfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementproductfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_product"": [{moneymovementproductfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementsubproductfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_subproduct"": [{moneymovementsubproductfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementcurrentbucketfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_current_bucket_currentbucket"": [{moneymovementcurrentbucketfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementbombucketfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_bucket_bombucket"": [{moneymovementbombucketfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementregionfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_region_region"": [{moneymovementregionfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementstatefilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_state_state"": [{moneymovementstatefilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementcityfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_city_city"": [{moneymovementcityfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementbranchfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""loanaccounts_branch_branchname"": [{moneymovementbranchfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementagentfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""applicationuser_agent_id"": [{moneymovementagentfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementpaymentstatusfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""paymentstatus"": [{moneymovementpaymentstatusfilter}] }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementpaymentmodefilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""collections_collectionmode_paymentmode"": [{moneymovementpaymentmodefilter}]  }}
        }},";

        if (!string.IsNullOrEmpty(moneymovementagencyfilter))
            dslquerywithFilters += $@"
        {{
          ""terms"": {{ ""applicationorg_agency_id"": [{moneymovementagencyfilter}]  }}
        }},";
       

         // Closing JSON structure
        dslquerywithFilters += @"
      ]
    }
  }
}";




        dslquerywithFilters = _elasticSearchService.RemoveCommaFromDslQuery(dslquerywithFilters);
        return dslquerywithFilters;

    }

}

/// <summary>
/// Defines available filters for the money movement detail fetch query.
/// Inherits from paginated parameter base class.
/// </summary>
public class GetMoneyMovementDetailsParams : PagedQueryParamsDtoBridge
{
    public List<string>? ProductGroup { get; set; }
    public List<string>? Product { get; set; }
    public List<string>? SubProduct { get; set; }
    public List<string>? BOMBucket { get; set; }
    public List<string>? CurrentBucket { get; set; }
    public List<string>? Zone { get; set; }
    public List<string>? Region { get; set; }
    public List<string>? State { get; set; }
    public List<string>? City { get; set; }
    public List<string>? Branch { get; set; }
    public List<string>? Agency { get; set; }
    public List<string>? Agent { get; set; }
    public List<string>? paymentMode { get; set; }
    public List<string>? paymentStatus { get; set; }
    public string? receiptMonth { get; set; }
    public string? receiptYear { get; set; }
}
