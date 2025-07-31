using AutoMapper;
using AutoMapper.Internal;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Security;
using ENTiger.ENCollect.SettlementModule;
using System.Collections.Generic;
using System.Linq;

namespace ENTiger.ENCollect
{
    public class MySettlementsService
    {
        private readonly ISettlementRepository _repoSettlement;

        private readonly IMapper _mapper;
        public MySettlementsService(
            ISettlementRepository repoSettlement, IMapper mapper)
        {
            _repoSettlement = repoSettlement;
            _mapper = mapper;
        }

        public async Task<MySettlementsSummaryDto> GetMySettlementsSummaryAsync(FlexAppContextBridge context, string userId)
        {
            // Dynamically determine open/closed groups via enum metadata
            var openSettlementStatuses = SettlementStatusEnum.ByGroup("Open").Select(s => s.Value).ToHashSet();
            var closedSettlementStatuses = SettlementStatusEnum.ByGroup("Closed").Select(s => s.Value).ToHashSet();

            // Get All settlements of the user
            var statusSet = openSettlementStatuses.Concat(closedSettlementStatuses).ToHashSet();
            var settlements = await _repoSettlement.GetByStatusesForCreatorAsync(context, userId, statusSet);
            var now = DateTime.Now;

            // In-memory split
            var openSettlements = settlements.Where(s => openSettlementStatuses.Contains(s.Status)).ToList();
            var closedSettlements = settlements.Where(s => closedSettlementStatuses.Contains(s.Status)).ToList();

            // Group by status
            List<CaseGroupSummaryDto> GroupCases(IEnumerable<Settlement> list) =>
                list.GroupBy(s => s.Status)
                    .Select(g => new CaseGroupSummaryDto
                    {
                        Status = g.Key,
                        Count = g.Count(),
                        TotalAmount = g.Sum(s => s.SettlementAmount)
                    })
                    .Append(new CaseGroupSummaryDto
                    {
                        Status = "Total",
                        Count = list.Count(),
                        TotalAmount = list.Sum(s => s.SettlementAmount)
                    })
                    .ToList();

            var openCases = GroupCases(openSettlements);
            var closedCases = GroupCases(closedSettlements);

            // Build aging buckets
            Dictionary<string, int> MakeBuckets(IEnumerable<Settlement> list, bool useStatusDate)
            {
                return new Dictionary<string, int>
                {
                    ["1-5"] = list.Count(s =>
                    {
                        // unify on DateTimeOffset
                        var since = useStatusDate
                            ? s.StatusUpdatedOn
                            : s.CreatedDate;
                        var days = (now - since).Days;
                        return days >= 1 && days <= 5;
                    }),
                    ["6-10"] = list.Count(s => {
                        var since = useStatusDate
                        ? s.StatusUpdatedOn
                        : s.CreatedDate;
                        var days = (now - since).Days;
                        return days >= 6 && days <= 10;
                    }),
                    [">10"] = list.Count(s => {
                        var since = useStatusDate ? s.StatusUpdatedOn : s.CreatedDate;
                        var days = (now - since).Days;
                        return days > 10;
                    })
                };
            }

            return new MySettlementsSummaryDto
            {
                OpenCases = openCases,
                ClosedCases = closedCases,
                AgingFromRequestedCounts = MakeBuckets(openSettlements, useStatusDate: false),
                AgingInCurrentStatusCounts = MakeBuckets(openSettlements, useStatusDate: true)
            };

        }

        public async Task<List<GetMyOpenSettlementsDto>> GetMyOpenSettlementSummaryAsync(FlexAppContextBridge context, string userId)
        {
            // Dynamically determine open groups via enum metadata
            var openSet = SettlementStatusEnum.ByGroup("Open").Select(s => s.Value).ToHashSet();

            // Get All settlements of the user with status
            var settlements = await _repoSettlement.GetByStatusesForCreatorAsync(context, userId, openSet);

            // Group by status
            List<GetMyOpenSettlementsDto> GroupCases(IEnumerable<Settlement> list) =>
                list.GroupBy(s => s.Status)
                    .Select(g => new GetMyOpenSettlementsDto
                    {
                        Status = SettlementStatusEnum.ByValue(g.Key).DisplayName,
                        Count = g.Count(),
                        TotalAmount = g.Sum(s => s.SettlementAmount)
                    })
                    .Append(new GetMyOpenSettlementsDto
                    {
                        Status = "Total",
                        Count = list.Count(),
                        TotalAmount = list.Sum(s => s.SettlementAmount)
                    })
                    .ToList();

            var openCases = GroupCases(settlements);

            return openCases;
        }

