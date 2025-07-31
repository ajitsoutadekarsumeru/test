using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class LogPermissionSchemeChangesOnUpdate : ILogPermissionSchemeChangesOnUpdate
    {
        private readonly ILogger<LogPermissionSchemeChangesOnUpdate> _logger;
        private readonly RepoFactory _repoFactory;
        private readonly IFlexHost _flexHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogPermissionSchemeChangesOnUpdate"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging messages.</param>
        /// <param name="repoFactory">Factory for creating repositories.</param>
        /// <param name="flexHost">Flex host instance for domain model interactions.</param>
        public LogPermissionSchemeChangesOnUpdate(
            ILogger<LogPermissionSchemeChangesOnUpdate> logger,
            RepoFactory repoFactory,
            IFlexHost flexHost)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _flexHost = flexHost;
        }

        /// <summary>
        /// Executes the process of logging permission scheme changes when a scheme is updated.
        /// </summary>
        /// <param name="event">Event data containing change log details.</param>
        /// <param name="serviceBusContext">Context for interacting with the Flex service bus.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(PermissionSchemeUpdated @event, IFlexServiceBusContext serviceBusContext)
        {
            InitializeContext(@event);
            await CreateAndPersistChangeLogAsync(@event);

            // Optional: Fire workflow event after log persistence.
            // await Fire<LogPermissionSchemeChangesOnAdd>(@eventCondition, serviceBusContext);
        }

        /// <summary>
        /// Initializes the application context and repository factory using the event data.
        /// </summary>
        /// <param name="event">Event containing application context information.</param>
        private void InitializeContext(PermissionSchemeUpdated @event)
        {
            var appContext = @event.AppContext;
            @event.PermissionSchemeChangeLog.SetAppContext(appContext);
            _repoFactory.Init(@event.PermissionSchemeChangeLog);
        }

        /// <summary>
        /// Creates and persists the permission scheme change log in the database.
        /// Logs success or failure.
        /// </summary>
        /// <param name="event">Event containing permission scheme change data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task CreateAndPersistChangeLogAsync(PermissionSchemeUpdated @event)
        {
            try
            {
                var changeLog = _flexHost.GetDomainModel<PermissionSchemeChangeLog>()
                                         .CreatePermissionSchemeChangeLog(@event.PermissionSchemeChangeLog);

                _repoFactory.GetRepo().InsertOrUpdate(changeLog);

                int recordsAffected = await _repoFactory.GetRepo().SaveAsync();

                LogPersistenceResult(recordsAffected, changeLog.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to persist PermissionSchemeChangeLog for event. Error: {ErrorMessage}",
                    ex.Message);
            }
        }

        /// <summary>
        /// Logs the result of the persistence operation.
        /// </summary>
        /// <param name="recordsAffected">Number of records affected by the save operation.</param>
        /// <param name="logId">Identifier of the permission scheme change log.</param>
        private void LogPersistenceResult(int recordsAffected, string logId)
        {
            if (recordsAffected > 0)
            {
                _logger.LogInformation(
                    "PermissionSchemeChangeLog with ID {LogId} inserted into database.",
                    logId);
            }
            else
            {
                _logger.LogWarning(
                    "No records inserted for PermissionSchemeChangeLog with ID {LogId}.",
                    logId);
            }
        }
    }
}
