using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateMastersPlugin : FlexiPluginBase, IFlexiPlugin<UpdateMastersPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139e49024520a1df93258dbc483e7f";
        public override string FriendlyName { get; set; } = "UpdateMastersPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateMastersPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected FlexAppContextBridge? _flexAppContext;
        private string? userId;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public UpdateMastersPlugin(ILogger<UpdateMastersPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(UpdateMastersPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();

            _repoFactory.Init(packet.Cmd.Dto);
            userId = _flexAppContext.UserId;
            string MasterName = "";
            int result = 0;
            dynamic? oldObject;

            _logger.LogDebug("UpdateMastersByIdFFPlugin : UpdateMastersByIdFFPlugin - Start");
            MasterName = packet.Cmd.Dto.MasterName;

            if (MasterName == "BASEBRANCHMASTER")
            {
                _logger.LogDebug("Enter in BaseBranchMaster");
                oldObject = await _repoFactory.GetRepo().FindAll<BaseBranch>().Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

                BaseBranch? baseBranch = await ConstructBaseBranchFromModelAsync(packet.Cmd.Dto, userId);
                baseBranch?.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(baseBranch);
                result = await _repoFactory.GetRepo().SaveAsync();

                if (result > 0)
                    await GenerateAndSendAuditEventAsync(oldObject, baseBranch, packet);
            }
            else if (string.Equals(MasterName, MasterEnum.PRODUCTMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in ProductMaster");
                oldObject = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();
                
                CategoryItem? categoryItem = await ConstructCategoryItemFromModel(packet.Cmd.Dto, userId);
                categoryItem?.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(categoryItem);
                result = await _repoFactory.GetRepo().SaveAsync();
                
                if (result > 0) 
                    await GenerateAndSendAuditEventAsync(oldObject, categoryItem, packet);
            }
            else if (string.Equals(MasterName, MasterEnum.DEPOSITBANKMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in DepositBankMaster");
                oldObject = await _repoFactory.GetRepo().FindAll<DepositBankMaster>().Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

                DepositBankMaster depositBankMaster = await ConstructDepositBankMasterFromModelAsync(packet.Cmd.Dto, userId);
                depositBankMaster.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(depositBankMaster);
                result = await _repoFactory.GetRepo().SaveAsync();

                if (result > 0)
                    await GenerateAndSendAuditEventAsync(oldObject, depositBankMaster, packet);
            }
            else if (string.Equals(MasterName, MasterEnum.DISPOSITIONCODEMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in DispositionCodeMaster");
                oldObject = await _repoFactory.GetRepo().FindAll<DispositionCodeMaster>().Where(a => a.Id == packet.Cmd.Dto.Id).FirstOrDefaultAsync();

                DispositionCodeMaster dispositionCodeMaster = await ConstructDispositionCodeMasterFromModelAsync(packet.Cmd.Dto, userId);
                dispositionCodeMaster.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(dispositionCodeMaster);
                result = await _repoFactory.GetRepo().SaveAsync();

                if (result > 0)
                    await GenerateAndSendAuditEventAsync(oldObject, dispositionCodeMaster, packet);
            }
            else if (string.Equals(MasterName, MasterEnum.BUCKETMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in BucketMaster");
                oldObject = await _repoFactory.GetRepo().FindAll<Bucket>().Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

                Bucket? bucket = await ConstructBucketMasterFromModelAsync(packet.Cmd.Dto, userId);
                bucket?.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(bucket);
                result = await _repoFactory.GetRepo().SaveAsync();

                if (result > 0)
                    await GenerateAndSendAuditEventAsync(oldObject, bucket, packet);
            }
            else if (string.Equals(MasterName, MasterEnum.BANKMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in BankMaster");
                oldObject = await _repoFactory.GetRepo().FindAll<Bank>().Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

                Bank bank = await ConstructBankMasterFromModelAsync(packet.Cmd.Dto, userId);
                bank.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(bank);
                result = await _repoFactory.GetRepo().SaveAsync();

                if (result > 0)
                    await GenerateAndSendAuditEventAsync(oldObject, bank, packet);
            }
            else if (string.Equals(MasterName, MasterEnum.GEOMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in GeoMaster");
                BaseBranch baseBranch = ConstructGeoMasterFromModel(packet.Cmd.Dto, userId);
            }
            else if (string.Equals(MasterName, MasterEnum.DEPARTMENTDESIGNATIONMASTER.Value, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("Enter in Department Designation Master");
                oldObject = await _repoFactory.GetRepo().FindAll<Designation>().FlexInclude(a => a.Department)
                                    .Where(a => a.Id == packet.Cmd.Dto.Id).FlexNoTracking().FirstOrDefaultAsync();

                Designation? designation = await ConstructDepartmentDEsignationMasterFromModelAsync(packet.Cmd.Dto, userId);
                designation?.SetModified();
                _repoFactory.GetRepo().InsertOrUpdate(designation);
                result = await _repoFactory.GetRepo().SaveAsync();

                if (result > 0)
                    await GenerateAndSendAuditEventAsync(oldObject, designation, packet);
            }
        }


        private async Task GenerateAndSendAuditEventAsync(dynamic oldObject,dynamic newObject, UpdateMastersPostBusDataPacket packet)
        {
            string jsonPatch = _diffGenerator.GenerateDiff(oldObject, newObject);
            _auditData = new AuditEventData(
                            EntityId: newObject?.Id,
                            EntityType: AuditedEntityTypeEnum.Master.Value,
                            Operation: AuditOperationEnum.Edit.Value,
                            JsonPatch: jsonPatch,
                            InitiatorId: _flexAppContext?.UserId,
                            TenantId: _flexAppContext?.TenantId,
                            ClientIP: _flexAppContext?.ClientIP
                        );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private BaseBranch ConstructGeoMasterFromModel(UpdateMastersDto inputmodel, string partyId)
        {
            BaseBranch baseBranch = new BaseBranch();
            return baseBranch;
        }

        private async Task<Designation?> ConstructDepartmentDEsignationMasterFromModelAsync(UpdateMastersDto model, string partyId)
        {
            Designation? designations = await _repoFactory.GetRepo().FindAll<Designation>().FlexInclude(a => a.Department).Where(a => a.Id == model.Id).FirstOrDefaultAsync();
            if (model.IsDisable == true)
            {
                designations?.disableDesignation(designations, userId);
            }
            return designations;
        }

        private async Task<Bank?> ConstructBankMasterFromModelAsync(UpdateMastersDto model, string partyId)
        {
            Bank? banks = new Bank();

            banks = await _repoFactory.GetRepo().FindAll<Bank>().Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            if (model.IsDisable == true)
            {
                banks?.disableBank(banks, userId);
            }
            return banks;
        }

        private async Task<Bucket?> ConstructBucketMasterFromModelAsync(UpdateMastersDto model, string partyId)
        {
            Bucket?  buckets = await _repoFactory.GetRepo().FindAll<Bucket>().Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            if (model.IsDisable == true)
            {
                buckets?.disableBucket(buckets, userId);
            }
            return buckets;
        }

        private async Task<DispositionCodeMaster?> ConstructDispositionCodeMasterFromModelAsync(UpdateMastersDto model, string partyId)
        {
            DispositionCodeMaster? DispositionCodeMaster = await _repoFactory.GetRepo().FindAll<DispositionCodeMaster>().Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            if (model.IsDisable == true)
            {
                DispositionCodeMaster?.disableDispositionCodeMaster(DispositionCodeMaster, userId);
            }
            return DispositionCodeMaster;
        }

        private async Task<DepositBankMaster> ConstructDepositBankMasterFromModelAsync(UpdateMastersDto model, string partyId)
        {
            DepositBankMaster? depositBanks = await _repoFactory.GetRepo().FindAll<DepositBankMaster>().Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            if (model.IsDisable == true)
            {
                //depositBank.IsDeleted = true;
                depositBanks?.disableDepositBankMaster(depositBanks, userId);
            }
            return depositBanks;
        }

        private async Task<CategoryItem?> ConstructCategoryItemFromModel(UpdateMastersDto model, string partyId)
        {
            CategoryItem?  categoryItems = await _repoFactory.GetRepo().FindAll<CategoryItem>().Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            if (model.IsDisable == true)
            {
                categoryItems?.disableCategoryItem(categoryItems, userId);
            }
            return categoryItems;
        }

        private async Task<BaseBranch?> ConstructBaseBranchFromModelAsync(UpdateMastersDto model, string partyId)
        {
            BaseBranch? BaseBranchs = await _repoFactory.GetRepo().FindAll<BaseBranch>().Where(a => a.Id == model.Id).FirstOrDefaultAsync();

            if (model.IsDisable == true)
            {
                BaseBranchs?.disableBaseBranch(BaseBranchs, userId);
            }

            return BaseBranchs;
        }
    }
}