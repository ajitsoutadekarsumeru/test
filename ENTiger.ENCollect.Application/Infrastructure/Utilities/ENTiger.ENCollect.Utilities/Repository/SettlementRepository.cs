using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.SettlementModule;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Drawing;
using System.Drawing.Printing;

namespace ENTiger.ENCollect
{
    public class SettlementRepository : ISettlementRepository
    {
        private readonly IRepoFactory _repoFactory;


        public SettlementRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }


        public async Task<List<Settlement>> GetByStatusesForCreatorAsync(FlexAppContextBridge context, string creatorId, HashSet<string> status)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                           .FindAll<Settlement>()
                           .ByCreatedBy(creatorId)
                           .ByStatus(status)
                           .ToListAsync();
        }

        public async Task<Settlement> GetWaiverDetailsByIdAsync(FlexAppContextBridge context,
            string id)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                           .FindAll<Settlement>()
                           .Include(s => s.WaiverDetails)
                           .ById(id)
                           .FirstOrDefaultAsync();
        }
        public async Task<Settlement> GetByIdAsync(FlexAppContextBridge context,
            string id)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                           .FindAll<Settlement>()
                                    .ById(id)
                                    .FirstOrDefaultAsync();


        }

        /// <summary>
        /// Fetch settlements whose latest history assignment includes the given user.
        /// </summary>
        public async Task<List<Settlement>> GetSettlementsAssignedToAsync(
            FlexAppContextBridge context, string userId)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                           .FindAll<SettlementQueueProjection>()
                            .AsNoTracking()
                            .Include(s => s.Settlement)
                            .Where(s => s.ApplicationUserId == userId && s.IsDeleted == false)
                            .Select(a => a.Settlement)
                            .ToListAsync();
        }

        /// <summary>
        /// Fetch paged settlements by status where latest history assignment includes the given user.
        /// </summary>
        public async Task<List<SettlementQueueProjection>> GetByStatusForAssignedUserAsync(
            FlexAppContextBridge context, string userId, string status)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                           .FindAll<SettlementQueueProjection>()
                            .AsNoTracking()
                            .FlexInclude(s => s.Settlement)
                            .FlexInclude(s => s.Settlement.StatusHistory)
                            .FlexInclude(s => s.Settlement.WaiverDetails)
                            .Where(s => s.ApplicationUserId == userId && s.IsDeleted == false)
                            .OrderBy(s => s.CreatedDate)
                            .ToListAsync();
        }

        public async Task<bool> ExistsOpenSettlementAsync(FlexAppContextBridge context, string loanAccountId)
        {
            _repoFactory.Init(context);
            // get the list of all status‐values whose Group == "Open"
            var openStatuses = SettlementStatusEnum
                                   .ByGroup("Open")
                                   .Select(s => s.Value)
                                   .ToArray();

            return await _repoFactory.GetRepo()
                           .FindAll<Settlement>()
                           .AsNoTracking()
                            .AnyAsync(s =>
                                s.LoanAccountId == loanAccountId &&
                                openStatuses.Contains(s.Status)
                            );
        }

        public async Task SaveAsync<TEntity>(FlexAppContextBridge context, TEntity entity) where TEntity : TFlex
        {
            _repoFactory.Init(context);
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
        }


        public async Task<IReadOnlyCollection<WaiverDetail>> GetMySettlementsWaiversBySettlementIdAsync(FlexAppContextBridge context, string settlementId)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                            .FindAll<Settlement>()
                            .Include(i => i.WaiverDetails)
                            .Where(w => w.Id == settlementId)
                            .SelectMany(s => s.WaiverDetails)
                            .ToListAsync();
        }

        public async Task<IReadOnlyCollection<InstallmentDetail>> GetMySettlementsInstallmentsBySettlementIdAsync(FlexAppContextBridge context, string settlementId)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                            .FindAll<Settlement>()
                            .Include(i => i.Installments)
                            .Where(w => w.Id == settlementId)
                            .SelectMany(s => s.Installments)
                            .ToListAsync();
        }

        public async Task<IReadOnlyCollection<SettlementDocument>> GetMySettlementsDocumentsBySettlementIdAsync(FlexAppContextBridge context, string settlementId)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                            .FindAll<Settlement>()
                            .Include(i => i.Documents)
                            .Where(w => w.Id == settlementId)
                            .SelectMany(s => s.Documents)
                            .ToListAsync();
        }



        public async Task<List<SettlementQueueProjection>> GetQueueProjectionsByWorkflowInstanceId(FlexAppContextBridge context, string workflowInstanceId)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                            .FindAll<SettlementQueueProjection>()
                            .Where(w => w.WorkflowInstanceId == workflowInstanceId)
                            .ToListAsync();
        }

        public Task<bool> IsUserAuthorizedForStep(
            FlexAppContextBridge flexAppContext, 
            string domainId, 
            string workflowName, 
            string stepName, 
            string userId)
        {
            _repoFactory.Init(flexAppContext);
            return _repoFactory.GetRepo()
                .FindAll<SettlementQueueProjection>()
                .AsNoTracking()
                .AnyAsync(a => a.WorkflowName == workflowName &&
                               a.StepName == stepName &&
                               a.ApplicationUserId == userId &&
                               a.SettlementId == domainId &&
                               a.IsDeleted == false);
        }

        public async Task<bool> IsUserAuthorized(string domainId, string workflowName, string stepName, string userId)
        {
            throw new NotImplementedException();
        }
        public async Task GenerateSettlementLetter(string filePath, string templatePath, SettlementLetterDto result)
        {
            // 1. Read the HTML template
            string html = await File.ReadAllTextAsync(templatePath);
            // 2. Replace placeholders with actual data
            var type = result.InstallmentCount > 1 ? "staggered" : "a one-time (OTS)";
            html = html.Replace("{CustomerName}", result.CustomerName ?? "Customer")
                       .Replace("{MailingAddress}", result.MailingAddress ?? "")
                       .Replace("{AgreementId}", result.AgreementId ?? "N/A")
                       .Replace("{Type}", type?? "")
                       .Replace("{SanctionDate}", result.SanctionDate?.ToString("dd MMMM yyyy") ?? "N/A")
                       .Replace("{BankName}", result.BankName ?? "Bank")
                       .Replace("{ApprovalDate}", result.ApprovalDate?.ToString("dd MMMM yyyy") ?? "N/A")
                       .Replace("{DisbursedAmount}", result.DisbursedAmount ?? "0.00")
                       .Replace("{OutstandingDues}", result.OutstandingDues ?? "0.00")
                       .Replace("{SettlementAmount}", result.SettlementAmount?.ToString("N2") ?? "0.00")
                       .Replace("{LastPaymentDate}", result.LastPaymentDate?.ToString("dd MMMM yyyy") ?? "N/A");

            // 3. Convert to PDF using XMLWorker
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var doc = new Document(PageSize.A4, 36, 36, 36, 36))
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                using (var sr = new StringReader(html))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
                }
                doc.Close();
            }

        }
      
        public  async Task<SettlementQueueProjection> GetQueueProjectionBySettlementIdAsync(
            FlexAppContextBridge context, 
            string settlementId, string applicationUserId)
        {
            //fetch the settlement queue projection by settlementId
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                .FindAll<SettlementQueueProjection>()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SettlementId == settlementId
                && s.ApplicationUserId == applicationUserId
                && s.IsDeleted == false);
        }
    }
    
}
