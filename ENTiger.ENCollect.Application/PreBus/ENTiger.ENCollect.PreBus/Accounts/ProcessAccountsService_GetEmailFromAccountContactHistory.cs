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
        public virtual IEnumerable<GetEmailFromAccountContactHistoryDto> GetEmailFromAccountContactHistory(GetEmailFromAccountContactHistoryParams @params)
        {
            return _flexHost.GetFlexiQuery<GetEmailFromAccountContactHistory>().AssignParameters(@params).Fetch();
        }
    }
}
