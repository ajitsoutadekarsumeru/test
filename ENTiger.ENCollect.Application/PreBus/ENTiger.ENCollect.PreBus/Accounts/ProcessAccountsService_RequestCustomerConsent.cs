using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> RequestCustomerConsentAsync(RequestCustomerConsentDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RequestCustomerConsentDataPacket, RequestCustomerConsentSequence, RequestCustomerConsentDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RequestCustomerConsentCommand cmd = new RequestCustomerConsentCommand
                {
                     Dto = dto,
                };

                await ProcessCommandAsync(cmd);
                
                CommandResult cmdResult = new CommandResult(Status.Success);
                RequestCustomerConsentResultModel outputResult = new RequestCustomerConsentResultModel();
                outputResult.Id = dto.GetGeneratedId();
                outputResult.EmailId = packet.EmailId;
                outputResult.MobileNumber = packet.MobileNumber;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class RequestCustomerConsentResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string MobileNumber {  get; set; }
        public string EmailId {  get; set; }
    }
}
