using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class ProcessAgencyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> MakeDormantAgencyUser(DormantAgencyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DormantAgencyUserDataPacket, DormantAgencyUserSequence, DormantAgencyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                MakeDormantAgencyUserCommand cmd = new MakeDormantAgencyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DormantAgencyUserResultModel outputResult = new DormantAgencyUserResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DormantAgencyUserResultModel : DtoBridge
    {
    }
}