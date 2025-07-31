using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateCodeOfConduct(UpdateCodeOfConductDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateCodeOfConductDataPacket, UpdateCodeOfConductSequence, UpdateCodeOfConductDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateCodeOfConductCommand cmd = new UpdateCodeOfConductCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateCodeOfConductResultModel outputResult = new UpdateCodeOfConductResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdateCodeOfConductResultModel : DtoBridge
    {
    }
}
