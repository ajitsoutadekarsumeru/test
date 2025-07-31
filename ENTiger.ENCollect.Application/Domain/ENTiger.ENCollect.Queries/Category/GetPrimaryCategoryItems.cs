using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CategoryModule;

public class GetPrimaryCategoryItems : FlexiQueryEnumerableBridgeAsync<CategoryItem, GetPrimaryCategoryItemsDto>
{
    protected readonly ILogger<GetPrimaryCategoryItems> _logger;
    protected GetPrimaryCategoryItemsParams _params;
    protected readonly IRepoFactory _repoFactory;
    
    public GetPrimaryCategoryItems(ILogger<GetPrimaryCategoryItems> logger, IRepoFactory repoFactory)
    {
        _logger = logger;
        _repoFactory = repoFactory;
    }

    public virtual GetPrimaryCategoryItems AssignParameters(GetPrimaryCategoryItemsParams @params)
    {
        _params = @params;
        return this;
    }


    public override async Task<IEnumerable<GetPrimaryCategoryItemsDto>> Fetch()
    {
        var result = await Build<CategoryItem>().SelectTo<GetPrimaryCategoryItemsDto>().ToListAsync();

        return result.OrderBy(a => a.Name);
    }

    protected override IQueryable<T> Build<T>()
    {
        _repoFactory.Init(_params);

        IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().OnlyActiveItems().ByCategoryMaster(_params.CategoryMasterId);

        return query;
    }
}

public class GetPrimaryCategoryItemsParams : DtoBridge
{
    public string? CategoryMasterId { get; set; }
}