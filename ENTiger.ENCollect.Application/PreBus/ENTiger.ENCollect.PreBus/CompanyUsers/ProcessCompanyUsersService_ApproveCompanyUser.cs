using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ApproveCompanyUser(ApproveCompanyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ApproveCompanyUserDataPacket, ApproveCompanyUserSequence, ApproveCompanyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ApproveCompanyUserCommand cmd = new ApproveCompanyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ApproveCompanyUserResultModel outputResult = new ApproveCompanyUserResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ApproveCompanyUserResultModel : DtoBridge
    {
    }
}