using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AddAgencyPlugin : FlexiPluginBase, IFlexiPlugin<AddAgencyPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a12d59fa1fc5d3e4f716d299e3761c0";
        public override string FriendlyName { get; set; } = "AddAgencyPlugin";

        protected string EventCondition = "";
        protected readonly ILogger<AddAgencyPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected Agency? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected AuditEventData _auditData;
        public AddAgencyPlugin(ILogger<AddAgencyPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddAgencyPostBusDataPacket packet)
        {
            _logger.LogInformation("AddAgencyPlugin : Start");
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<Agency>().AddAgency(packet.Cmd);

            _model.PAN = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(_model.PAN));

            _model.AgencyIdentifications = await AddProfileIdentification(packet.Cmd.Dto, _model.Id);
            int deferredCount = _model.AgencyIdentifications.Where(x => x.IsDeferred != null && x.IsDeferred == true).Count();
            _logger.LogInformation("AddAgencyPlugin : DeferredCount - " + deferredCount);
            if (packet.Cmd.Dto.isSaveAsDraft)
            {
                _model.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencySavedAsDraft>().SetTFlexId(_model.Id).SetStateChangedBy(_model.CreatedBy ?? "");
            }
            else if (deferredCount > 0)
            {
                _model.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencyPendingApprovalWithDeferrals>().SetTFlexId(_model.Id).SetStateChangedBy(_model.CreatedBy ?? "");
            }
            else
            {
                _model.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencyPendingApproval>().SetTFlexId(_model.Id).SetStateChangedBy(_model.CreatedBy ?? "");
            }

            _model.AgencyIdentifications?.SetAddedOrModified();
            if (_model.AgencyIdentifications != null)
            {
                foreach (var t in _model.AgencyIdentifications)
                {
                    t.TFlexId = _model.Id;
                    t.TFlexIdentificationDocs?.SetAddedOrModified();
                    t.TFlexIdentificationDocs?.ToList().ForEach(x =>
                    {
                        x.TFlexIdentificationId = t.Id;
                    });
                }
            }
            _model.TransactionSource = _flexAppContext?.RequestSource;
            _logger.LogInformation("AddAgencyPlugin : Save JSON - " + JsonConvert.SerializeObject(_model));
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(Agency).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Agency).Name, _model.Id);
            }
            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(AddAgencyPostBusDataPacket packet)
        {
            //ignore recurring loop use this settings
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string jsonPatch = JsonConvert.SerializeObject(_model, settings);

            _auditData = new AuditEventData(
                            EntityId: _model?.Id,
                            EntityType: AuditedEntityTypeEnum.Agency.Value,
                            Operation: AuditOperationEnum.Add.Value,
                            JsonPatch: jsonPatch,
                            InitiatorId: _flexAppContext?.UserId,
                            TenantId: _flexAppContext?.TenantId,
                            ClientIP: _flexAppContext?.ClientIP
                        );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<AgencyIdentification>> AddProfileIdentification(AddAgencyDto model, string id)
        {
            _logger.LogInformation("AddAgencyPlugin : AddProfileIdentification - Start");
            List<AgencyIdentification> tflexidentification = new List<AgencyIdentification>();
            if (model.ProfileIdentification != null)
            {
                foreach (var apimodel in model.ProfileIdentification)
                {
                    AgencyIdentification obj = new AgencyIdentification();
                    obj.DeferredTillDate = apimodel.DeferredTillDate;
                    obj.IsDeferred = apimodel.IsDeferred;
                    obj.IsWavedOff = apimodel.IsWavedOff;
                    obj.TFlexIdentificationDocTypeId = apimodel.IdentificationDocTypeId;
                    obj.TFlexIdentificationTypeId = apimodel.IdentificationTypeId;
                    //obj.TFlexId = id;
                    //obj.SetAdded();

                    AgencyIdentificationDoc? docs = await _repoFactory.GetRepo().FindAll<AgencyIdentificationDoc>()
                                                            .Where(x => x.Id == apimodel.IdentificationDocId)
                                                            .FlexNoTracking().FirstOrDefaultAsync();
                    if (docs != null)
                    {
                        obj.TFlexIdentificationDocs = new List<AgencyIdentificationDoc>();
                        obj.TFlexIdentificationDocs.Add(docs);
                    }
                    tflexidentification.Add(obj);
                }
            }
            _logger.LogInformation("AddAgencyPlugin : AddProfileIdentification - End");
            return tflexidentification;
        }
    }
}