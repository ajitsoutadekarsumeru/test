using Microsoft.EntityFrameworkCore;
namespace ENTiger.ENCollect.CategoryModule;

public class GetSecondaryCategoryByParentId : FlexiQueryEnumerableBridgeAsync<CategoryItem, GetSecondaryCategoryByParentIdDto>
{
    protected readonly ILogger<GetSecondaryCategoryByParentId> _logger;
    protected GetSecondaryCategoryByParentIdParams _params;
    protected readonly IRepoFactory _repoFactory;

    public GetSecondaryCategoryByParentId(ILogger<GetSecondaryCategoryByParentId> logger, IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;
    }

    public virtual GetSecondaryCategoryByParentId AssignParameters(GetSecondaryCategoryByParentIdParams @params)
    {
        _params = @params;
        return this;
    }

    public override async Task<IEnumerable<GetSecondaryCategoryByParentIdDto>> Fetch()
    {
        var result = await Build<CategoryItem>().SelectTo<GetSecondaryCategoryByParentIdDto>().ToListAsync();

        return result;
    }

    protected override IQueryable<T> Build<T>()
    {
        _repoFactory.Init(_params);

        IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().OnlyActiveItems().ByParent(_params.ParentId);

        return query;
    }
}

public class GetSecondaryCategoryByParentIdParams : DtoBridge
{
    public string? ParentId { get; set; }
}