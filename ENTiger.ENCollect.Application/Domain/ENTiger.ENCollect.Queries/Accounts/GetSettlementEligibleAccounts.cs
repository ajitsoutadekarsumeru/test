using DocumentFormat.OpenXml.Spreadsheet;
using Elastic.Clients.Elasticsearch.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Bcpg;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public class GetSettlementEligibleAccounts : FlexiQueryPagedListBridgeAsync<LoanAccount, GetSettlementEligibleAccountsParams, GetSettlementEligibleAccountsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetSettlementEligibleAccounts> _logger;
        protected GetSettlementEligibleAccountsParams _params;
        protected readonly RepoFactory _repoFactory;

        protected readonly IApplicationUserQueryRepository _applicationUserQueryRepository;

        protected List<string?>? userBranches;
        protected FlexAppContextBridge? _flexAppContext;

        public GetSettlementEligibleAccounts(ILogger<GetSettlementEligibleAccounts> logger, RepoFactory repoFactory
            , IApplicationUserQueryRepository applicationUserQueryRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _applicationUserQueryRepository = applicationUserQueryRepository;
        }

        public virtual GetSettlementEligibleAccounts AssignParameters(GetSettlementEligibleAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// return the paginated list of settlement-eligible accounts
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetSettlementEligibleAccountsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            string userId = _flexAppContext?.UserId;

            userBranches = await _applicationUserQueryRepository.GetUserBranchByIdAsync(_flexAppContext, userId);

            var projection = await Build<LoanAccount>()
                                    .SelectTo<GetSettlementEligibleAccountsDto>()
                                    .ToListAsync();

            return BuildPagedOutput(projection);
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_flexAppContext);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByAccountNo(_params.AccountNumber)
                                        .ByCustomerId(_params.CustomerId)
                                        .ByDPDRange(_params.CurrentDPDFrom, _params.CurrentDPDTo)
                                        .WithNpaStageid(_params.NPAFlag)
                                        .ByEligibleForSettlement(_params.IsEligibleForSettlement)
                                        .ByAccountId(_params.AccountId)
                                        .ByUserBranch(userBranches)
                                        .IncludeSettlements();

            return CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);
        }
    }

    public class GetSettlementEligibleAccountsParams : PagedQueryParamsDtoBridge, IValidatableObject
    {
        public string? AccountNumber { get; set; }
        public string? CustomerId { get; set; }
        public long? CurrentDPDFrom { get; set; }
        public long? CurrentDPDTo { get; set; }
        public bool? NPAFlag { get; set; }
        public bool? IsEligibleForSettlement { get; set; }
        public string? AccountId { get; set; }
        public int Take { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if at least one filter is present
            if (string.IsNullOrWhiteSpace(AccountNumber) &&
                string.IsNullOrWhiteSpace(CustomerId) &&
                string.IsNullOrWhiteSpace(AccountId) &&
                !CurrentDPDFrom.HasValue &&
                !CurrentDPDTo.HasValue &&
                !NPAFlag.HasValue &&
                !IsEligibleForSettlement.HasValue)
            {
                yield return new ValidationResult(
                    "At least one filter must be specified.",
                    new[]
                    {
                    nameof(AccountNumber),
                    nameof(CustomerId),
                    nameof(AccountId),
                    nameof(CurrentDPDFrom),
                    nameof(CurrentDPDTo),
                    nameof(NPAFlag),
                    nameof(IsEligibleForSettlement)
                    });
            }

            // CurrentDPD validation (from/to interdependency) //  TODO :: check with Mona
            if (CurrentDPDFrom.HasValue && !CurrentDPDTo.HasValue)
            {
                yield return new ValidationResult(
                    "CurrentDPDTo is required when CurrentDPDFrom is specified.",
                    new[] { nameof(CurrentDPDTo) });
            }

            if (CurrentDPDTo.HasValue && !CurrentDPDFrom.HasValue)
            {
                yield return new ValidationResult(
                    "CurrentDPDFrom is required when CurrentDPDTo is specified.",
                    new[] { nameof(CurrentDPDFrom) });
            }

            if (CurrentDPDFrom.HasValue && CurrentDPDTo.HasValue && CurrentDPDFrom > CurrentDPDTo)
            {
                yield return new ValidationResult(
                    "CurrentDPDFrom should be less than or equal to CurrentDPDTo.",
                    new[] { nameof(CurrentDPDFrom), nameof(CurrentDPDTo) });
            }
        }
    }
}
