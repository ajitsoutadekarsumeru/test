namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class ProcessCommunicationService : ProcessFlexServiceBridge
    {
        public async Task<GetTemplateByIdDto> GetTemplateById(GetTemplateByIdParams @params)
        {
            return await _flexHost.GetFlexiQuery<GetTemplateById>().AssignParameters(@params).Fetch();
        }
    }
}