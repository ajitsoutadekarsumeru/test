using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class ProcessPermissionSchemesService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> CreatePermissionScheme(CreatePermissionSchemeDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<CreatePermissionSchemeDataPacket, CreatePermissionSchemeSequence, CreatePermissionSchemeDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                CreatePermissionSchemeCommand cmd = new CreatePermissionSchemeCommand
                {
                     Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                CreatePermissionSchemeResultModel outputResult = new CreatePermissionSchemeResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class CreatePermissionSchemeResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}
