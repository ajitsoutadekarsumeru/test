
namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessAllocationService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GenerateSecondaryAllocationFileDto GenerateSecondaryAllocationFile(GenerateSecondaryAllocationFileParams @params)
        {
            return _flexHost.GetFlexiQuery<GenerateSecondaryAllocationFile>().AssignParameters(@params).Fetch();
        }
    }
}
