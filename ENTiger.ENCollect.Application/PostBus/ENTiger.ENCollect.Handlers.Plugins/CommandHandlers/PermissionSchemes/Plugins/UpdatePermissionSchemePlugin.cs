using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// Plugin to handle updating of permission schemes in the system.
    /// </summary>
    public partial class UpdatePermissionSchemePlugin : FlexiPluginBase, IFlexiPlugin<UpdatePermissionSchemePostBusDataPacket>
    {
        /// <inheritdoc/>
        public override string Id { get; set; } = "3a1947f833baf3754b7677090a03a43f";

        /// <inheritdoc/>
        public override string FriendlyName { get; set; } = "UpdatePermissionSchemePlugin";

        private readonly ILogger<UpdatePermissionSchemePlugin> _logger;
        private readonly IFlexHost _flexHost;
        private readonly RepoFactory _repoFactory;

        private string _eventCondition = string.Empty;
        private PermissionSchemes? _permissionScheme;
        private FlexAppContextBridge? _appContext;
        private PermissionSchemeChangeLogDto? _permissionSchemeChangeLog;
        private AuditEventData? _auditData;
        private readonly IPermissionRepository _permissionRepository;

        private List<string>? _addedPermissions;
        private List<string>? _removedPermissions;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePermissionSchemePlugin"/> class.
        /// </summary>
        public UpdatePermissionSchemePlugin(ILogger<UpdatePermissionSchemePlugin> logger, IFlexHost flexHost, RepoFactory repoFactory, IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// Executes the update permission scheme plugin logic.
        /// </summary>
        /// <param name="packet">The data packet containing update details.</param>
        public async Task Execute(UpdatePermissionSchemePostBusDataPacket packet)
        {
            InitializeContext(packet);
            bool isUpdated = await UpdatePermissionSchemeAsync(packet);

            if (isUpdated)
            {
                await TriggerPermissionUpdatedEventAsync(packet);
                await GenerateAndSendAuditEventAsync(packet);
            }
        }

        /// <summary>
        /// Initializes repository and application context from packet.
        /// </summary>
        /// <param name="packet">The data packet containing context details.</param>
        private void InitializeContext(UpdatePermissionSchemePostBusDataPacket packet)
        {
            _appContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);
        }

        /// <summary>
        /// Updates the permission scheme entity and persists changes to the database.
        /// </summary>
        /// <param name="packet">The data packet containing update details.</param>
        /// <returns>A task returning true if update is successful; otherwise false.</returns>
        private async Task<bool> UpdatePermissionSchemeAsync(UpdatePermissionSchemePostBusDataPacket packet)
        {
            try
            {
                _permissionScheme = await _repoFactory.GetRepo()
                    .FindAll<PermissionSchemes>()
                    .Include(m => m.EnabledPermissions)
                    .Where(m => m.Id == packet.Cmd.Dto.Id)
                    .FirstOrDefaultAsync();

                _removedPermissions = _permissionScheme?.EnabledPermissions?.Select(p => p.PermissionId).ToList();
                _addedPermissions = packet.Cmd.Dto.EnabledPermissionIds.Distinct().ToList();

                if (_permissionScheme != null)
                {
                    _permissionScheme.UpdatePermissionScheme(packet.Cmd);

                    _repoFactory.GetRepo().InsertOrUpdate(_permissionScheme);
                    int recordsAffected = await _repoFactory.GetRepo().SaveAsync();

                    if (recordsAffected > 0)
                    {
                        await LogPermissionSchemeChangeAsync(
                            packet.Cmd.Dto,
                            _permissionScheme.Id,
                            _removedPermissions,
                            newPermissionIds: packet.Cmd.Dto.EnabledPermissionIds?.Distinct().ToList() ?? new List<string>());

                        _eventCondition = CONDITION_ONSUCCESS;

                        _logger.LogInformation($"PermissionScheme with ID {_permissionScheme.Id} updated in database.");
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning($"No records updated for PermissionScheme with ID {_permissionScheme.Id}.");
                        return false;
                    }
                }
                else
                {
                    _logger.LogWarning($"PermissionScheme with ID {packet.Cmd.Dto.Id} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating and persisting permission scheme.");
                return false;
            }
        }

        /// <summary>
        /// Logs the permission scheme change with added and removed permissions.
        /// </summary>
        /// <param name="dto">The update permission scheme DTO.</param>
        /// <param name="schemeId">The ID of the permission scheme.</param>
        /// <param name="oldPermissionIds">List of old permission IDs before update.</param>
        /// <param name="newPermissionIds">List of new permission IDs after update.</param>
        private async Task LogPermissionSchemeChangeAsync(UpdatePermissionSchemeDto dto, string schemeId, List<string> oldPermissionIds, List<string> newPermissionIds)
        {
            // Find Removed and Added permissions
            var removedPermissionIds = oldPermissionIds.Except(newPermissionIds).ToList();
            var addedPermissionIds = newPermissionIds.Except(oldPermissionIds).ToList();

            _removedPermissions = await GetPermissionNamesAsync(removedPermissionIds);
            _addedPermissions = await GetPermissionNamesAsync(addedPermissionIds);

            _permissionSchemeChangeLog = new PermissionSchemeChangeLogDto
            {
                PermissionSchemeId = schemeId,
                AddedPermissions = string.Join(",", _addedPermissions),
                RemovedPermissions = string.Join(",", _removedPermissions),
                ChangeType = PermissionSchemeChangeTypeEnum.Edit.Value,
                Remarks = dto.Remarks
            };
        }

        /// <summary>
        /// Retrieves permission names based on the given permission IDs.
        /// </summary>
        /// <param name="permissionIds">The list of permission IDs.</param>
        /// <returns>List of permission names.</returns>
        private async Task<List<string>> GetPermissionNamesAsync(List<string> permissionIds)
        {
            // Fetch the Permissions entities (await the Task first)
            var permissions = await _permissionRepository.GetPermissionsByIdsAsync(_appContext, permissionIds);

            // Use LINQ to select only the Name property from the Permissions entities
            var permissionNames = permissions
                .Select(p => p.Name)  // Select the Name property
                .ToList();            // Convert to List<string>

            return permissionNames;
        }



        /// <summary>
        /// Generates and sends audit event capturing the permission scheme changes.
        /// </summary>
        /// <param name="packet">The data packet containing context.</param>
        private async Task GenerateAndSendAuditEventAsync(UpdatePermissionSchemePostBusDataPacket packet)
        {
            _auditData = BuildAuditEvent();
            _eventCondition = CONDITION_ONAUDITREQUEST;

            await Fire(_eventCondition, packet.FlexServiceBusContext);
        }

        /// <summary>
        /// Builds the audit event data with permission scheme changes.
        /// </summary>
        /// <returns>The populated <see cref="AuditEventData"/> object.</returns>
        private AuditEventData BuildAuditEvent()
        {
            // Extended Audit log to also include Added/Removed Permissions
            var auditPayload = new
            {
                PermissionScheme = _permissionScheme,
                AddedPermissions = _addedPermissions,
                RemovedPermissions = _removedPermissions
            };

            string jsonPatch = SerializeModelIgnoringReferenceLoops(auditPayload);

            return new AuditEventData(
                EntityId: _permissionScheme!.Id,
                EntityType: AuditedEntityTypeEnum.PermissionScheme.ToString(),
                Operation: AuditOperationEnum.Edit.ToString(),
                JsonPatch: jsonPatch,
                InitiatorId: _appContext?.UserId,
                TenantId: _appContext?.TenantId,
                ClientIP: _appContext?.ClientIP
            );
        }

        /// <summary>
        /// Triggers the workflow event based on the current event condition.
        /// </summary>
        /// <param name="packet">The data packet containing service bus context.</param>
        private async Task TriggerPermissionUpdatedEventAsync(UpdatePermissionSchemePostBusDataPacket packet)
        {
            await Fire(_eventCondition, packet.FlexServiceBusContext);
        }

        /// <summary>
        /// Serializes the model while ignoring reference loops to avoid JSON errors.
        /// </summary>
        /// <param name="model">The model object to serialize.</param>
        /// <returns>Serialized JSON string.</returns>
        private string SerializeModelIgnoringReferenceLoops(object model)
        {
            return JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
