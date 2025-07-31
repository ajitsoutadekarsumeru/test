using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetAccountBalanceDetails(GetAccountBalanceDetailsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetAccountBalanceDetailsDataPacket, GetAccountBalanceDetailsSequence, GetAccountBalanceDetailsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetAccountBalanceDetailsCommand cmd = new GetAccountBalanceDetailsCommand
                //{
                //    Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                GetAccountBalanceDetailsResultModel outputResult = new GetAccountBalanceDetailsResultModel()
                {
                    Id = dto.GetGeneratedId(),
                    outputResult = packet.getAccountBalanceDetailsDto
                };
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class GetAccountBalanceDetailsResultModel : DtoBridge
    {
        public string? Id { get; set; }
        public GetAccountBalanceOutputDetailsDto outputResult { get; set; }

    }
}
