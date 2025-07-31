using Microsoft.EntityFrameworkCore;
namespace ENTiger.ENCollect.CategoryModule;

public class GetSecondaryCategoryById : FlexiQueryBridgeAsync<CategoryItem, GetSecondaryCategoryByIdDto>
{
    protected readonly ILogger<GetSecondaryCategoryById> _logger;
    protected GetSecondaryCategoryByIdParams _params;
    protected readonly IRepoFactory _repoFactory;

    public GetSecondaryCategoryById(ILogger<GetSecondaryCategoryById> logger, IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;
    }

    public virtual GetSecondaryCategoryById AssignParameters(GetSecondaryCategoryByIdParams @params)
    {
        _params = @params;
        return this;
    }

    public override async Task<GetSecondaryCategoryByIdDto> Fetch()
    {
        var result = await Build<CategoryItem>().SelectTo<GetSecondaryCategoryByIdDto>().FirstOrDefaultAsync();

        return result;
    }

    protected override IQueryable<T> Build<T>()
    {
        _repoFactory.Init(_params);

        IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().OnlyActiveItems().ByTFlexId(_params.Id);

        return query;
    }
}

public class GetSecondaryCategoryByIdParams : DtoBridge
{
    public string? Id { get; set; }
}