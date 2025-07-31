using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateCustomerConsentExpiryAsync(UpdateCustomerConsentExpiryDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateCustomerConsentExpiryDataPacket, UpdateCustomerConsentExpirySequence, UpdateCustomerConsentExpiryDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateCustomerConsentExpiryCommand cmd = new UpdateCustomerConsentExpiryCommand
                {
                     Dto = dto,
                };

                await ProcessCommandAsync(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateCustomerConsentExpiryResultModel outputResult = new UpdateCustomerConsentExpiryResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateCustomerConsentExpiryResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
