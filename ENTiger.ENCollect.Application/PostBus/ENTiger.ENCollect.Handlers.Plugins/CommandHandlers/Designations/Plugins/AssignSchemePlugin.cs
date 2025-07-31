using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// Plugin to assign schemes to designations in ENCollect.
    /// Handles fetching, updating, and saving Designation entities.
    /// </summary>
    public partial class AssignSchemePlugin : FlexiPluginBase, IFlexiPlugin<AssignSchemePostBusDataPacket>
    {
        /// <summary>
        /// Unique identifier for this plugin.
        /// </summary>
        public override string Id { get; set; } = "3a1947f8acc5cc5a1167ed5911bfa1e6";

        /// <summary>
        /// Display-friendly name of the plugin.
        /// </summary>
        public override string FriendlyName { get; set; } = "AssignSchemePlugin";

        /// <summary>
        /// Placeholder property for event conditions (reserved for future use).
        /// </summary>
        protected string EventCondition = "";

        /// <summary>
        /// Logger instance for writing diagnostic and operational logs.
        /// </summary>
        protected readonly ILogger<AssignSchemePlugin> _logger;

        /// <summary>
        /// FlexHost instance that provides plugin host context.
        /// </summary>
        protected readonly IFlexHost _flexHost;

        /// <summary>
        /// Factory for creating repository instances for database operations.
        /// </summary>
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// Represents the Designation entity fetched from database.
        /// </summary>
        protected Designation? _model;

        /// <summary>
        /// Holds the application context during plugin execution.
        /// </summary>
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignSchemePlugin"/> class.
        /// </summary>
        /// <param name="logger">Logger for writing logs.</param>
        /// <param name="flexHost">Host context for Flexi plugins.</param>
        /// <param name="repoFactory">Factory for repository instances.</param>
        public AssignSchemePlugin(ILogger<AssignSchemePlugin> logger, IFlexHost flexHost, RepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Executes the plugin logic to assign a scheme to a Designation entity.
        /// </summary>
        /// <param name="packet">The incoming data packet containing command and DTO.</param>
        public virtual async Task Execute(AssignSchemePostBusDataPacket packet)
        {
            // Mandatory context initialization (do not remove this line)
            _flexAppContext = packet.Cmd.Dto.GetAppContext();

            // Initialize repository factory with DTO context
            _repoFactory.Init(packet.Cmd.Dto);

            // Fetch Designation entity matching the provided Id
            _model = _repoFactory.GetRepo().FindAll<Designation>().Where(m => m.Id == packet.Cmd.Dto.Id).FirstOrDefault(); // single or default needs to make

            if (_model != null)
            {
                // Assign scheme logic applied on the fetched entity
                _model.AssignScheme(packet.Cmd);

                // Insert or update entity in repository
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                // Save changes asynchronously
                int records = await _repoFactory.GetRepo().SaveAsync();

                if (records > 0)
                {
                    // Log success
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Designation).Name, _model.Id);
                }
                else
                {
                    // Log warning when no records are updated
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Designation).Name, _model.Id); // log error needs to set
                }
            }
            else
            {
                // Log warning when entity is not found
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Designation).Name, packet.Cmd.Dto.Id);// log error needs to set
            }
        }
    }
}
