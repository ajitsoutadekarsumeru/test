using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.SettlementModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ISettlementRepository
    {
        
        Task<bool> ExistsOpenSettlementAsync(FlexAppContextBridge context, string loanAccountId);
        Task<Settlement> GetByIdAsync(FlexAppContextBridge context, string id);
        Task<Settlement> GetWaiverDetailsByIdAsync(FlexAppContextBridge context, string id);

        Task<List<Settlement>> GetByStatusesForCreatorAsync(FlexAppContextBridge context, string creatorId,HashSet<string> status);
        Task<List<Settlement>> GetSettlementsAssignedToAsync(FlexAppContextBridge context, string userId);
        Task<List<SettlementQueueProjection>> GetByStatusForAssignedUserAsync(FlexAppContextBridge context, string userId, string status);

        // Persists changes to the Settlement aggregate.
        Task SaveAsync<TEntity>(FlexAppContextBridge context, TEntity entity) where TEntity : TFlex;

        Task<IReadOnlyCollection<WaiverDetail>> GetMySettlementsWaiversBySettlementIdAsync(FlexAppContextBridge context, string settlementId);

        Task<IReadOnlyCollection<InstallmentDetail>> GetMySettlementsInstallmentsBySettlementIdAsync(FlexAppContextBridge context, string settlementId);

        Task<IReadOnlyCollection<SettlementDocument>> GetMySettlementsDocumentsBySettlementIdAsync(FlexAppContextBridge context, string settlementId);


        Task<List<SettlementQueueProjection>> GetQueueProjectionsByWorkflowInstanceId(FlexAppContextBridge context, string workflowInstanceId);
        Task<bool> IsUserAuthorizedForStep(FlexAppContextBridge flexAppContext, 
            string domainId, string workflowName, string stepName, string userId);
        Task<bool> IsUserAuthorized(string domainId, string workflowName, string stepName, string userId);
        Task GenerateSettlementLetter(string outputPdfPath, string templatePath, SettlementLetterDto dto);

        Task<SettlementQueueProjection> GetQueueProjectionBySettlementIdAsync(FlexAppContextBridge context, string settlementId, string applicationUserId);

    }
}
