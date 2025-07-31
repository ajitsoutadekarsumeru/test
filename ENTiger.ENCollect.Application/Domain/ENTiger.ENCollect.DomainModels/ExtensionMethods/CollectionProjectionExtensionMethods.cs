namespace ENTiger.ENCollect.DomainModels.ExtensionMethods;
public static class CollectionProjectionExtensionMethods
{


    public static IQueryable<T> ByCollectionProjectionCollectionIds<T>(this IQueryable<T> query, List<string> collectionIds) where T : CollectionProjection
    {
        if (collectionIds != null && collectionIds.Any())
        {
            query = query.Where(t => collectionIds.Contains(t.CollectionId));
        }
        return query;
    }

    public static IQueryable<T> ByCollectionProjectionCollectionId<T>(this IQueryable<T> query, string collectionId) where T : CollectionProjection
    {
        if (!string.IsNullOrEmpty(collectionId))
        {
            query = query.Where(t => t.CollectionId == collectionId);
        }
        return query;
    }
}
