namespace ENTiger.ENCollect
{
    public interface ICollectionPoster
    {
        Task PostCollectionAsync(CollectionDtoWithId collection);

    }
}
