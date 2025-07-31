using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ApproveCollectionAgency(ApproveCollectionAgencyDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ApproveCollectionAgencyDataPacket, ApproveCollectionAgencySequence, ApproveCollectionAgencyDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ApproveCollectionAgencyCommand cmd = new ApproveCollectionAgencyCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ApproveCollectionAgencyResultModel outputResult = new ApproveCollectionAgencyResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ApproveCollectionAgencyResultModel : DtoBridge
    {
    }
}