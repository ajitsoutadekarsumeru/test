using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect;

public class TriggerAccountQueueProjection : DomainModelBridge
{
    /// <summary>
    /// Correlation ID for this specific run of the trigger.
    /// </summary>
    public string RunId { get; set; }

    /// <summary>
    /// The Trigger definition that kicked off this queue entry.
    /// </summary>   
    [StringLength(32)]
    public string TriggerId { get; private set; }
    public CommunicationTrigger Trigger { get; private set; }

    /// <summary>
    /// The Account that was identified against this trigger.
    /// </summary>

    [StringLength(32)]
    public string AccountId { get; private set; }
    public LoanAccount Account { get; private set; }

    // EF Core
    public TriggerAccountQueueProjection() { }

    public TriggerAccountQueueProjection(
        string triggerId,
        string runId,
        string accountId       
        )
    {
        Id = SequentialGuid.NewGuidString();
        RunId = runId;
        AccountId = accountId;

        TriggerId = triggerId;
        TrackingState = TrackingState.Added;
    }

    public void Delete()
    {        
        this.IsDeleted = true;
        this.TrackingState = TrackingState.Deleted;
        this.SetAddedOrModified();
    }
}
