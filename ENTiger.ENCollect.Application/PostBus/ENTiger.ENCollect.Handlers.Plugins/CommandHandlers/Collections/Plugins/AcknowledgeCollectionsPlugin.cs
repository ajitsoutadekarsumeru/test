using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// Plugin to acknowledge collections in the system.
    /// </summary>
    public partial class AcknowledgeCollectionsPlugin : FlexiPluginBase, IFlexiPlugin<AcknowledgeCollectionsPostBusDataPacket>
    {
        /// <summary>
        /// Unique identifier for the plugin.
        /// </summary>
        public override string Id { get; set; } = "3a138f3bce8c47f7306e43d611f6b395";

        /// <summary>
        /// Friendly name for the plugin.
        /// </summary>
        public override string FriendlyName { get; set; } = "AcknowledgeCollectionsPlugin";

        /// <summary>
        /// Condition for triggering events after execution.
        /// </summary>
        protected string EventCondition = "";

        private readonly ILogger<AcknowledgeCollectionsPlugin> _logger;
        private readonly IFlexHost _flexHost;
        private readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        protected List<string>? collectionIds;
        /// <summary>
        /// Initializes a new instance of the <see cref="AcknowledgeCollectionsPlugin"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging messages.</param>
        /// <param name="flexHost">Flex host instance for plugin execution.</param>
        /// <param name="repoFactory">Repository factory instance for data operations.</param>
        public AcknowledgeCollectionsPlugin(ILogger<AcknowledgeCollectionsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Executes the plugin logic to acknowledge collections.
        /// </summary>
        /// <param name="packet">The input data packet containing collection IDs.</param>
        public virtual async Task Execute(AcknowledgeCollectionsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);
            
            collectionIds = packet.Cmd.Dto.CollectionIds?.ToList();

            var collections = await _repoFactory.GetRepo().FindAll<Collection>()
                                  .Where(m => collectionIds.Contains(m.Id)) 
                                  .ToListAsync();


            if (collections.Any())
            {
                foreach (var obj in collections)
                {
                    obj.AcknowledgeCollections(_flexAppContext?.UserId);
                    _repoFactory.GetRepo().InsertOrUpdate(obj); 
                }

                // Save changes to the database
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with IDs {EntityIds} updated in Database.", nameof(Collection), string.Join(",", collectionIds));
                    EventCondition = CONDITION_ONSUCCESS;  // Fire event only if records are updated
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with IDs {EntityIds}.", nameof(Collection), string.Join(",", collectionIds));
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with IDs {EntityIds} not found in Database.", nameof(Collection), string.Join(",", collectionIds));
            }

            // Fire event based on execution outcome
            await Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}
