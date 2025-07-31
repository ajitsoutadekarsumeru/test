using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class ProcessGeoTagService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> SearchUsersByGeoTag(SearchUsersByGeoTagDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<SearchUsersByGeoTagDataPacket, SearchUsersByGeoTagSequence, SearchUsersByGeoTagDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //SearchUsersByGeoTagCommand cmd = new SearchUsersByGeoTagCommand
                //{
                //     Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                List<SearchUsersByGeoTagResultModel> outputResult = new List<SearchUsersByGeoTagResultModel>();
                //outputResult.Id = dto.GetGeneratedId();
                outputResult = packet.output;
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class SearchUsersByGeoTagResultModel : DtoBridge
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserName { get; set; }
        public string? IMEI { get; set; }
        public DateTimeOffset? LocationDateTime { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}