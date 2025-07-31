using ENCollect.Dyna.Workflows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class GenerateSettlementLetter : IGenerateSettlementLetter
    {
        protected readonly ILogger<GenerateSettlementLetter> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly ISettlementRepository _settlementRepository;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        protected FlexAppContextBridge? _flexAppContext;

        public GenerateSettlementLetter(
            ILogger<GenerateSettlementLetter> logger, 
            IRepoFactory repoFactory,
             IFileSystem fileSystem,
           IOptions<FilePathSettings> fileSettings,
            ISettlementRepository settlementRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(SettlementApprovedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            _logger.LogInformation("GenerateSettlementLetter : Start");

            var query = _repoFactory.GetRepo().FindAll<Settlement>()
                .Include(s=>s.Documents)
                .Where(x => x.Id == @event.DomainId);

            var result = query.Select(x => new SettlementLetterDto
                {
                    Id = x.Id,
                    LoanAccountId = x.LoanAccountId, 
                    CustomerName = x.LoanAccount.CUSTOMERNAME,
                    AgreementId = x.LoanAccount.AGREEMENTID,
                    DisbursedAmount = x.LoanAccount.DISBURSEDAMOUNT,
                    OutstandingDues = x.LoanAccount.TOS,
                    LastPaymentDate = x.Installments
                                   .Select(i => (DateTime?)i.InstallmentDueDate.UtcDateTime)
                                   .Max()                        // null-safe max
                                   ?? DateTime.MinValue,         // or default(DateTime)
                    SettlementAmount = x.SettlementAmount,
                    ApprovalDate = x.StatusUpdatedOn,
                    InstallmentCount = x.Installments.Count,
            })
                .FirstOrDefault();

            if (result != null)
            {

                // Generate the settlement letter
                var fileName = $"Settlement_{result.AgreementId}_{result.Id}.pdf";
                var templateName = "SettlementLetter.html";
                var filePath = BuildFilePath(fileName);
                var templatePath = _fileSystem.Path.Combine(
                    _fileSettings.BasePath, 
                    _fileSettings.IncomingPath,
                    _fileSettings.TemplateFilePath,
                    templateName);
                await _settlementRepository.GenerateSettlementLetter(filePath, templatePath, result);

                // Save the settlement
                Settlement settlement = query.FirstOrDefault();
                settlement.AddDocument("SettlementLetter", "Settlement Letter", fileName);

                
                await _settlementRepository.SaveAsync(_flexAppContext, settlement);
            }


            _logger.LogInformation("GenerateSettlementLetter : End");
            await Task.CompletedTask;
        }
        private string BuildFilePath(string filename)
        {
            return _fileSystem.Path.Combine(
                _fileSettings.BasePath, _fileSettings.IncomingPath,
                filename);
        }
    }
}