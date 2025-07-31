
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
        public virtual GenerateAllocationAchievedFileDto GenerateAllocationAchievedFile(GenerateAllocationAchievedFileParams @params)
        {
            return _flexHost.GetFlexiQuery<GenerateAllocationAchievedFile>().AssignParameters(@params).Fetch();
        }
    }
}
