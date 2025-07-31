namespace ENTiger.ENCollect
{
    public interface IRepoFactory
    {
        void Dispose();

        IFlexRepositoryBridge GetRepo();

        RepoFactory Init(ConnectionType type, DtoBridge dto);

        RepoFactory Init(ConnectionType type, PagedQueryParamsDtoBridge dto);

        RepoFactory Init(DtoBridge dto);

        RepoFactory Init(FlexAppContextBridge context);

        RepoFactory Init(FlexEventBridge<FlexAppContextBridge> @event);

        RepoFactory Init(PagedQueryParamsDtoBridge dto);
    }
}