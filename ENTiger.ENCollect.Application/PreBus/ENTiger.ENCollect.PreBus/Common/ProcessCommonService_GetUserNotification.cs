
namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual async Task<GetUserNotificationDto> GetUserNotification(GetUserNotificationParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUserNotification>().AssignParameters(@params).Fetch();
        }
    }
}
