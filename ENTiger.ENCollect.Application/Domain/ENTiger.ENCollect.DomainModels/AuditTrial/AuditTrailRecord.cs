using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class AuditTrailRecord : DomainModelBridge
    {

        [StringLength(32)]
        public string EntityId { get; set; }
        [StringLength(100)]
        public string EntityType { get; set; }
        [StringLength(100)]
        public string Operation { get; set; }
        public string DiffJson { get; set; }
        public string ClientIP { get; set; }

        public AuditTrailRecord Create(string EntityId, string EntityType, string Operation, string diffJson, string InitiatorId, string TenantId, string ClientIP)
        {
            this.EntityId = EntityId;
            this.EntityType = EntityType;
            this.Operation = Operation;
            this.DiffJson = diffJson;
            this.CreatedBy = InitiatorId;
            this.ClientIP = ClientIP;
            this.LastModifiedBy = InitiatorId;
            this.SetAdded(SequentialGuid.NewGuidString());
            return this;
        }

        public AuditTrailRecord Create(AuditEventData data)
        {

            this.EntityId = data.EntityId;
            this.EntityType = data.EntityType;
            this.Operation = data.Operation;
            this.DiffJson = data.JsonPatch;
            this.CreatedBy = data.InitiatorId;
            this.ClientIP = data.ClientIP;
            this.LastModifiedBy = data.InitiatorId;
            this.SetAdded(SequentialGuid.NewGuidString());
            return this;           
        }
    }
}
