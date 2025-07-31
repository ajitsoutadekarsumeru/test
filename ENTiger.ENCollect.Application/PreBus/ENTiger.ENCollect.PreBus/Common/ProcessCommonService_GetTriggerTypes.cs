namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCommonService : ProcessFlexServiceBridge
    {
        public virtual async Task<IEnumerable<GetTriggerTypesDto>> GetTriggerTypes(GetTriggerTypesParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTriggerTypes>().AssignParameters(@params).Fetch();
        }
    }
}
