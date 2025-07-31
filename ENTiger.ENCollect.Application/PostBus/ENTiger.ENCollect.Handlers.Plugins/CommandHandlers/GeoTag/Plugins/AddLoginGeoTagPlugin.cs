using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class AddLoginGeoTagPlugin : FlexiPluginBase, IFlexiPlugin<AddLoginGeoTagPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13679ba2958c902df8506008059a1d";
        public override string FriendlyName { get; set; } = "AddLoginGeoTagPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddLoginGeoTagPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected GeoTagDetails? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddLoginGeoTagPlugin(ILogger<AddLoginGeoTagPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddLoginGeoTagPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            GeoTagDetails checkFirstGeotagDetails;

            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);

            checkFirstGeotagDetails = await _repoFactory.GetRepo().FindAll<GeoTagDetails>()
                    .Where(a => a.CreatedDate >= startDate && a.CreatedDate < endDate && a.CreatedBy == _flexAppContext.UserId).FirstOrDefaultAsync();

            if (checkFirstGeotagDetails == null)
            {
                _logger.LogDebug("AddFirstLoginGeoTagFFPlugin: ");
                _model = _flexHost.GetDomainModel<GeoTagDetails>().AddLoginGeoTag(packet.Cmd);

                _repoFactory.GetRepo().InsertOrUpdate(_model);
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(GeoTagDetails).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(GeoTagDetails).Name, _model.Id);
                }
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}