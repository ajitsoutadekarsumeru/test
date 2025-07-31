using Sumeru.Flex;

namespace ENTiger.ENCollect.ReportsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProcessReportsService : ProcessFlexServiceBridge
    {
        /// <summary>
        /// YourRemarksForMethod
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual FlexiPagedList<GetCollectionIntensityDetailsDto> GetCollectionIntensityDetails(GetCollectionIntensityDetailsParams @params)
        {
            return _flexHost.GetFlexiQuery<GetCollectionIntensityDetails>().AssignParameters(@params).Fetch();
        }
    }
}
