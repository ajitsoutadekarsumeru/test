using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ProcessApplicationUsersService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> Login(LoginDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<LoginDataPacket, LoginSequence, LoginDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //LoginCommand cmd = new LoginCommand
                //{
                //     Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                LoginResultModel outputResult = new LoginResultModel()
                {
                    Id = dto.GetGeneratedId(),
                    access_token = packet.token.access_token,
                    expires_in = packet.token.expires_in,
                    token_type = packet.token.token_type,
                    message = packet.token.message
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class LoginResultModel : DtoBridge
    {
        public string Id { get; set; }
        public string access_token { get; set; }
        public int? expires_in { get; set; }
        public string token_type { get; set; }
        public string? message { get; set; }
    }
}