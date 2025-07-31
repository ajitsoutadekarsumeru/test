using System.ComponentModel.DataAnnotations;
using System.Text;
using Elastic.Transport;
using ENCollect.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class AccountsLookup : FlexiQueryPagedListBridgeAsync<LoanAccount, AccountsLookupParams, AccountsLookupDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AccountsLookup> _logger;
        protected AccountsLookupParams _params;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;
        private readonly IELKUtility _elasticUtility;
        protected string _userId;      
        protected string _parentId;

        private string agencyId = string.Empty;
        private ApplicationUser usertype = new ApplicationUser();
        public string key { get; set; }
        private EffectiveScope _scope;

        private readonly IAccountabilityQueryRepository _accountabilityQueryRepository;
        private readonly IAccountScopeConfigurationQueryRepository _scopeConfigurationQueryRepository;

        private readonly AccountScopeEvaluatorService _scopeEvaluatorService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public AccountsLookup(ILogger<AccountsLookup> logger, 
            IRepoFactory repoFactory,            
            ICustomUtility customUtility,
            IELKUtility elasticUtility,
             IAccountabilityQueryRepository accountabilityQueryRepository,
            IAccountScopeConfigurationQueryRepository scopeConfigurationQueryRepository,
            AccountScopeEvaluatorService scopeEvaluatorService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _elasticUtility = elasticUtility;
            _customUtility = customUtility;
            _accountabilityQueryRepository = accountabilityQueryRepository;
            _scopeConfigurationQueryRepository = scopeConfigurationQueryRepository;
            _scopeEvaluatorService = scopeEvaluatorService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual AccountsLookup AssignParameters(AccountsLookupParams @params)
        {
            _params = @params;
            _userId = _params.GetAppContext()?.UserId;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<AccountsLookupDto>> Fetch()
        {
            _repoFactory.Init(_params);

            _params.CreditCardNumber = await DecryptCreditCardNumberAsync();

            // Set the account scope for the current user.
            _scope = await GetAccountScope(_userId);

            //Get the user`s parent
            _parentId = await FetchUserParentIdAsync();

            var projection = Build<LoanAccount>().SelectTo<AccountsLookupDto>().ToList();
            
            await MapToDto(projection);

            var result = BuildPagedOutput(projection);

            await SetSegmentationData(result);

            return result;
        }

        private async Task MapToDto(List<AccountsLookupDto> projection)
        {
            foreach (AccountsLookupDto account in projection)
            {
                LoanAccountJSON json;
                Dictionary<dynamic, dynamic> accountData = new Dictionary<dynamic, dynamic>();

                json = await _repoFactory.GetRepo().FindAll<LoanAccountJSON>().Where(x => x.AccountId == account.Id).FirstOrDefaultAsync();
                accountData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(json.AccountJSON);

                account.AccountStatus = _customUtility.GetValue(accountData, LoanAccountJsonEnum.Status.Value);
                account.PDD = _customUtility.GetValue(accountData, LoanAccountJsonEnum.PaymentDueDate.Value);
                account.TAD = _customUtility.GetValue(accountData, LoanAccountJsonEnum.MAD.Value);
                account.MAD = (string.IsNullOrEmpty(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D180.Value)) || Math.Round(Convert.ToDouble(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D180.Value))) <= 0) ?
                               (string.IsNullOrEmpty(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D150.Value)) || Math.Round(Convert.ToDouble(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D150.Value))) <= 0 ?
                               (string.IsNullOrEmpty(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D120.Value)) || Math.Round(Convert.ToDouble(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D120.Value))) <= 0 ?
                               (string.IsNullOrEmpty(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D90.Value)) || Math.Round(Convert.ToDouble(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D90.Value))) <= 0 ?
                               (string.IsNullOrEmpty(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D60.Value)) || Math.Round(Convert.ToDouble(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D60.Value))) <= 0 ?
                               (string.IsNullOrEmpty(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D30.Value)) || Math.Round(Convert.ToDouble(_customUtility.GetValue(accountData, LoanAccountJsonEnum.D30.Value))) <= 0 ? "0" :
                               _customUtility.GetValue(accountData, LoanAccountJsonEnum.D30.Value)) : _customUtility.GetValue(accountData, LoanAccountJsonEnum.D60.Value)) : _customUtility.GetValue(accountData, LoanAccountJsonEnum.D90.Value)) : _customUtility.GetValue(accountData, LoanAccountJsonEnum.D120.Value)) : _customUtility.GetValue(accountData, LoanAccountJsonEnum.D150.Value)) : _customUtility.GetValue(accountData, LoanAccountJsonEnum.D180.Value);

                account.CurrentBalance = _customUtility.GetValue(accountData, LoanAccountJsonEnum.CurrentBalance.Value);
                account.AccountJSON = json?.AccountJSON ?? null;
            }
        }
        private async Task<EffectiveScope> GetAccountScope(string userId)
        {
            // 1. Fetch Accountabilities for the given user.
            List<Accountability> accountabilities = await _accountabilityQueryRepository.GetAccountabilities(userId, _params.GetAppContext());

            // 2. Fetch Scope Configurations based on the retrieved accountabilities.
            List<AccountScopeConfiguration> scopeConfigs = await _scopeConfigurationQueryRepository.GetScopeConfigurations(accountabilities, _params.GetAppContext());

            // 3. Evaluate the effective scope (which may include the parent's id) using the provided accountabilities and scope configurations.
            EffectiveScope effectiveScope = await _scopeEvaluatorService.EvaluateScope(accountabilities, scopeConfigs, userId, _params.GetAppContext());

            return effectiveScope;
        }
        private async Task<string> FetchUserParentIdAsync()
        {
            string? parentId = _scope.ParentId;
            if (string.IsNullOrEmpty(_scope.ParentId))
            {
                // Fetch the user once and check their type
                var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().FirstOrDefaultAsync(a => a.Id == _userId);

                switch (user)
                {
                    case AgencyUser agencyUser:
                        parentId = agencyUser.AgencyId;
                        break;

                    case CompanyUser companyUser:
                        parentId = companyUser.BaseBranchId;
                        break;

                    default:
                        parentId = null; // Ensure _parentId is reset if user is not found
                        break;
                }
            }
            return parentId;
        }
        private async Task SetSegmentationData(FlexiPagedList<AccountsLookupDto> result)
        {
            List<string> loanaccountids = result.Select(a => a.CustomerAccountNo).ToList();
            var segmentids = await FetchSegmentName(loanaccountids);
            result.ForEach(a =>
            {
                var p = segmentids.Where(b => b.id == a.Id).FirstOrDefault();
                a.SegmentationId = p != null ? p.segmentationid : "";
            });

            var segmentationids = result.Select(a => a.SegmentationId).ToList();

            var segments = await _repoFactory.GetRepo().FindAll<Segmentation>().Where(a => segmentationids.Contains(a.Id)).ToListAsync();
            result.ForEach(a =>
            {
                var p = segments.Where(b => b.Id == a.SegmentationId).FirstOrDefault();
                a.SegmentationName = p != null ? p.Name : "";
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);  

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                 .IncludeAccountJson()
                                    .ByIsLoanAccount(_params.isloanaccount)
                                    .ByLoanAccountNo(_params.AccountNo, _params.isloanaccount)
                                    .ByLastXDigitsOfAccountNo(_params.LastXDigitsOfAccountNo)
                                    .ByPartnerLoanId(_params.PartnerLoanId)
                                    .ByCustomerName(_params.CustomerName)
                                    .ByMobileNumber(_params.MobileNumber)
                                    .ByCustomerId(_params.CustomerID)
                                    .ByCardNumber(_params.CreditCardNumber)
                                    .ByLastXDigitsOfCardNumber(_params.LastXDigitsOfCreditCardNumber)
                                    .byBucket(_params.Bucket)
                                    .byBillingCycle(_params.Cycle)
                                    .byBranchCode(_params.BranchCode)
                                    .byPaymentStatus(_params.Status)
                                    .ByUserType(usertype, _parentId)
                                    .ByScope(_scope, _userId);

            //Build Your Query Here
            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);
            return query;
        }

        private async Task<string> DecryptCreditCardNumberAsync()
        {
            string creditCardNumber = string.Empty;
            if (!string.IsNullOrEmpty(_params.ReferenceId))
            {
                var userloginresult = await _repoFactory.GetRepo().Find<UserLoginKeys>(_params.ReferenceId).FirstOrDefaultAsync();
                key = userloginresult != null ? userloginresult.Key : "";
                var aesGcmCrypto = new AesGcmCrypto();
                var aesGcmKey = Encoding.UTF8.GetBytes(key);
                creditCardNumber = !string.IsNullOrEmpty(_params.CreditCardNumber) ? aesGcmCrypto.Decrypt(_params.CreditCardNumber, aesGcmKey) : string.Empty;
            }
            return creditCardNumber;
        }




        private async Task<List<ElasticSearchSimulateLoanAccount>> FetchSegmentName(List<string> loanaccountids)
        {
            string strloanaccountids = string.Empty;
            List<ElasticSearchSimulateLoanAccount> segments = new List<ElasticSearchSimulateLoanAccount>();
            try
            {
                foreach (var id in loanaccountids)
                {
                    if (string.IsNullOrEmpty(id))
                    {
                        strloanaccountids = "" + id + "";
                    }
                    else
                    {
                        strloanaccountids = strloanaccountids + "\"" + "," + "\"" + id;
                    }
                }

                string DSLQueryForAllDocs1 = @"
            {
                  ""_source"": [""id"",""segmentationid"",""AGREEMENTID""],
                  ""size"": 100,
                  ""query"": {
                                ""terms"": {
                                    ""AGREEMENTID"":[";

                DSLQueryForAllDocs1 += $@"

            ""{strloanaccountids}""
            ]
            }}
            }}
            }}
            ";

                var fetchindexname = await _repoFactory.GetRepo().FindAll<FeatureMaster>().Where(a => a.Parameter == "SegmentationIndexName").FirstOrDefaultAsync();

                string loanaccountsIndex = fetchindexname?.Value;

                var client = _elasticUtility.GetElasticConnection();

                string elasticsearchapipath = loanaccountsIndex + "/_search";

                var elkresp1 = client.Transport.RequestAsync<StringResponse>(Elastic.Transport.HttpMethod.POST, elasticsearchapipath, PostData.String(DSLQueryForAllDocs1)).GetAwaiter().GetResult();

                if (elkresp1 != null && elkresp1.Body != null)
                {
                    string response = elkresp1.Body;
                    dynamic RootObj = JObject.Parse(response);
                    if (RootObj != null && RootObj.hits != null && RootObj.hits.hits != null)
                    {
                        foreach (var res in RootObj.hits.hits)
                        {
                            ElasticSearchSimulateLoanAccount loanaccount = new ElasticSearchSimulateLoanAccount();
                            var obj = res._source;
                            if (obj != null)
                            {
                                loanaccount.id = obj.id != null ? obj.id : "";
                                loanaccount.segmentationid = obj.segmentationid != null ? obj.segmentationid : "";
                                loanaccount.agreementid = obj.AGREEMENTID != null ? obj.AGREEMENTID : "";
                                segments.Add(loanaccount);
                            }
                        }
                    }
                }

                return segments;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while fetching segmentationId " + ex);
            }
            return segments;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class AccountsLookupParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid Id")]
        public string? Id { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid CustomerName")]
        public string? CustomerName { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid AccountNo")]
        public string? AccountNo { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid AccountNo")]
        public string? LastXDigitsOfAccountNo { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid MobileNumber")]
        public string? MobileNumber { get; set; }

        public string? CustomerID { get; set; }
        public string? ReferenceId { get; set; }
        public bool isloanaccount { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? LastXDigitsOfCreditCardNumber { get; set; }
        public string? PartnerLoanId { get; set; }
        public string? Bucket { get; set; }
        public string? Cycle { get; set; }
        public string? BranchCode { get; set; }
        public string? Status { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
    public class ElasticSearchSimulateLoanAccount
    {
        public string id { get; set; }

        public double? bom_pos { get; set; }

        public string segmentationid { get; set; }

        public string agreementid { get; set; }

        public string paymentstatus { get; set; }

        public string createddate { get; set; }

        public int counter { get; set; }
    }
}