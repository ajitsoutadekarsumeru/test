using Sumeru.Flex;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    public partial class ProcessUserSearchCriteriaService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> Add(AddDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddDataPacket, AddSequence, AddDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddCommand cmd = new AddCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddResultModel outputResult = new AddResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}