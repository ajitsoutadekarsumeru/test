using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> GetAzure(GetAzureDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetAzureDataPacket, GetAzureSequence, GetAzureDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetAzureCommand cmd = new GetAzureCommand
                //{
                //     Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                GetAzureResultModel outputResult = new GetAzureResultModel()
                {
                    Id = dto.GetGeneratedId(),
                    Service = packet.OutputDto.Service,
                    Url = packet.OutputDto.Url,
                    ClientId = packet.OutputDto.ClientId,
                    TenantId = packet.OutputDto.TenantId
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class GetAzureResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string Service { get; set; }
        public string Url { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
    }
}