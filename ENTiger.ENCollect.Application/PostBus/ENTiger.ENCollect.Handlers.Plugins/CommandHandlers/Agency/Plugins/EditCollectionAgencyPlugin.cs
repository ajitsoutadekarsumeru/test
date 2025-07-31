using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class EditCollectionAgencyPlugin : FlexiPluginBase, IFlexiPlugin<EditCollectionAgencyPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1317a25bdf79a53a91fa69bab6f3bb";
        public override string FriendlyName { get; set; } = "EditCollectionAgencyPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<EditCollectionAgencyPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Agency? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly UserFieldSettings _userFieldSettings;

        protected Agency? _agency;
        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;

        public EditCollectionAgencyPlugin(ILogger<EditCollectionAgencyPlugin> logger, IFlexHost flexHost
                ,IRepoFactory repoFactory, IOptions<UserFieldSettings> userFieldSettings, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _userFieldSettings = userFieldSettings.Value;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(EditCollectionAgencyPostBusDataPacket packet)
        {            
            string PANEncodeFlag = AppConfigManager.AppSettings["PANEncodeFlag"]?.ToString() ?? "yes";
            _logger.LogInformation("EditCollectionAgencyFFPlugin : Start");
            _flexAppContext = packet.Cmd.Dto.GetAppContext(); 
            _repoFactory.Init(packet.Cmd.Dto);

            string partyId = _flexAppContext.UserId;
            string tenantId = _flexAppContext.TenantId;

            _model = await _repoFactory.GetRepo().FindAll<Agency>().Where(m => m.Id == packet.Cmd.Dto.Id)
                            .IncludeAgencyScopeOfWork().IncludePlaceOfWork().FlexNoTracking().FirstOrDefaultAsync();

            _agency = await _repoFactory.GetRepo().FindAll<Agency>().Where(m => m.Id == packet.Cmd.Dto.Id)
                            .IncludeAgencyScopeOfWork().IncludePlaceOfWork().FlexNoTracking().FirstOrDefaultAsync();

            if (_model != null)
            {
                var user = _model;
                _model.EditCollectionAgency(packet.Cmd);

                _model.Address.SetAddedOrModified();
                _model.CreditAccountDetails.SetAddedOrModified();
                 
                _model.PAN = Encoding.UTF8.GetString(Convert.FromBase64String(_model.PAN)); 

                List<AgencyScopeOfWork> scopeList = new List<AgencyScopeOfWork>();

                List<AgencyPlaceOfWork> placeList = new List<AgencyPlaceOfWork>();

                user.ScopeOfWork.Where(t => !string.IsNullOrEmpty(t.Id)).ToList().ForEach(t => t.SetIsDeleted(true));
                user.PlaceOfWork.Where(t => !string.IsNullOrEmpty(t.Id)).ToList().ForEach(t => t.SetIsDeleted(true));

                if (user.ScopeOfWork != null)
                {
                    foreach (var scope in user.ScopeOfWork)
                    {
                        var obj = _model.ScopeOfWork.Where(c => c.Id == scope.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            scope.SetIsDeleted(false);
                            scope.ProductGroup = obj.ProductGroup;
                            scope.Product = obj.Product;
                            scope.SubProduct = obj.SubProduct;
                            scope.Bucket = obj.Bucket;
                            scope.Zone = obj.Zone;
                            scope.Region = obj.Region;
                            scope.State = obj.State;
                            scope.City = obj.City;
                            scope.SetLastModifiedBy(partyId);
                            scope.SetLastModifiedDate(DateTimeOffset.Now);
                        }
                        scope.SetModified();
                        scopeList.Add(scope);
                    }
                }

                if (_model.PlaceOfWork != null)
                {
                    foreach (var place in user.PlaceOfWork)
                    {
                        var data = _model.PlaceOfWork.Where(c => c.Id == place.Id).FirstOrDefault();
                        if (data != null)
                        {
                            place.SetIsDeleted(false);
                            place.ProductGroup = data.ProductGroup;
                            place.Product = data.Product;
                            place.SubProduct = data.SubProduct;
                            place.Bucket = data.Bucket;
                            place.Zone = data.Zone;
                            place.Region = data.Region;
                            place.State = data.State;
                            place.City = data.City;
                            place.ManagerId = data.ManagerId;
                            place.SetLastModifiedBy(partyId);
                            place.SetLastModifiedDate(DateTimeOffset.Now);
                        }
                        place.SetModified();
                        placeList.Add(place);
                    }
                }

                foreach (var scope in _model.ScopeOfWork.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    scope.SetLastModifiedBy(partyId);
                    scope.SetAddedOrModified();
                    scopeList.Add(scope);
                }

                foreach (var place in _model.PlaceOfWork.Where(c => string.IsNullOrEmpty(c.Id)))
                {
                    place.SetLastModifiedBy(partyId);
                    place.SetAddedOrModified();
                    placeList.Add(place);
                }

                _model.ScopeOfWork = scopeList;
                _model.PlaceOfWork = placeList;

                _model.AgencyIdentifications = await EditProfileIdentificationDocs(packet.Cmd.Dto, tenantId);
                _model.AgencyTypeId = packet.Cmd.Dto.CollectionAgencyTypeId;

                _model.UserId = user.UserId;

                _model.SetCreatedBy(user.CreatedBy);
                _model.SetCreatedDate(user.CreatedDate);
                _model.SetLastModifiedDate(DateTimeOffset.Now);
                _model.SetLastModifiedBy(partyId);

                int deferredCount = _model.AgencyIdentifications.Where(x => x.IsDeferred != null && x.IsDeferred == true).Count();
                if (deferredCount > 0)
                {
                    _model.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencyPendingApprovalWithDeferrals>().SetTFlexId(_model.Id).SetStateChangedBy(_model.LastModifiedBy ?? "");
                }
                else
                {
                    _model.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencyPendingApproval>().SetTFlexId(_model.Id).SetStateChangedBy(_model.LastModifiedBy ?? "");
                }
                if (!_userFieldSettings.EnableUpdateEmail)
                {
                    _model.PrimaryEMail = user.PrimaryEMail;
                }
                if (!_userFieldSettings.EnableUpdateMobileNo)
                {
                    _model.PrimaryMobileNumber = user.PrimaryMobileNumber;
                }
                _repoFactory.GetRepo().InsertOrUpdate(_model);
                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Agency).Name, _model.Id);

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Agency).Name, _model.Id);
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Agency).Name, packet.Cmd.Dto.Id);
            }
            _logger.LogInformation("EditCollectionAgencyFFPlugin : End | EventCondition - " + EventCondition);
        }

        private async Task GenerateAndSendAuditEventAsync(EditCollectionAgencyPostBusDataPacket packet)
        {
            string jsonPatch = _diffGenerator.GenerateDiff(_agency, _model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.Agency.Value,
                Operation: AuditOperationEnum.Edit.Value,
                JsonPatch: jsonPatch,
                InitiatorId: _flexAppContext?.UserId,
                TenantId: _flexAppContext?.TenantId,
                ClientIP: _flexAppContext?.ClientIP
            );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<AgencyIdentification>> EditProfileIdentificationDocs(EditCollectionAgencyDto model, string tenantId)
        {
            List<AgencyIdentification> tflexidentification = new List<AgencyIdentification>();
            if (model.ProfileIdentification != null)
            {
                foreach (var apimodel in model.ProfileIdentification)
                {
                    AgencyIdentification Identification = new AgencyIdentification();
                    Identification.SetId(apimodel.Id);
                    Identification.DeferredTillDate = apimodel.DeferredTillDate;
                    Identification.IsDeferred = apimodel.IsDeferred;
                    Identification.IsWavedOff = apimodel.IsWavedOff;
                    Identification.TFlexIdentificationDocTypeId = apimodel.IdentificationDocTypeId;
                    Identification.TFlexIdentificationTypeId = apimodel.IdentificationTypeId;
                    Identification.SetModified();
                    AgencyIdentificationDoc? docs = await _repoFactory.GetRepo().FindAll<AgencyIdentificationDoc>()
                                                            .Where(x => x.Id == apimodel.IdentificationDocId).FirstOrDefaultAsync();
                    if (docs != null)
                    {
                        Identification.TFlexIdentificationDocs = new List<AgencyIdentificationDoc>();
                        docs.TFlexIdentificationId = Identification.Id;
                        docs.SetModified();
                        Identification.TFlexIdentificationDocs.Add(docs);
                    }
                    tflexidentification.Add(Identification);
                }
            }
            _logger.LogDebug("EditCollectionAgencyFFPlugin : EditProfileIdentificationDocs - End");
            return tflexidentification;
        }
    }
}