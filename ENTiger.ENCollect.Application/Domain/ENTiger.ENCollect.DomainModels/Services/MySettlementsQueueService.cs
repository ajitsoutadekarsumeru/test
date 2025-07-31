using ENTiger.ENCollect.SettlementModule;
using System.Drawing.Printing;

namespace ENTiger.ENCollect
{
    public class MySettlementsQueueService
    {
        private readonly ISettlementRepository _repoSettlement;

        public MySettlementsQueueService(
            ISettlementRepository repoSettlement)
        {
            _repoSettlement = repoSettlement;
        }

        public async Task<List<MySettlementQueueDetailsDto>> GetMySettlementQueueDetailsAsync(
            FlexAppContextBridge context,
            string userId,
            string status, List<SettlementQueueProjection> settlementQueue)
        {           
            var now = DateTime.Now;
            var list = new List<MySettlementQueueDetailsDto>();

            foreach (var queueItem in settlementQueue)
            {
                var settlement = queueItem.Settlement;
                // 1) Null‐safe waiver details
                var waivers = settlement.WaiverDetails ?? Enumerable.Empty<WaiverDetail>();
                var interest = waivers
                                .FirstOrDefault(w =>
                                w.ChargeType.Equals(
                                WaiverCargeTypeEnum.Interest.Value,
                                StringComparison.OrdinalIgnoreCase));

                var principal = waivers
                                .FirstOrDefault(w =>
                                w.ChargeType.Equals(
                                   WaiverCargeTypeEnum.Principal.Value,
                                   StringComparison.OrdinalIgnoreCase));

                decimal principalWaiverAmount = principal?.WaiverAmount ?? 0m;
                decimal principalWaiverPercentage = principal?.WaiverPercent ?? 0m;

                decimal interestWaiverAmount = interest?.WaiverAmount ?? 0m;
                decimal interestWaiverPercentage = interest?.WaiverPercent ?? 0m;

                // 2) Null‐safe status history
                var history = (settlement.StatusHistory ?? Enumerable.Empty<SettlementStatusHistory>())
                    .OrderBy(h => h.ChangedDate)
                    .ToList();

                //fetch last history assignment
                var lastStep = history.LastOrDefault();

                var beforeNames = history
                    .Select(u => u.ChangedByUserId)
                    .ToList();

                var since = lastStep?.ChangedDate ?? settlement.CreatedDate;
                var agingDays = (int)(now - since).TotalDays;

                list.Add(new MySettlementQueueDetailsDto
                {
                    Id = settlement.Id,
                    CustomId = settlement.CustomId,
                    LoanAccountId = settlement.LoanAccountId,
                    NpaFlag = settlement.NPA_STAGEID,
                    Status = SettlementStatusEnum.ByValue(settlement.Status).DisplayName,
                    SettlementAmount = settlement.SettlementAmount,
                    RenegotiationAmount = settlement.RenegotiationAmount,
                    PrincipalWaiverAmount = principalWaiverAmount,
                    PrincipalWaiverPercentage = principalWaiverPercentage,
                    InterestWaiverAmount = interestWaiverAmount,
                    InterestWaiverPercentage = interestWaiverPercentage,
                    ActionedBeforeYou = new ActionedBeforeDto { Count = beforeNames.Count, Names = beforeNames },
                    AgingInCurrentStatusDays = agingDays,
                    WorkflowInstanceId = queueItem.WorkflowInstanceId,
                    WorkflowName = queueItem.WorkflowName,
                    StepName = queueItem.StepName,
                    StepType = queueItem.UIActionContext,
                });

            }
            return list;
        }
    }
}
