using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> ApproveAgencyWithDeferral(ApproveAgencyWithDeferralDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<ApproveAgencyWithDeferralDataPacket, ApproveAgencyWithDeferralSequence, ApproveAgencyWithDeferralDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                ApproveAgencyWithDeferralCommand cmd = new ApproveAgencyWithDeferralCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                ApproveAgencyWithDeferralResultModel outputResult = new ApproveAgencyWithDeferralResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class ApproveAgencyWithDeferralResultModel : DtoBridge
    {
    }
}