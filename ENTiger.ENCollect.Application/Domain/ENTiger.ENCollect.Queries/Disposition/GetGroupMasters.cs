using Microsoft.EntityFrameworkCore;
namespace ENTiger.ENCollect.DispositionModule
{
    public class GetGroupMasters : FlexiQueryEnumerableBridgeAsync<DispositionGroupMaster, GetGroupMastersDto>
    {
        protected readonly ILogger<GetGroupMasters> _logger;
        protected GetGroupMastersParams _params;
        protected readonly IRepoFactory _repoFactory;

        public GetGroupMasters(ILogger<GetGroupMasters> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual GetGroupMasters AssignParameters(GetGroupMastersParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<IEnumerable<GetGroupMastersDto>> Fetch()
        {
            return await Build<DispositionGroupMaster>().SelectTo<GetGroupMastersDto>().ToListAsync();
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            if (!String.IsNullOrEmpty(_params.dispositionAccess))
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                      .Where(x => !x.IsDeleted &&
                                      (string.Equals(x.DispositionAccess, _params.dispositionAccess) ||
                                      string.Equals(x.DispositionAccess, "both")))
                                      .OrderBy(x => x.SrNo);

                return query;
            }
            else
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(x => !x.IsDeleted).OrderBy(x => x.SrNo);

                return query;
            }
        }
    }

    public class GetGroupMastersParams : DtoBridge
    {
        public string? dispositionAccess { get; set; }
    }
}