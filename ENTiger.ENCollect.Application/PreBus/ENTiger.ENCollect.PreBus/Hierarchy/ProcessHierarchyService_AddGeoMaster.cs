using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class ProcessHierarchyService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddGeoMaster(AddGeoMasterDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddGeoMasterDataPacket, AddGeoMasterSequence, AddGeoMasterDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddGeoMasterCommand cmd = new AddGeoMasterCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddGeoMasterResultModel outputResult = new AddGeoMasterResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class AddGeoMasterResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
