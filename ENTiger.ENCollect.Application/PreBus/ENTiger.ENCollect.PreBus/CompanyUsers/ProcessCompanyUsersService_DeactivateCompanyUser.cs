using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> DeactivateCompanyUser(DeactivateCompanyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<DeactivateCompanyUserDataPacket, DeactivateCompanyUserSequence, DeactivateCompanyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                DeactivateCompanyUserCommand cmd = new DeactivateCompanyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                DeactivateCompanyUserResultModel outputResult = new DeactivateCompanyUserResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class DeactivateCompanyUserResultModel : DtoBridge
    {
    }
}