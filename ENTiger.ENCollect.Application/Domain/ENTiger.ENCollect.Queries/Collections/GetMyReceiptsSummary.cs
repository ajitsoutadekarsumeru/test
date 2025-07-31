using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// Retrieves today's receipt count and total amount for the logged-in user.
    /// </summary>
    public class GetMyReceiptsSummary : FlexiQueryBridgeAsync<Collection, GetMyReceiptsSummaryDto>
    {
        private readonly ILogger<GetMyReceiptsSummary> _logger;
        private readonly RepoFactory _repoFactory;
        private GetMyReceiptsSummaryParams _params;
        private FlexAppContextBridge? _flexAppContext;

        public GetMyReceiptsSummary(ILogger<GetMyReceiptsSummary> logger, RepoFactory repoFactory)
        {
            _logger = logger ;
            _repoFactory = repoFactory;
        }

        public GetMyReceiptsSummary AssignParameters(GetMyReceiptsSummaryParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<GetMyReceiptsSummaryDto> Fetch()
        {
            var query = Build<Collection>();

                var result = await query
                    .GroupBy(c => 1)
                    .Select(g => new GetMyReceiptsSummaryDto
                    {
                        TotalReceiptsCount = g.Count(),
                        TotalReceiptAmount = g.Sum(x => x.Amount)
                    })
                    .FirstOrDefaultAsync();


            return result ?? new GetMyReceiptsSummaryDto(); 

        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);
            _flexAppContext = _params.GetAppContext();
            string userId = _flexAppContext!.UserId;

            var repo = _repoFactory.GetRepo().FindAll<Collection>()
                .ByCollector(userId)
                .ByToday();

            return (IQueryable<T>)repo;
        }
    }


    /// <summary>
    /// Parameter class for GetMyReceiptsSummary query.
    /// </summary>
    public class GetMyReceiptsSummaryParams : DtoBridge
    {
        // Extend if needed
    }
}
