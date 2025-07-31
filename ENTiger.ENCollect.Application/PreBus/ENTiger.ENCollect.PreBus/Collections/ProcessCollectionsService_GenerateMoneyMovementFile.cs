
namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessCollectionsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GenerateMoneyMovementFileDto GenerateMoneyMovementFile(GenerateMoneyMovementFileParams @params)
        {
            return _flexHost.GetFlexiQuery<GenerateMoneyMovementFile>().AssignParameters(@params).Fetch();
        }
    }
}
