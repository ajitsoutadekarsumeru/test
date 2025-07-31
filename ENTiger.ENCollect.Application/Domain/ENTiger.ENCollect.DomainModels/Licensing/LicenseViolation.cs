using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class LicenseViolation : DomainModelBridge
    {
        public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
        [StringLength(50)]
        public string Feature { get; set; } = string.Empty;
        public int Limit { get; set; }
        public int Actual { get; set; }
        [StringLength(100)]
        public string ErrorMessage { get; set; } = string.Empty;

        public LicenseViolation() { }

        public LicenseViolation(DateTime occurredOn, string feature, int limit, int actual, string errorMessage)
        {
            OccurredOn = occurredOn;
            Feature = feature;
            Limit = limit;
            Actual = actual;
            ErrorMessage = errorMessage; 
        }

        public LicenseViolation Create(DateTime occurredOn, string feature, int limit, int actual, string errorMessage, string InitiatorId)
        {
            this.OccurredOn = occurredOn;
            this.Feature = feature;
            this.Limit = limit;
            this.Actual = actual;
            this.ErrorMessage = errorMessage;
            this.CreatedBy = InitiatorId;
            this.LastModifiedBy = InitiatorId;
            this.SetAdded(SequentialGuid.NewGuidString());
            return this;
        }

        public LicenseViolation Create(LicenseViolation data, string InitiatorId)
        {
            this.OccurredOn = data.OccurredOn;
            this.Feature = data.Feature;
            this.Limit = data.Limit;
            this.Actual = data.Actual;
            this.ErrorMessage = data.ErrorMessage;
            this.CreatedBy = InitiatorId;
            this.LastModifiedBy = InitiatorId;
            this.SetAdded(SequentialGuid.NewGuidString());
            return this;
        }
    }
}
