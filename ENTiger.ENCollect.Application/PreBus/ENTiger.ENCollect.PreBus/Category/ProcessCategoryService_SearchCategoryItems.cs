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
        public async Task<IEnumerable<SearchCategoryItemsDto>> SearchCategoryItems(SearchCategoryItemsParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchCategoryItems>().AssignParameters(@params).Fetch();
        }
    }
}