using System.Threading.Tasks;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class ProcessPermissionSchemesService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdatePermissionScheme(UpdatePermissionSchemeDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdatePermissionSchemeDataPacket, UpdatePermissionSchemeSequence, UpdatePermissionSchemeDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdatePermissionSchemeCommand cmd = new UpdatePermissionSchemeCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdatePermissionSchemeResultModel outputResult = new UpdatePermissionSchemeResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }
    public class UpdatePermissionSchemeResultModel : DtoBridge
    {
    }
}
