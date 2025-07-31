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
        public async Task<GetColorDto> GetColor(GetColorParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetColor>().AssignParameters(@params).Fetch();
        }
    }
}