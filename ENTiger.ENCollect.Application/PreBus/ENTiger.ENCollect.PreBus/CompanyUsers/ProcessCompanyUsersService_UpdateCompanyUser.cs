using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> UpdateCompanyUser(UpdateCompanyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateCompanyUserDataPacket, UpdateCompanyUserSequence, UpdateCompanyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateCompanyUserCommand cmd = new UpdateCompanyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateCompanyUserResultModel outputResult = new UpdateCompanyUserResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateCompanyUserResultModel : DtoBridge
    {
    }
}