
namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTriggerByIdDto GetTriggerById(GetTriggerByIdParams @params)
        {
            return _flexHost.GetFlexiQuery<GetTriggerById>().AssignParameters(@params).Fetch();
        }
    }
}
