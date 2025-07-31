using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class ProcessTreatmentService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> GetTreatmentReportFile(GetTreatmentReportFileDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<GetTreatmentReportFileDataPacket, GetTreatmentReportFileSequence, GetTreatmentReportFileDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                //GetTreatmentReportFileCommand cmd = new GetTreatmentReportFileCommand
                //{
                //     Dto = dto,
                //};

                //await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                GetTreatmentReportFileResultModel outputResult = new GetTreatmentReportFileResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class GetTreatmentReportFileResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}