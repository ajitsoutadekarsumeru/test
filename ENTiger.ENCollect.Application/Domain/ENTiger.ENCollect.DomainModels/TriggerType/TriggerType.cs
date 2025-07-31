using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TriggerType : DomainModelBridge
    {
        protected readonly ILogger<TriggerType> _logger;

        protected TriggerType()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TriggerType>>();
        }

        public TriggerType(ILogger<TriggerType> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        public string Name { get; private set; }
        [StringLength(50)]
        public string CustomName { get; private set; } // Short name for the TriggerType, e.g. XDaysBefore, XDaysAfter, etc.
        [StringLength(150)]
        public string EntryPoint { get; private set; } // Account | User
        [StringLength(50)]
        public string? OffsetBasis { get; private set; } //  DueDate | LastLoginDate
        [StringLength(50)]
        public string? OffsetDirection { get; private set; } //  Before | After | Since

        public bool RequiresDaysOffset { get; private set; } // true if this TriggerType requires a DaysOffset value, false if it does not (e.g. XDaysBefore, XDaysAfter, etc.)
        public bool IsActive { get; private set; }
        [StringLength(100)]
        public string? Description { get; private set; }
       
        public virtual ICollection<CommunicationTrigger> Triggers { get; private set; } = new List<CommunicationTrigger>();

        // Add a new Trigger to this TriggerType
        public void AddTrigger(CommunicationTrigger trigger)
        {
            if (trigger == null) throw new ArgumentNullException(nameof(trigger));
            Triggers.Add(trigger);
            this.SetAddedOrModified();
        }



        // Enable this TriggerType
        public void Enable() => IsActive = true;

        // Disable this TriggerType
        public void Disable() => IsActive = false;
        #endregion

        #region "Protected"
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion

    }
}
