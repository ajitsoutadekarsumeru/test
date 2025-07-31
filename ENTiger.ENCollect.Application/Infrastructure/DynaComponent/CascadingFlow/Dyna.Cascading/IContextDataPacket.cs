namespace ENCollect.Dyna.Cascading
{
    /// <summary>
    /// Marker interface indicating a class is a valid workflow context,
    /// containing the fields needed by IsNeeded checks and actor flows.
    /// </summary>
    public interface IContextDataPacket
    {
        // No members required at the moment.
        // If in the future you want all contexts to have, for example, 
        // a "WorkflowInstanceId" property, you could add it here.
    }
}