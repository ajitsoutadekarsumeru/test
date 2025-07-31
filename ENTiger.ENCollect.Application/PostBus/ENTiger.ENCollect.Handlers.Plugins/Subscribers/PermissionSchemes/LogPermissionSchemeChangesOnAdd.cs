using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Logs changes when a new permission scheme is added.
    /// Persists change logs into the database and optionally triggers related workflow events.
    /// </summary>
    public partial class LogPermissionSchemeChangesOnAdd : ILogPermissionSchemeChangesOnAdd
    {
        private readonly ILogger<LogPermissionSchemeChangesOnAdd> _logger;
        private readonly RepoFactory _repoFactory;
        private readonly IFlexHost _flexHost;

        private FlexAppContextBridge? _appContext;
        private PermissionSchemeChangeLog? _permissionSchemeChangeLog;
        private string _eventCondition = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogPermissionSchemeChangesOnAdd"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging activities.</param>
        /// <param name="repoFactory">Repository factory for database operations.</param>
        /// <param name="flexHost">Host interface to access domain models.</param>
        public LogPermissionSchemeChangesOnAdd(
            ILogger<LogPermissionSchemeChangesOnAdd> logger,
            RepoFactory repoFactory,
            IFlexHost flexHost)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _flexHost = flexHost;
        }

        /// <summary>
        /// Executes the process of logging permission scheme changes when a scheme is added.
        /// </summary>
        /// <param name="event">Event data containing change log details.</param>
        /// <param name="serviceBusContext">Context for interacting with the Flex service bus.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual async Task Execute(PermissionSchemeAdded @event, IFlexServiceBusContext serviceBusContext)
        {
            InitializeContext(@event);

            await CreateAndPersistChangeLogAsync(@event);

            // Optional: Fire workflow event after log persistence.
            // await Fire<LogPermissionSchemeChangesOnAdd>(_eventCondition, serviceBusContext);
        }

        /// <summary>
        /// Initializes the application context and repository factory using the event data.
        /// </summary>
        /// <param name="event">Event containing application context information.</param>
        private void InitializeContext(PermissionSchemeAdded @event)
        {
            _appContext = @event.AppContext;
            @event.PermissionSchemeChangeLog.SetAppContext(_appContext);
            _repoFactory.Init(@event.PermissionSchemeChangeLog);
        }

        /// <summary>
        /// Creates and persists the permission scheme change log in the database.
        /// Logs success or failure.
        /// </summary>
        /// <param name="event">Event containing permission scheme change data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task CreateAndPersistChangeLogAsync(PermissionSchemeAdded @event)
        {
            try
            {
                _permissionSchemeChangeLog = _flexHost.GetDomainModel<PermissionSchemeChangeLog>()
                                                      .CreatePermissionSchemeChangeLog(@event.PermissionSchemeChangeLog);

                _repoFactory.GetRepo().InsertOrUpdate(_permissionSchemeChangeLog);

                int recordsAffected = await _repoFactory.GetRepo().SaveAsync();

                LogPersistenceResult(recordsAffected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to persist PermissionSchemeChangeLog for event ID {EventId}. Error: {ErrorMessage}",
                    _permissionSchemeChangeLog.Id, ex.Message);
                throw;

            }
        }


        /// <summary>
        /// Logs the result of the persistence operation.
        /// </summary>
        /// <param name="recordsAffected">Number of records affected by the save operation.</param>
        private void LogPersistenceResult(int recordsAffected)
        {
            if (recordsAffected > 0)
            {
                _logger.LogInformation(
                    "PermissionSchemeChangeLog with ID {LogId} inserted into database.",
                    _permissionSchemeChangeLog.Id);
            }
            else
            {
                _logger.LogWarning(
                    "No records inserted for PermissionSchemeChangeLog with ID {LogId}.",
                    _permissionSchemeChangeLog.Id);
            }
        }
    }
}
