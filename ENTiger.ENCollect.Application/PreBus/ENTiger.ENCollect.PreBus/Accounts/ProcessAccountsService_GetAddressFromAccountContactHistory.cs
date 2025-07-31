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
        public virtual IEnumerable<GetAddressFromAccountContactHistoryDto> GetAddressFromAccountContactHistory(GetAddressFromAccountContactHistoryParams @params)
        {
            return _flexHost.GetFlexiQuery<GetAddressFromAccountContactHistory>().AssignParameters(@params).Fetch();
        }
    }
}
