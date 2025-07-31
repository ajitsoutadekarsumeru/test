using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ProcessPublicService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdatePayuResponse(UpdatePayuResponseDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdatePayuResponseDataPacket, UpdatePayuResponseSequence, UpdatePayuResponseDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //UpdatePayuResponseCommand cmd = new UpdatePayuResponseCommand
                //{
                //    Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdatePayuResponseResultModel outputResult = new UpdatePayuResponseResultModel();
                outputResult.Status = "Success";
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdatePayuResponseResultModel : DtoBridge
    {
        public string? Status { get; set; }
    }
}