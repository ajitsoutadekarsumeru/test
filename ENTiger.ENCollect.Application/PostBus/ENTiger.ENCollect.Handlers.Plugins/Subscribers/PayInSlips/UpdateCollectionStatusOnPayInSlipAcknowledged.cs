using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    
    public partial class UpdateCollectionStatusOnPayInSlipAcknowledged : IUpdateCollectionStatusForAckPayinslips
    {
        protected readonly ILogger<UpdateCollectionStatusOnPayInSlipAcknowledged> _logger;
        protected string EventCondition = "";
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateCollectionStatusOnPayInSlipAcknowledged(ILogger<UpdateCollectionStatusOnPayInSlipAcknowledged> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        
        public async Task Execute(UpdateCollectionStatusOnPayInSlipAcknowledgedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _repoFactory.Init(@event);

            string payinslipid = @event.PayInSlipId;

            // Get list of batchIds
            string collectionbatchid = _repoFactory.GetRepo().FindAll<CollectionBatch>()
                                    .ByCollectionBatchPaySlipId(payinslipid).Select(a=>a.Id).FirstOrDefault();


            if (collectionbatchid != null)
            {
                // Get list of collectionids
                var collections = _repoFactory.GetRepo().FindAll<Collection>()
                                             .ByCollectionBatchId(collectionbatchid).ToList();

                //Update collection status to WithBank
                collections.ForEach(a =>
                {
                    a.Status = CollectionStatusEnum.withBank.Value;
                    a.SetAddedOrModified();
                    _repoFactory.GetRepo().InsertOrUpdate(a);
                });

                await _repoFactory.GetRepo().SaveAsync();
            }

        }
    }

}
