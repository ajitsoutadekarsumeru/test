namespace ENCollect.Dyna.Workflows;

public static class WorkflowIdBuilder
{
    /// <summary>
    /// Builds a string-based workflow instance ID by concatenating
    /// the domainId and the workflowDefinition's Name,
    /// e.g. "1234_SettlementWorkflow".
    /// </summary>
    public static string BuildWorkflowInstanceId(
        string domainId,
        DynaWorkflowDefinition<IContextDataPacket> def)
    {
        return $"{domainId}_{def.WorkflowName}";
    }
}