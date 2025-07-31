using System.Collections.Generic;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual IEnumerable<GetMobileFromAccountContactHistoryDto> GetMobileFromAccountContactHistory(GetMobileFromAccountContactHistoryParams @params)
        {
            return _flexHost.GetFlexiQuery<GetMobileFromAccountContactHistory>().AssignParameters(@params).Fetch();
        }
    }
}
