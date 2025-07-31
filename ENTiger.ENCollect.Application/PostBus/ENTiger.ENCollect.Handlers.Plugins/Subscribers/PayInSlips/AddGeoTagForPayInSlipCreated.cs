using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AddGeoTagForPayInSlipCreated : IAddGeoTagForPayInSlipCreated
    {
        protected readonly ILogger<AddGeoTagForPayInSlipCreated> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public AddGeoTagForPayInSlipCreated(ILogger<AddGeoTagForPayInSlipCreated> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PayInSlipCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:

            var slip = await repo.GetRepo().FindAll<PayInSlip>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(slip.Lattitude) && !string.IsNullOrEmpty(slip.Longitude))
            {
                GeoTagDetails _model = new GeoTagDetails()
                {
                    Latitude = Convert.ToDouble(slip.Lattitude),
                    Longitude = Convert.ToDouble(slip.Longitude),
                    ApplicationUserId = slip.CreatedBy
                };
                _model.SetCreatedBy(slip.CreatedBy);
                _model.SetAdded();

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
            else
            {
                _logger.LogWarning("Latitude and Longitude values missing for {Entity} with {EntityId}", typeof(PayInSlip).Name, @event.Id);
            }

            await this.Fire<AddGeoTagForPayInSlipCreated>(EventCondition, serviceBusContext);
        }
    }
}