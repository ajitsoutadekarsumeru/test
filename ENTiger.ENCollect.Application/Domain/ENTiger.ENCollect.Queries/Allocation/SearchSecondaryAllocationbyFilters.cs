using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchSecondaryAllocationbyFilters : FlexiQueryBridgeAsync<LoanAccount, SearchSecondaryAllocationbyFiltersDto>
    {
        protected readonly ILogger<SearchSecondaryAllocationbyFilters> _logger;
        protected SearchSecondaryAllocationbyFiltersParams _params;
        protected readonly IRepoFactory _repoFactory;
        private decimal? totalTOS;
        public Int64 Count;
        public bool? Unallocatedbool;
        public bool? Allocatedbool;
        public string userId;
        public int _skip;
        public int _take;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchSecondaryAllocationbyFilters(ILogger<SearchSecondaryAllocationbyFilters> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchSecondaryAllocationbyFilters AssignParameters(SearchSecondaryAllocationbyFiltersParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<SearchSecondaryAllocationbyFiltersDto> Fetch()
        {
            if (_params.take < 0)
            {
                _take = 0;
            }
            if (_params.take == 0)
            {
                _take = 50;
            }
            else
            {
                _take = _params.take;
            }
            if (_params.skip < 0)
            {
                _skip = 0;
            }
            else
            {
                _skip = _params.skip;
            }
            Unallocatedbool = _params.Unallocated;
            Allocatedbool = _params.Allocated;

            SearchSecondaryAllocationbyFiltersDto outputmodel = new SearchSecondaryAllocationbyFiltersDto();
            var projection = await Build<LoanAccount>().ToListAsync();

            var loanaccounts = projection.Select(a => new SearchSecondaryAllocationbyLoanAccountsDto
            {
                Id = a.Id,
                CITY = a.CITY,
                CustomId = a.CustomId,
                CUSTOMERNAME = a.CUSTOMERNAME,
                PRODUCT = a.PRODUCT,
                ProductGroup = a.ProductGroup,
                BUCKET = a.BUCKET,
                //  DELSTRING = a.DELSTRING,
                CURRENT_DPD = a.CURRENT_DPD,
                TOS = string.IsNullOrEmpty(a.TOS) ? "0" : a.TOS,
                CollectorId = a.CollectorId,
                TeleCallerId = a.TeleCallerId,
                AgencyName = a.Agency?.FirstName + " " + a.Agency?.LastName,
                TeleCallingAgencyName = a.TeleCallingAgency?.FirstName + " " + a.TeleCallingAgency?.LastName,
                CollectorName = a.Collector?.FirstName + " " + a.Collector?.LastName,
                TeleCallerName = a.TeleCaller?.FirstName + " " + a.TeleCaller?.LastName,
                CUSTOMERID = a.CUSTOMERID
            })
            .ToList();
            //var result = BuildPagedOutput(projection);

            //packet.outputModel = new SearchAccountsForSecondaryAllocationOutputApiModel();
            outputmodel.count = loanaccounts.Count();
            totalTOS = loanaccounts.Sum(ts => Convert.ToDecimal(ts.TOS));
            outputmodel.totalTos = loanaccounts.Sum(ts => Convert.ToDecimal(ts.TOS));
            outputmodel.UnAllocated = new List<AgentSummaryUnAllocatedOutputApiModel>();
            outputmodel.UnAllocated = Unallocated(loanaccounts);
            outputmodel.Allocated = new List<AgentSummaryAllocatedOutPutApiModel>();
            outputmodel.Allocated = AllocatedSummary(loanaccounts);
            outputmodel.AccountList = new List<AgencyFilterGridOutPutApiModel>();
            outputmodel.AccountList = Accountlist(loanaccounts);
            //outputmodel.count = Count;

            return outputmodel;
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
                                        .FlexInclude(x => x.Agency)
                                        .FlexInclude(x => x.Collector)
                                        .FlexInclude(x => x.TeleCallingAgency)
                                        .FlexInclude(x => x.TeleCaller)
                                  .ByAccountNo(_params.AccountNo)
                                  .ByCustomerName(_params.CustomerName)
                                  .byProductGroup(_params.ProductGroup)
                                  .byproduct(_params.Product)
                                  .bysubProduct(_params.SubProduct)
                                  .bydownloadBucket(_params.bucket)
                                  .byDPD(_params.CurrentDPD)
                                  .byZone(_params.zone)
                                  .byRegion(_params.region)
                                  .byState(_params.state)
                                  .byCity(_params.city)
                                  .Skip(_skip).Take(_take)
                                  ;

            //Build Your Query With All Parameters Here

            //query = CreatePagedQuery<T>(query, _params.PageNumber, _params.PageSize);

            return query;
        }

        private ICollection<AgencyFilterGridOutPutApiModel> Accountlist(ICollection<SearchSecondaryAllocationbyLoanAccountsDto> loanAccounts)
        {
            List<AgencyFilterGridOutPutApiModel> output = new List<AgencyFilterGridOutPutApiModel>();
            if (Unallocatedbool != null && Unallocatedbool == true)
            {
                ICollection<AgencyFilterGridOutPutApiModel> unallocated = new List<AgencyFilterGridOutPutApiModel>();
                unallocated = loanAccounts.Where(q => (q.TeleCallerId == null && q.CollectorId == null))
                              .Select(a => new AgencyFilterGridOutPutApiModel
                              {
                                  Id = a.Id,
                                  AccountNo = a.CustomId,
                                  CustomerName = a.CUSTOMERNAME,
                                  Product = a.PRODUCT,
                                  Bucket = a.BUCKET.ToString(),
                                  //  delstring = a.DELSTRING,
                                  dpd = a.CURRENT_DPD.ToString(),
                                  Tos = Convert.ToDecimal(a.TOS),
                                  AgencyName = a.AgencyName,
                                  TCAgencyName = a.TeleCallingAgencyName,
                                  TCAgentName = a.TeleCallerName,
                                  AgentName = a.CollectorName,
                                  CUSTOMERID = a.CUSTOMERID
                              }).ToList();
                output.AddRange(unallocated);
            }
            if (Allocatedbool != null && Allocatedbool == true)
            {
                ICollection<AgencyFilterGridOutPutApiModel> allocated = new List<AgencyFilterGridOutPutApiModel>();
                allocated = loanAccounts.Where(q => (!string.IsNullOrEmpty(q.TeleCallerId) || !string.IsNullOrEmpty(q.CollectorId)))
                              .Select(a => new AgencyFilterGridOutPutApiModel
                              {
                                  Id = a.Id,
                                  AccountNo = a.CustomId,
                                  CustomerName = a.CUSTOMERNAME,
                                  Product = a.PRODUCT,
                                  Bucket = a.BUCKET.ToString(),
                                  //  delstring = a.DELSTRING,
                                  dpd = a.CURRENT_DPD.ToString(),
                                  Tos = Convert.ToDecimal(a.TOS),
                                  AgencyName = a.AgencyName,
                                  TCAgencyName = a.TeleCallingAgencyName,
                                  TCAgentName = a.TeleCallerName,
                                  AgentName = a.CollectorName,
                                  CUSTOMERID = a.CUSTOMERID
                              }).ToList();
                output.AddRange(allocated);
            }

            return output;
        }

        private ICollection<AgentSummaryAllocatedOutPutApiModel> AllocatedSummary(ICollection<SearchSecondaryAllocationbyLoanAccountsDto> loanAccounts)
        {
            ICollection<AgentSummaryAllocatedOutPutApiModel> output = new List<AgentSummaryAllocatedOutPutApiModel>();
            output = loanAccounts.Where(q => (!string.IsNullOrEmpty(q.TeleCallerId) || !string.IsNullOrEmpty(q.CollectorId)))
                            .GroupBy(t => new { t.CollectorId, t.TeleCallerId })
                            .Select(t => new AgentSummaryAllocatedOutPutApiModel
                            {
                                TeleCallerAgentName = t.FirstOrDefault().TeleCallerName,
                                AgentName = t.FirstOrDefault().CollectorName,
                                Count = t.Count().ToString(),
                                TOS = t.Sum(ts => Convert.ToDecimal(ts.TOS)),
                                Tospercentage = totalTOS != 0 ? ((t.Sum(ts => Convert.ToDecimal(ts.TOS)) * 100) / totalTOS) : 0
                            }).ToList();
            return output;
        }

        private ICollection<AgentSummaryUnAllocatedOutputApiModel> Unallocated(ICollection<SearchSecondaryAllocationbyLoanAccountsDto> loanAccounts)
        {
            ICollection<AgentSummaryUnAllocatedOutputApiModel> output = new List<AgentSummaryUnAllocatedOutputApiModel>();

            output = loanAccounts.Where(q => (q.TeleCallerId == null && q.CollectorId == null))
                                   .GroupBy(t => new { t.AgencyId, t.TeleCallerId }).Select(t => new AgentSummaryUnAllocatedOutputApiModel
                                   {
                                       TeleCallerAgencyName = t.FirstOrDefault().TeleCallingAgencyName,
                                       AgencyName = t.FirstOrDefault().AgencyName,
                                       Count = t.Count().ToString(),
                                       Tos = t.Sum(ts => Convert.ToDecimal(ts.TOS)),
                                       TosPercentage = totalTOS != 0 ? ((t.Sum(ts => Convert.ToDecimal(ts.TOS)) * 100) / totalTOS) : 0
                                   }).ToList();

            return output;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchSecondaryAllocationbyFiltersParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "You may not use special characters.Invalid ProductGroup")]
        public string? ProductGroup { get; set; }

        // [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Product")]
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "You may not use special characters.Invalid Product")]
        public string? Product { get; set; }

        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Sub Product")]
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "You may not use special characters.Invalid SubProduct")]
        public string? SubProduct { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid DPD")]
        public string? CurrentDPD { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid Delstring")]
        public string? delstring { get; set; }

        //public long? bucket { get; set; }
        public string? bucket { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Zone")]
        public string? zone { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Invalid Region")]
        public string? region { get; set; }

        public string? branch { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid State")]
        public string? state { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid City")]
        public string? city { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid CustomerName")]
        public string? CustomerName { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid AccountNo")]
        public string? AccountNo { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid TCAgency")]
        public string? TCAgency { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid Agency")]
        public string? Agency { get; set; }

        public bool? Allocated { get; set; }
        public bool? Unallocated { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Invalid Branch Name")]
        public string? BranchName { get; set; }

        [Required]
        public int take { get; set; }

        [Required]
        public int skip { get; set; }
    }
}