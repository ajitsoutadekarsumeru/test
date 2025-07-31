using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class UpdateCodeOfConductPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCodeOfConductPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a180bf0748089781c7fda47fa9292ac";
        public override string FriendlyName { get; set; } = "UpdateCodeOfConductPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<UpdateCodeOfConductPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected ApplicationUser? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateCodeOfConductPlugin(ILogger<UpdateCodeOfConductPlugin> logger, 
            IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateCodeOfConductPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(m => m.Id == _flexAppContext.UserId).FirstOrDefaultAsync();
            
            if (_model != null)
            {
                _model.UpdateCodeOfConduct(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(ApplicationUser).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(ApplicationUser).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(ApplicationUser).Name, _flexAppContext.UserId);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

 
            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}