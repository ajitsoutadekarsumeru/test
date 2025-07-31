using ENTiger.ENCollect.AgencyUsersModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class ProcessAgencyService : ProcessFlexServiceBridge
    {
        public async Task<CommandResult> AgencyUpload(AgencyUploadDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AgencyUploadDataPacket, AgencyUploadSequence, AgencyUploadDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                //dto.SetGeneratedId(_pkGenerator.GenerateKey());
                packet.OutputDto.SetAppContext(dto.GetAppContext());
                AgencyUploadCommand cmd = new AgencyUploadCommand
                {
                    Dto = packet.OutputDto
                };
                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UploadResultModel outputResult = new UploadResultModel()
                {
                    DocumentId = this.Id,
                    FileName = packet.OutputDto.Name,
                    filePath = packet.OutputDto.Path,
                    FileSize = packet.OutputDto.Size
                };
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AgencyUploadResultModel : DtoBridge
    {
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public string? filePath { get; set; }
        public string? DocumentId { get; set; }
    }
}