using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect
{
    public class RepoFactory : IDisposable, IRepoFactory
    {
        private bool disposed = false;

        private IFlexRepositoryBridge _repo;
        private IConnectionProvider<FlexAppContextBridge> _connectionProvider;

        /// <summary>
        /// For Context in Input Parameter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public RepoFactory Init(ConnectionType type, PagedQueryParamsDtoBridge dto)
        {
            switch (type)
            {
                case ConnectionType.ReadDb:
                    _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IReadDbConnectionProviderBridge>();
                    _connectionProvider.ConfigureDbConnectionString(dto.GetAppContext());
                    _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
                    _repo.InitializeConnection(_connectionProvider);
                    break;

                case ConnectionType.WriteDb:
                    _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IWriteDbConnectionProviderBridge>();
                    _connectionProvider.ConfigureDbConnectionString(dto.GetAppContext());

                    _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
                    _repo.InitializeConnection(_connectionProvider);
                    break;

                default:
                    throw new InvalidOperationException("Invalid connection type.");
            }

            return this;
        }

        /// <summary>
        /// For Context in Input Parameter without passing ConnectionType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public RepoFactory Init(PagedQueryParamsDtoBridge dto)
        {
            _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IReadDbConnectionProviderBridge>();
            _connectionProvider.ConfigureDbConnectionString(dto.GetAppContext());

            _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
            _repo.InitializeConnection(_connectionProvider);
            return this;
        }

        /// <summary>
        /// For Context in Input Parameter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public RepoFactory Init(ConnectionType type, DtoBridge dto)
        {
            switch (type)
            {
                case ConnectionType.ReadDb:
                    _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IReadDbConnectionProviderBridge>();
                    _connectionProvider.ConfigureDbConnectionString(dto.GetAppContext());
                    _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
                    _repo.InitializeConnection(_connectionProvider);
                    break;

                case ConnectionType.WriteDb:
                    _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IWriteDbConnectionProviderBridge>();
                    _connectionProvider.ConfigureDbConnectionString(dto.GetAppContext());

                    _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
                    _repo.InitializeConnection(_connectionProvider);
                    break;

                default:
                    throw new InvalidOperationException("Invalid connection type.");
            }

            return this;
        }

        /// <summary>
        /// For Context in Input Parameter without passing ConnectionType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public RepoFactory Init(DtoBridge dto)
        {
            _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IWriteDbConnectionProviderBridge>();
            _connectionProvider.ConfigureDbConnectionString(dto.GetAppContext());

            _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
            _repo.InitializeConnection(_connectionProvider);
            return this;
        }

        /// <summary>
        /// For Context in Input Parameter without passing ConnectionType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public RepoFactory Init(FlexEventBridge<FlexAppContextBridge> @event)
        {
            _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IWriteDbConnectionProviderBridge>();
            _connectionProvider.ConfigureDbConnectionString(@event.AppContext);

            _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
            _repo.InitializeConnection(_connectionProvider);
            return this;
        }

        public RepoFactory Init(FlexAppContextBridge context)
        {
            _connectionProvider = FlexContainer.ServiceProvider.GetRequiredService<IWriteDbConnectionProviderBridge>();
            _connectionProvider.ConfigureDbConnectionString(context);

            _repo = FlexContainer.ServiceProvider.GetRequiredService<IFlexRepositoryBridge>();
            _repo.InitializeConnection(_connectionProvider);

            return this;
        }

        public IFlexRepositoryBridge GetRepo()
        {
            return _repo;
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _repo?.Dispose();
                }

                // Free unmanaged resources (if any) here and set large fields to null

                disposed = true;
            }
        }

        // Destructor
        ~RepoFactory()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }
    }

    public enum ConnectionType
    {
        ReadDb,
        WriteDb
    }
}