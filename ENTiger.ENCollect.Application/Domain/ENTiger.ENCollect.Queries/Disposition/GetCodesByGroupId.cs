using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.DispositionModule
{
    public class GetCodesByGroupId : FlexiQueryEnumerableBridgeAsync<DispositionCodeMaster, GetCodesByGroupIdDto>
    {
        protected readonly ILogger<GetCodesByGroupId> _logger;
        protected GetCodesByGroupIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        public GetCodesByGroupId(ILogger<GetCodesByGroupId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual GetCodesByGroupId AssignParameters(GetCodesByGroupIdParams @params)
        {
            _params = @params;
            return this;
        }

        public override async Task<IEnumerable<GetCodesByGroupIdDto>> Fetch()
        {
            var result = await Build<DispositionCodeMaster>().SelectTo<GetCodesByGroupIdDto>().ToListAsync();

            return result;
        }

        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            if (!String.IsNullOrEmpty(_params.DispositionAccess))
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                     .Where(x => !x.IsDeleted &&
                                     x.DispositionGroupMasterId == _params.DispositionGroupId &&
                                     (string.Equals(x.DispositionAccess, _params.DispositionAccess) ||
                                      string.Equals(x.DispositionAccess, "both")))
                                     .OrderBy(x => x.SrNo);

                return query;
            }
            else
            {
                IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                            .Where(x => !x.IsDeleted && x.DispositionGroupMasterId == _params.DispositionGroupId)
                                            .OrderBy(x => x.SrNo);
                return query;
            }
        }
    }

    public class GetCodesByGroupIdParams : DtoBridge
    {
        public string? DispositionGroupId { get; set; }
        public string? DispositionAccess { get; set; }
    }
}