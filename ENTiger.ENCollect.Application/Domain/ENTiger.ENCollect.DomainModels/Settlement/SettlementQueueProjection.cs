using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect;

public class SettlementQueueProjection : DomainModelBridge
{
    [StringLength(50)]
    public string WorkflowName { get; private set; }
    [StringLength(150)]
    public string WorkflowInstanceId { get; private set; }
    [StringLength(32)]
    public string SettlementId { get; private set; }
    public Settlement Settlement { get; private set; }
    [StringLength(100)]
    public string StepName { get; private set; }
    [StringLength(3502)]
    public string StepType { get; private set; }
    [StringLength(50)]
    public string UIActionContext { get; private set; }
    [StringLength(32)]
    public string ApplicationUserId { get; private set; }
    public ApplicationUser ApplicationUser { get; private set; }


    // EF Core
    public SettlementQueueProjection() { }

    public SettlementQueueProjection(
        string workflowName,
        string workflowInstanceId,
        string settlementId,       
        string userId,
        string stepName,
        string stepType,
        string uIActionContext)
    {
        Id = SequentialGuid.NewGuidString();
        WorkflowName = workflowName ?? throw new ArgumentNullException(nameof(workflowName));
        WorkflowInstanceId = workflowInstanceId;
        SettlementId = settlementId;
        
        ApplicationUserId = userId;
        TrackingState = TrackingState.Added;
        StepName = stepName;
        StepType = uIActionContext;
        UIActionContext = uIActionContext;
    }

    public void Delete()
    {        
        this.IsDeleted = true;
        this.TrackingState = TrackingState.Deleted;
        this.SetAddedOrModified();
    }
}
