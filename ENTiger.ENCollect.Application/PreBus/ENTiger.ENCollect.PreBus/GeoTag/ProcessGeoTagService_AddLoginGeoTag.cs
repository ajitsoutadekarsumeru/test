using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddLoginGeoTag(AddLoginGeoTagDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddLoginGeoTagDataPacket, AddLoginGeoTagSequence, AddLoginGeoTagDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddLoginGeoTagCommand cmd = new AddLoginGeoTagCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddLoginGeoTagResultModel outputResult = new AddLoginGeoTagResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddLoginGeoTagResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}