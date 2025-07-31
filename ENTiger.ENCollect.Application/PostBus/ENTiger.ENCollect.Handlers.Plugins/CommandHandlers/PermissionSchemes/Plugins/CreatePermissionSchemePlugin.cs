using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Plugin to handle the creation of a new permission scheme and trigger audit and workflow events.
    /// </summary>
    public partial class CreatePermissionSchemePlugin : FlexiPluginBase, IFlexiPlugin<CreatePermissionSchemePostBusDataPacket>
    {
        /// <inheritdoc />
        public override string Id { get; set; } = "3a1947f7c0710a65ad364c24e403c1b5";

        /// <inheritdoc />
        public override string FriendlyName { get; set; } = nameof(CreatePermissionSchemePlugin);

        private readonly ILogger<CreatePermissionSchemePlugin> _logger;
        private readonly IFlexHost _flexHost;
        private readonly RepoFactory _repoFactory;

        private string _eventCondition = string.Empty;
        private PermissionSchemes? _permissionScheme;
        private FlexAppContextBridge? _appContext;
        private PermissionSchemeChangeLogDto? _permissionSchemeChangeLog;
        private AuditEventData? _auditData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePermissionSchemePlugin"/> class.
        /// </summary>
        /// <param name="logger">Logger for plugin activities.</param>
        /// <param name="flexHost">Host interface for domain model access.</param>
        /// <param name="repoFactory">Repository factory for database operations.</param>
        public CreatePermissionSchemePlugin(ILogger<CreatePermissionSchemePlugin> logger, IFlexHost flexHost, RepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Executes the plugin to create a new permission scheme and trigger related audit and workflow events.
        /// </summary>
        /// <param name="packet">Data packet containing command and context information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public virtual async Task Execute(CreatePermissionSchemePostBusDataPacket packet)
        {
            InitializeContext(packet);

            bool isCreated = await CreatePermissionSchemeAsync(packet);

            if (isCreated)
            {
                await TriggerPermissionAddedEventAsync(packet); // change the name 
                await GenerateAndSendAuditEventAsync(packet);
                
            }
        }

        /// <summary>
        /// Initializes the application context and repository with provided packet data.
        /// </summary>
        /// <param name="packet">Packet containing command DTO and context.</param>
        private void InitializeContext(CreatePermissionSchemePostBusDataPacket packet)
        {
            _appContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);
        }
    
        /// <summary>
        /// Creates a permission scheme, persists it to the database, and logs its creation.
        /// </summary>
        /// <param name="packet">Packet containing the command to execute.</param>
        /// <returns>
        /// True if the permission scheme was successfully created and saved; otherwise, false.
        /// </returns>
        private async Task<bool> CreatePermissionSchemeAsync(CreatePermissionSchemePostBusDataPacket packet)
        {
            try
            {
                _permissionScheme = _flexHost.GetDomainModel<PermissionSchemes>()
                                             .CreatePermissionScheme(packet.Cmd);

                _repoFactory.GetRepo().InsertOrUpdate(_permissionScheme);

                int recordsAffected = await _repoFactory.GetRepo().SaveAsync();

                if (recordsAffected > 0)
                {
                    await LogPermissionSchemeCreationAsync(packet.Cmd.Dto, _permissionScheme.Id);
                    _eventCondition = CONDITION_ONSUCCESS;

                    _logger.LogInformation($"PermissionScheme with ID {_permissionScheme.Id} inserted into database.");

                    return true;
                }
                else
                {

                    _logger.LogWarning($"No records inserted for PermissionScheme with ID {_permissionScheme.Id}.");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating and persisting permission scheme.");
                throw;
            }
        }


        /// <summary>
        /// Logs the creation details of the permission scheme including added permissions.
        /// </summary>
        /// <param name="dto">DTO containing scheme creation details.</param>
        /// <param name="schemeId">Unique identifier of the created permission scheme.</param>
        private async Task LogPermissionSchemeCreationAsync(CreatePermissionSchemeDto dto, string schemeId)
        {
            _permissionSchemeChangeLog = new PermissionSchemeChangeLogDto
            {
                PermissionSchemeId = schemeId,
                ChangeType = PermissionSchemeChangeTypeEnum.Create.Value,
                Remarks = dto.Remarks
            };
        }


        /// <summary>
        /// Generates an audit event for the created permission scheme and sends it through the service bus.
        /// </summary>
        /// <param name="packet">Packet containing context information for the audit event.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateAndSendAuditEventAsync(CreatePermissionSchemePostBusDataPacket packet)
        {
            _auditData = BuildAuditEvent();

            _eventCondition = CONDITION_ONAUDITREQUEST;

            await Fire(_eventCondition, packet.FlexServiceBusContext);
        }

        /// <summary>
        /// Builds the audit event data object based on the created permission scheme.
        /// </summary>
        /// <returns>An <see cref="AuditEventData"/> object with event details.</returns>
        private AuditEventData BuildAuditEvent()
        {
            var jsonPatch = SerializeModelIgnoringReferenceLoops(_permissionScheme!);

            return new AuditEventData(
                EntityId: _permissionScheme.Id,
                EntityType: AuditedEntityTypeEnum.PermissionScheme.ToString(),
                Operation: AuditOperationEnum.Add.ToString(),
                JsonPatch: jsonPatch,
                InitiatorId: _appContext?.UserId,
                TenantId: _appContext?.TenantId,
                ClientIP: _appContext?.ClientIP
            );
        }

        /// <summary>
        /// Triggers a workflow event based on the current event condition.
        /// </summary>
        /// <param name="packet">Packet containing Flex service bus context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task TriggerPermissionAddedEventAsync(CreatePermissionSchemePostBusDataPacket packet)
        {
            await Fire(_eventCondition, packet.FlexServiceBusContext);
        }

        /// <summary>
        /// Serializes the specified model to JSON while ignoring reference loops.
        /// </summary>
        /// <param name="model">The model to serialize.</param>
        /// <returns>A JSON string representing the model.</returns>
        private string SerializeModelIgnoringReferenceLoops(object model)
        {
            return JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }

}