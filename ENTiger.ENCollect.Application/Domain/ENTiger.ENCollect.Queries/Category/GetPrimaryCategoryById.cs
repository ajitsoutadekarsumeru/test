using Microsoft.EntityFrameworkCore;
namespace ENTiger.ENCollect.CategoryModule;


public class GetPrimaryCategoryById : FlexiQueryBridgeAsync<CategoryItem, GetPrimaryCategoryByIdDto>
{
    protected readonly ILogger<GetPrimaryCategoryById> _logger;
    protected GetPrimaryCategoryByIdParams _params;
    protected readonly IRepoFactory _repoFactory;

    public GetPrimaryCategoryById(ILogger<GetPrimaryCategoryById> logger, IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;
    }

    public virtual GetPrimaryCategoryById AssignParameters(GetPrimaryCategoryByIdParams @params)
    {
        _params = @params;
        return this;
    }

    public override async Task<GetPrimaryCategoryByIdDto> Fetch()
    {
        var result = await Build<CategoryItem>().SelectTo<GetPrimaryCategoryByIdDto>().FirstOrDefaultAsync();

        return result;
    }

    protected override IQueryable<T> Build<T>()
    {
        _repoFactory.Init(_params);

        IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().OnlyActiveItems().ByTFlexId(_params.Id);

        return query;
    }
}

public class GetPrimaryCategoryByIdParams : DtoBridge
{
    public string? Id { get; set; }
}