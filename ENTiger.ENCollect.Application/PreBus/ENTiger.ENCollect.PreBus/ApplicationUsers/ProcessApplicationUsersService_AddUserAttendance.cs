using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddUserAttendance(AddUserAttendanceDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddUserAttendanceDataPacket, AddUserAttendanceSequence, AddUserAttendanceDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddUserAttendanceCommand cmd = new AddUserAttendanceCommand
                {
                    Dto = dto,
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddUserAttendanceResultModel outputResult = new AddUserAttendanceResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddUserAttendanceResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}