        public async Task<List<GetMyClosedSettlementsDto>> GetMyCloseSettlementSummaryAsync(FlexAppContextBridge context, string userId)
        {
            // Dynamically determine closed groups via enum metadata
            var closedSet = SettlementStatusEnum.ByGroup("Closed").Select(s => s.Value).ToHashSet();

            // Get All settlements of the user
            var settlements = await _repoSettlement.GetByStatusesForCreatorAsync(context, userId, closedSet);

            // Group by status
            List<GetMyClosedSettlementsDto> GroupCases(IEnumerable<Settlement> list) =>
                list.GroupBy(s => s.Status)
                    .Select(g => new GetMyClosedSettlementsDto
                    {
                        Status = SettlementStatusEnum.ByValue(g.Key).DisplayName,
                        Count = g.Count(),
                        TotalAmount = g.Sum(s => s.SettlementAmount)
                    })
                    .Append(new GetMyClosedSettlementsDto
                    {
                        Status = "Total",
                        Count = list.Count(),
                        TotalAmount = list.Sum(s => s.SettlementAmount)
                    })
                    .ToList();

            var closedCases = GroupCases(settlements);

            return closedCases;
        }

        public async Task<List<GetMySettlementsAgingByDateDto>>
            GetMySettlementsAgingByDateSummaryAsync(
            FlexAppContextBridge context, List<Settlement> settlements)
        {
            var now = DateTime.Now.Date;


            //calculate settlement ageing
            var settlementsWithDays = settlements
                                            .Select(s => new
                                            {
                                                DaysOld = (now - s.CreatedDate).Days,
                                                Amount = s.SettlementAmount
                                            })
                                            .ToList();
            var settlementsWithAgingMoreThanZero = settlementsWithDays
                                            .Where(s => s.DaysOld > 0)
                                            .ToList();
            // Define aging ranges
            var buckets = new[]
            {
                new { Name = "1-5", FromDay = 1, ToDay = 5 },
                new { Name = "6-10", FromDay = 6, ToDay = 10 },
                new { Name = ">10", FromDay = 11, ToDay = int.MaxValue }
            };

            var settlementsAgingByDate = buckets
                            .Select(bucket =>
                            {
                                var items = settlementsWithDays
                                    .Where(s => s.DaysOld >= bucket.FromDay && s.DaysOld <= bucket.ToDay);

                                return new GetMySettlementsAgingByDateDto
                                {
                                    RangeOfAging = bucket.Name,
                                    Count = items.Count(),
                                    TotalAmount = items.Sum(s => s.Amount)
                                };
                            })
                            .Append(new GetMySettlementsAgingByDateDto
                            {
                                RangeOfAging = "Total",
                                Count = settlementsWithAgingMoreThanZero.Count(),
                                TotalAmount = settlementsWithAgingMoreThanZero.Sum(s => s.Amount)
                            })
                            .ToList();

            return settlementsAgingByDate;
        }

        public async Task<List<GetMySettlementsAgingByStatusDto>> GetMySettlementsAgingByStatusSummaryAsync(
            FlexAppContextBridge context,
            List<Settlement> settlements)
        {
            var now = DateTime.Now.Date;


            //calculate settlement ageing
            var settlementsWithDays = settlements
                                        .Select(s => new
                                        {
                                            DaysOld = (now - s.StatusUpdatedOn).Days,
                                            Amount = s.SettlementAmount
                                        })
                                        .ToList();

            var settlementsWithAgingMoreThanZero = settlementsWithDays
                                           .Where(s => s.DaysOld > 0)
                                           .ToList();
            // Define aging ranges
            var buckets = new[]
            {
                new { Name = "1-5", FromDay = 1, ToDay = 5 },
                new { Name = "6-10", FromDay = 6, ToDay = 10 },
                new { Name = ">10", FromDay = 11, ToDay = int.MaxValue }
            };

            var settlementsAgingByStatus = buckets
                            .Select(bucket =>
                            {
                                var items = settlementsWithDays
                                    .Where(s => s.DaysOld >= bucket.FromDay && s.DaysOld <= bucket.ToDay);

                                return new GetMySettlementsAgingByStatusDto
                                {
                                    RangeOfAging = bucket.Name,
                                    Count = items.Count(),
                                    TotalAmount = items.Sum(s => s.Amount)
                                };
                            })
                            .Append(new GetMySettlementsAgingByStatusDto
                            {
                                RangeOfAging = "Total",
                                Count = settlementsWithAgingMoreThanZero.Count(),
                                TotalAmount = settlementsWithAgingMoreThanZero.Sum(s => s.Amount)
                            })
                            .ToList();

            return settlementsAgingByStatus;
        }
    }
}
