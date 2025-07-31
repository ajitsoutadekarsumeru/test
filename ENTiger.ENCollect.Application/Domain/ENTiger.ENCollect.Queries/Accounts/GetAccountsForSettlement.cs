using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public class GetAccountsForSettlement : FlexiQueryPagedListBridgeAsync<LoanAccount, GetAccountsForSettlementParams, GetAccountsForSettlementDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetAccountsForSettlement> _logger;
        protected GetAccountsForSettlementParams _params;
        protected readonly RepoFactory _repoFactory;

        public GetAccountsForSettlement(ILogger<GetAccountsForSettlement> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual GetAccountsForSettlement AssignParameters(GetAccountsForSettlementParams @params)
        {
            _params = @params;
            return this;
        }


        /// <summary>
        /// return the paginated list of accounts available for settlement
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetAccountsForSettlementDto>> Fetch()
        {
            var accounts = await Build<LoanAccount>().SelectTo<GetAccountsForSettlementDto>().ToListAsync();

            return BuildPagedOutput(accounts);
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByDPDRange(_params.CurrentDPDFrom,_params.CurrentDPDTo)
                                        .ByTotalOutStanding(_params.TotalOutStandingFrom, _params.TotalOutStandingTo)
                                        .ByTotalOverDue(_params.TotalOverDueFrom, _params.TotalOverDueTo)
                                        .ByCustomerId(_params.CustomerId)
                                        .WithNpaStageid(_params.NPAFlag)
                                        .ByEligibleForSettlement(_params.MarkedAsEligible);

            return  CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);
        }
    }

    public class GetAccountsForSettlementParams : PagedQueryParamsDtoBridge, IValidatableObject
    {
        public long? CurrentDPDFrom { get; set; }
        public long? CurrentDPDTo { get; set; }
        public decimal? TotalOutStandingFrom { get; set; }
        public decimal? TotalOutStandingTo { get; set; }
        public decimal? TotalOverDueFrom { get; set; }
        public decimal? TotalOverDueTo { get; set; }
        public string? CustomerId { get; set; }
        public bool? NPAFlag { get; set; }
        public bool? MarkedAsEligible { get; set; }
        public int Take { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if at least one filter is present
            if (!CurrentDPDFrom.HasValue && !CurrentDPDTo.HasValue &&
                !TotalOutStandingFrom.HasValue && !TotalOutStandingTo.HasValue &&
                !TotalOverDueFrom.HasValue && !TotalOverDueTo.HasValue &&
                string.IsNullOrWhiteSpace(CustomerId) && 
                !NPAFlag.HasValue &&
                !MarkedAsEligible.HasValue)
            {
                yield return new ValidationResult(
                    "At least one filter must be specified.",
                    new[]
                    {
                    nameof(CurrentDPDFrom), nameof(CurrentDPDTo),
                    nameof(TotalOutStandingFrom), nameof(TotalOutStandingTo),
                    nameof(TotalOverDueFrom), nameof(TotalOverDueTo),
                    nameof(CustomerId), nameof(NPAFlag), nameof(MarkedAsEligible)
                    });
            }

            // CurrentDPD validation
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

            // TotalOutStanding validation
            if (TotalOutStandingFrom.HasValue && !TotalOutStandingTo.HasValue)
            {
                yield return new ValidationResult(
                    "TotalOutStandingTo is required when TotalOutStandingFrom is specified.",
                    new[] { nameof(TotalOutStandingTo) });
            }

            if (TotalOutStandingTo.HasValue && !TotalOutStandingFrom.HasValue)
            {
                yield return new ValidationResult(
                    "TotalOutStandingFrom is required when TotalOutStandingTo is specified.",
                    new[] { nameof(TotalOutStandingFrom) });
            }

            if (TotalOutStandingFrom.HasValue && TotalOutStandingTo.HasValue && TotalOutStandingFrom > TotalOutStandingTo)
            {
                yield return new ValidationResult(
                    "TotalOutStandingFrom should be less than or equal to TotalOutStandingTo.",
                    new[] { nameof(TotalOutStandingFrom), nameof(TotalOutStandingTo) });
            }

            // TotalOverDue validation
            if (TotalOverDueFrom.HasValue && !TotalOverDueTo.HasValue)
            {
                yield return new ValidationResult(
                    "TotalOverDueTo is required when TotalOverDueFrom is specified.",
                    new[] { nameof(TotalOverDueTo) });
            }

            if (TotalOverDueTo.HasValue && !TotalOverDueFrom.HasValue)
            {
                yield return new ValidationResult(
                    "TotalOverDueFrom is required when TotalOverDueTo is specified.",
                    new[] { nameof(TotalOverDueFrom) });
            }

            if (TotalOverDueFrom.HasValue && TotalOverDueTo.HasValue && TotalOverDueFrom > TotalOverDueTo)
            {
                yield return new ValidationResult(
                    "TotalOverDueFrom should be less than or equal to TotalOverDueTo.",
                    new[] { nameof(TotalOverDueFrom), nameof(TotalOverDueTo) });
            }
        }
    }
}
