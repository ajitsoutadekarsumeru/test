using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class ProcessGeoLocationService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateGeoLocations(UpdateGeoLocationsDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateGeoLocationsDataPacket, UpdateGeoLocationsSequence, UpdateGeoLocationsDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateGeoLocationsCommand cmd = new UpdateGeoLocationsCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateGeoLocationsResultModel outputResult = new UpdateGeoLocationsResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateGeoLocationsResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
