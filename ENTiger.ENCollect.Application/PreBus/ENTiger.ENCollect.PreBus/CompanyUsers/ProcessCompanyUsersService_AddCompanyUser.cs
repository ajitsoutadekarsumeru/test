using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class ProcessCompanyUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AddCompanyUser(AddCompanyUserDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddCompanyUserDataPacket, AddCompanyUserSequence, AddCompanyUserDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddCompanyUserCommand cmd = new AddCompanyUserCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddCompanyUserResultModel outputResult = new AddCompanyUserResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddCompanyUserResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}