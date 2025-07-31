using System.Collections.Generic;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ProcessDesignationsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchDesignationDto>> SearchDesignation(SearchDesignationParams @params)
        {
            return await _flexHost.GetFlexiQuery<SearchDesignation>().AssignParameters(@params).Fetch();
        }
    }
}