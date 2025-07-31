namespace ENCollect.Dyna.Workflows
{
    /// <summary>
    /// A read-model interface indicating how to check if a given user 
    /// is authorized on a particular domain workflow step, based on 
    /// the data recorded by <see cref="RecordPotentialActorsCommand"/>.
    /// </summary>
    public interface IPotentialActorsReadModel
    {
        /// <summary>
        /// Returns true if the user is present in the potential-actors store 
        /// for the specified (domainId, workflowName, stepIndex).
        /// </summary>
        bool IsUserAuthorized(string domainId, 
            string workflowName, 
            string stepName, string userId);
        
        /// <summary>
        /// A method to store data => RecordPotentialActorsHandler
        /// will call this.
        /// </summary>
        void AddActors(string domainId, string workflowName, int stepIndex, List<string> userIds);
    }
}