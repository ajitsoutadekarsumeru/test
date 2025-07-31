using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> UpdateLoanAccountNote(UpdateLoanAccountNoteDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<UpdateLoanAccountNoteDataPacket, UpdateLoanAccountNoteSequence, UpdateLoanAccountNoteDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                UpdateLoanAccountNoteCommand cmd = new UpdateLoanAccountNoteCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                UpdateLoanAccountNoteResultModel outputResult = new UpdateLoanAccountNoteResultModel();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class UpdateLoanAccountNoteResultModel : DtoBridge
    {
    }
}