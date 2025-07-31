
namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async virtual Task<GetUserWalletDetailsDto> GetUserWalletDetails(GetUserWalletDetailsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetUserWalletDetails>().AssignParameters(@params).Fetch();
        }
    }
}
