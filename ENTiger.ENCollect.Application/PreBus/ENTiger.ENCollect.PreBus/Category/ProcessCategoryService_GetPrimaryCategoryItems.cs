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
        public async Task<IEnumerable<GetPrimaryCategoryItemsDto>> GetPrimaryCategoryItems(GetPrimaryCategoryItemsParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetPrimaryCategoryItems>().AssignParameters(@params).Fetch();
        }
    }
}