namespace ENTiger.ENCollect.CategoryModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessCategoryService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<GetPrimaryCategoryByIdDto> GetPrimaryCategoryById(GetPrimaryCategoryByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPrimaryCategoryById>().AssignParameters(@params).Fetch();
        }
    }
}