using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> MakeDormantCompanyUser(DormantCompanyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<MakeDormantCompanyUserDataPacket, MakeDormantCompanyUserSequence, DormantCompanyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                MakeDormantCompanyUserCommand cmd = new MakeDormantCompanyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                MakeDormantCompanyUserResultModel outputResult = new MakeDormantCompanyUserResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class MakeDormantCompanyUserResultModel : DtoBridge
    {
    }
}