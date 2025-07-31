
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
        public virtual GeneratePrimaryAllocationFileDto GeneratePrimaryAllocationFile(GeneratePrimaryAllocationFileParams @params)
        {
            return _flexHost.GetFlexiQuery<GeneratePrimaryAllocationFile>().AssignParameters(@params).Fetch();
        }
    }
}
