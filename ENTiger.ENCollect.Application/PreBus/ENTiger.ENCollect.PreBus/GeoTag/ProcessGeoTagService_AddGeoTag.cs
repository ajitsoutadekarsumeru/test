using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddGeoTag(AddGeoTagDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddGeoTagDataPacket, AddGeoTagSequence, AddGeoTagDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddGeoTagCommand cmd = new AddGeoTagCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddGeoTagResultModel outputResult = new AddGeoTagResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddGeoTagResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}