using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountsService : ProcessFlexServiceBridge
    {
        public virtual async Task<CommandResult> AddLoanAccountNote(AddLoanAccountNoteDto dto)
        {
            var packet = await ProcessBusinessRuleSequence<AddLoanAccountNoteDataPacket, AddLoanAccountNoteSequence, AddLoanAccountNoteDto, FlexAppContextBridge>(dto);

            if (packet.HasError)
            {
                return new CommandResult(Status.Failed, packet.Errors());
            }
            else
            {
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                AddLoanAccountNoteCommand cmd = new AddLoanAccountNoteCommand
                {
                    Dto = dto,
                };

                await ProcessCommand(cmd);

                CommandResult cmdResult = new CommandResult(Status.Success);
                AddLoanAccountNoteResultModel outputResult = new AddLoanAccountNoteResultModel();
                outputResult.Id = dto.GetGeneratedId();
                cmdResult.result = outputResult;
                return cmdResult;
            }
        }
    }

    public class AddLoanAccountNoteResultModel : DtoBridge
    {
        public string Id { get; set; }
    }
}