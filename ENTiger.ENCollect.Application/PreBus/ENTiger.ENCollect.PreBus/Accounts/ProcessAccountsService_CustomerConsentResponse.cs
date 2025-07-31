using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> CustomerConsentResponseAsync(CustomerConsentResponseDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CustomerConsentResponseDataPacket, CustomerConsentResponseSequence, CustomerConsentResponseDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CustomerConsentResponseCommand cmd = new CustomerConsentResponseCommand
                {
                    Dto = dto,
                };
              
                await ProcessCommandAsync(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CustomerConsentResponseResultModel outputResult = new CustomerConsentResponseResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class CustomerConsentResponseResultModel : DtoBridge
    {
    }
}
