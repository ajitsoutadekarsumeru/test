using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateUserAttendance(UpdateUserAttendanceDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateUserAttendanceDataPacket, UpdateUserAttendanceSequence, UpdateUserAttendanceDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //UpdateUserAttendanceCommand cmd = new UpdateUserAttendanceCommand
                //{
                //    Dto = dto,
                //};
                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateUserAttendanceResultModel outputResult = new UpdateUserAttendanceResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateUserAttendanceResultModel : DtoBridge
    {
    }
}