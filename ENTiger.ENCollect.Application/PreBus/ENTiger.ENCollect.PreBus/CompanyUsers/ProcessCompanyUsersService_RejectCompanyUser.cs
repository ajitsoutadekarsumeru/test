using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> RejectCompanyUser(RejectCompanyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<RejectCompanyUserDataPacket, RejectCompanyUserSequence, RejectCompanyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                RejectCompanyUserCommand cmd = new RejectCompanyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                RejectCompanyUserResultModel outputResult = new RejectCompanyUserResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class RejectCompanyUserResultModel : DtoBridge
    {
    }
}