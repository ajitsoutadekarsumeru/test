using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddAgency(AddAgencyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddAgencyDataPacket, AddAgencySequence, AddAgencyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddAgencyCommand cmd = new AddAgencyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddAgencyResultModel outputResult = new AddAgencyResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddAgencyResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}