using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchPrimaryAllocationbyFilters : FlexiQueryBridgeAsync<LoanAccount, SearchPrimaryAllocationbyFiltersDto>
    {
        protected readonly ILogger<SearchPrimaryAllocationbyFilters> _logger;
        protected SearchPrimaryAllocationbyFiltersParams _params;
        protected readonly IRepoFactory _repoFactory;

        public ICollection<SearchPrimaryAllocationbyLoanAccountsDto> LoanAccounts = new List<SearchPrimaryAllocationbyLoanAccountsDto>();
        public ICollection<SearchPrimaryAllocationbyLoanAccountsDto> UnAllocatedAccounts = new List<SearchPrimaryAllocationbyLoanAccountsDto>();
        public ICollection<SearchPrimaryAllocationbyLoanAccountsDto> AllocatedAccounts = new List<SearchPrimaryAllocationbyLoanAccountsDto>();
        public List<SearchPrimaryAllocationbyLoanAccountsDto> AccountList = new List<SearchPrimaryAllocationbyLoanAccountsDto>();
        private IFlexRepository _RepoFlex;
        public decimal? totalTOS;
        public Int64 Count;
        public bool? Unallocatedbool;
        public bool? Allocatedbool;
        public string userId;
        private ApplicationUser loggedInUserParty = new ApplicationUser();
        private Accountability responsibleUser;
        private string commissionerId;
        protected FlexAppContextBridge? _flexAppContext;
        public int _skip;
        public int _take;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchPrimaryAllocationbyFilters(ILogger<SearchPrimaryAllocationbyFilters> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchPrimaryAllocationbyFilters AssignParameters(SearchPrimaryAllocationbyFiltersParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<SearchPrimaryAllocationbyFiltersDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();  //do not remove this line
            _repoFactory.Init(_params);
            _take = 50;

            _skip = 0;

            LoanAccounts = await FetchAccountAsync(LoanAccounts, _params);

            SearchPrimaryAllocationbyFiltersDto searchPrimaryAllocationbyFiltersDto = new SearchPrimaryAllocationbyFiltersDto();
            var projection = Build<LoanAccount>();
            Allocatedbool = _params.Allocated;
            Unallocatedbool = _params.Unallocated;
            userId = _flexAppContext.UserId;

            loggedInUserParty = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == _flexAppContext.UserId).FirstOrDefaultAsync();
            responsibleUser = loggedInUserParty.GetAccountabilityAsResponsible(_repoFactory.GetRepo()).FirstOrDefault();

            if (projection?.Count() > 0)
            {
                if (Allocatedbool != null && Allocatedbool == true)
                {
                    AllocatedAccounts = Allocated(LoanAccounts);
                    AccountList.AddRange(AllocatedAccounts);
                    searchPrimaryAllocationbyFiltersDto.Allocated = AllocatedAccounts.GroupBy(t => new { t.AgencyId, t.TeleCallingAgencyId, t.TOS })
                                                    .Select(t => new AgencySummaryAllocatedDto
                                                    {
                                                        TelleCallerAgencyName = t.FirstOrDefault().TeleCallingAgencyName,
                                                        AgencyName = t.FirstOrDefault().AgencyName,
                                                        Count = t.Count().ToString(),
                                                        TOS = t.Sum(ts => Convert.ToDecimal(ts.TOS)),
                                                        Tospercentage = totalTOS != 0 ? ((t.Sum(ts => Convert.ToDecimal(ts.TOS)) * 100) / totalTOS) : 0,
                                                    }).ToList();
                }

                if (Unallocatedbool != null && Unallocatedbool == true)
                {
                    UnAllocatedAccounts = UnAllocated(LoanAccounts);
                    AccountList.AddRange(UnAllocatedAccounts);
                    searchPrimaryAllocationbyFiltersDto.UnAllocated = UnAllocatedAccounts
                                                    .GroupBy(t => new { t.CITY, t.TOS }).Select(t => new AgencySummaryUnAllocatedDto
                                                    {
                                                        city = t.FirstOrDefault()?.CITY,
                                                        Count = t.Count().ToString(),
                                                        Tos = t.Sum(ts => Convert.ToDecimal(ts.TOS)),
                                                        TosPercentage = totalTOS != 0 ? ((t.Sum(ts => Convert.ToDecimal(ts.TOS)) * 100) / totalTOS) : 0,
                                                    }).ToList();
                }

                searchPrimaryAllocationbyFiltersDto.AccountList = AccountList
                                                .Select(a => new AgencyFilterGridDto
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
                                                    CUSTOMERID = a.CUSTOMERID
                                                }).ToList();

                searchPrimaryAllocationbyFiltersDto.count = AccountList.Count();
                totalTOS = AccountList.Sum(ts => Convert.ToDecimal(ts.TOS));
                searchPrimaryAllocationbyFiltersDto.totalTos = AccountList.Sum(ts => Convert.ToDecimal(ts.TOS));
            }

            return searchPrimaryAllocationbyFiltersDto;
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
                                .ByAccountNo(_params.AccountNo)
                               .ByCustomerName(_params.CustomerName)
                               .ByBucket(_params.bucket)
                               .byDPD(_params.CurrentDPD)
                               .byZone(_params.zone)
                               .byRegions(_params.region)
                               .byStates(_params.state)
                               .byCitys(_params.city);

            return query;
        }

        private ICollection<SearchPrimaryAllocationbyLoanAccountsDto> Allocated(ICollection<SearchPrimaryAllocationbyLoanAccountsDto> accounts)
        {
            ICollection<AgencySummaryAllocatedDto> output = new List<AgencySummaryAllocatedDto>();

            if (loggedInUserParty.GetType() == typeof(AgencyUser))
            {
                accounts = accounts.Where(b => b.AgencyId == commissionerId).ToList();
            }
            else if (loggedInUserParty.GetType() == typeof(CompanyUser) &&
                     string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
            {
                accounts = accounts.Where(a => (a.TeleCallingAgencyId != null || a.AgencyId != null)).ToList();
            }
            else if (loggedInUserParty.GetType() == typeof(CompanyUser) &&
                     !string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
            {
                accounts = accounts.Where(a => a.AllocationOwnerId == userId && (a.TeleCallingAgencyId != null || a.AgencyId != null)).ToList();
            }

            return accounts;
        }


        private ICollection<SearchPrimaryAllocationbyLoanAccountsDto> UnAllocated(ICollection<SearchPrimaryAllocationbyLoanAccountsDto> accounts)
        {
            ICollection<AgencySummaryUnAllocatedDto> output = new List<AgencySummaryUnAllocatedDto>();

            if (loggedInUserParty.GetType() == typeof(AgencyUser))
            {
                accounts = accounts.Where(b => 1 == 2).ToList();  // Will return no records (placeholder logic).
            }
            else if (loggedInUserParty.GetType() == typeof(CompanyUser) &&
                     string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
            {
                accounts = accounts.Where(a => (a.TeleCallingAgencyId == null && a.AgencyId == null)).ToList();
            }
            else if (loggedInUserParty.GetType() == typeof(CompanyUser) &&
                     !string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
            {
                accounts = accounts.Where(a => a.AllocationOwnerId == userId && (a.TeleCallingAgencyId == null && a.AgencyId == null)).ToList();
            }

            return accounts;
        }


        private async Task<ICollection<SearchPrimaryAllocationbyLoanAccountsDto>> FetchAccountAsync(ICollection<SearchPrimaryAllocationbyLoanAccountsDto> loanAccounts, SearchPrimaryAllocationbyFiltersParams input)
        {
            Count = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                           .ByAccountNo(input.AccountNo)
                           .ByCustomerName(input.CustomerName)
                           .byProductGroup(input.ProductGroup)
                           .byproduct(input.Product)
                           .bysubProduct(input.SubProduct)
                           .bydownloadBucket(input.bucket)
                           .byDPD(input.CurrentDPD)
                           .byZone(input.zone)
                           .bycity(input.city)
                           .byRegion(input.region)
                           .byState(input.state)
                           .byCity(input.city)
                           .CountAsync();

            List<SearchPrimaryAllocationbyLoanAccountsDto> loanAccountsoutput = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                        .FlexInclude(x => x.Agency)
                                        .FlexInclude(x => x.Collector)
                                        .FlexInclude(x => x.TeleCallingAgency)
                                        .FlexInclude(x => x.TeleCaller)
                           .ByAccountNo(input.AccountNo)
                           .ByCustomerName(input.CustomerName)
                           .byProductGroup(input.ProductGroup)
                           .byproduct(input.Product)
                           .bysubProduct(input.SubProduct)
                           .bydownloadBucket(input.bucket)
                           .byDPD(input.CurrentDPD)
                           .byZone(input.zone)
                           .bycity(input.city)
                           .byRegion(input.region)
                           .byState(input.state)
                           .byCity(input.city).Select(s => new
                           {
                               s.Id,
                               s.CITY,
                               s.CustomId,
                               s.CUSTOMERNAME,
                               s.PRODUCT,
                               s.ProductGroup,
                               s.BUCKET,
                               //s.DELSTRING,
                               s.CURRENT_DPD,
                               s.TOS,
                               s.AgencyId,
                               s.TeleCallingAgencyId,
                               s.AllocationOwnerId,
                               Agency = new { s.Agency.FirstName, s.Agency.LastName, s.Agency.CustomId },
                               TelecallerAgency = new { s.TeleCallingAgency.FirstName, s.TeleCallingAgency.LastName, s.TeleCallingAgency.CustomId },
                               s.CUSTOMERID
                           }).ToAsyncEnumerable()
                            .Select(a => new SearchPrimaryAllocationbyLoanAccountsDto
                            {
                                Id = a.Id,
                                CITY = a.CITY,
                                CustomId = a.CustomId,
                                CUSTOMERNAME = a.CUSTOMERNAME,
                                PRODUCT = a.PRODUCT,
                                ProductGroup = a.ProductGroup,
                                BUCKET = a.BUCKET,
                                // DELSTRING = a.DELSTRING,
                                CURRENT_DPD = a.CURRENT_DPD,
                                TOS = string.IsNullOrEmpty(a.TOS) ? "0" : a.TOS,
                                AgencyId = a.AgencyId,
                                TeleCallingAgencyId = a.TeleCallingAgencyId,
                                AgencyName = a.Agency.FirstName + " " + a.Agency.LastName,
                                TeleCallingAgencyName = a.TelecallerAgency.FirstName + " " + a.TelecallerAgency.LastName,
                                AllocationOwnerId = a.AllocationOwnerId,
                                CUSTOMERID= a.CUSTOMERID,
                            }).Skip(_skip).Take(_take)
                           .ToListAsync();

            return loanAccountsoutput;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchPrimaryAllocationbyFiltersParams : DtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "You may not use special characters.Invalid ProductGroup")]
        public string? ProductGroup { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "You may not use special characters.Invalid Product")]
        public string? Product { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "You may not use special characters.Invalid SubProduct")]
        public string? SubProduct { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid DPD")]
        public string? CurrentDPD { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Invalid Delstring")]
        public string? delstring { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Invalid Region")]
        public string? region { get; set; }

        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "Invalid Bucket")]
        public string? bucket { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Zone")]
        public string? zone { get; set; }

        public string? branch { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid State")]
        public string? state { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid City")]
        public string? city { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid CustomerName")]
        public string? CustomerName { get; set; }

        [RegularExpression("^[a-zA-Z0-9-.]*$", ErrorMessage = "Invalid AccountNo")]
        public string? AccountNo { get; set; }

        public bool? Allocated { get; set; }

        public bool? Unallocated { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Invalid Branch Name")]
        public string? BranchName { get; set; }
    }
}