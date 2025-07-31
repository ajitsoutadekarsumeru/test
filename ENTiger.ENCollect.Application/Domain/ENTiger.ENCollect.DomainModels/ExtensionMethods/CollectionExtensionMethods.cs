using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class CollectionExtensionMethods
    {
        public static IQueryable<T> ByReadyForBatchWorkflowState<T>(this IQueryable<T> model, CollectionWorkflowState ReadyForBatch, CollectionWorkflowState CollectionAcknowledged) where T : Collection
        {
            return model.Where((T c) => c.CollectionWorkflowState.Name == ReadyForBatch.Name || c.CollectionWorkflowState.Name == CollectionAcknowledged.Name);
        }

        public static IQueryable<T> ByCollectionId<T>(this IQueryable<T> model, string value) where T : Collection
        {
            return model.Where(c => c.CustomId == value || c.Id == value);
        }

        public static IQueryable<T> ByReceiptType<T>(this IQueryable<T> model, string value) where T : Collection
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.ReceiptType == value);
            }
            return model;
        }


        public static IQueryable<T> CollectionForCancellationWorkFlowState<T>(this IQueryable<T> model, CollectionWorkflowState receivebycollector, CollectionWorkflowState collectionAcknowledged, CollectionWorkflowState readyForBatch, CollectionWorkflowState errorinCBS) where T : Collection
        {
            return model.Where(c => c.CollectionWorkflowState.Name == receivebycollector.Name || c.CollectionWorkflowState.Name == collectionAcknowledged.Name || c.CollectionWorkflowState.Name == readyForBatch.Name || c.CollectionWorkflowState.Name == errorinCBS.Name);
        }

        public static IQueryable<T> IncludeCollectionState<T>(this IQueryable<T> model) where T : Collection
        {
            return model.FlexInclude(c => c.CollectionWorkflowState);
        }

        public static IQueryable<T> ByCollectionIds<T>(this IQueryable<T> model, List<string> Ids) where T : TFlex
        {
            return model.Where(i => Ids.Contains(i.Id));
        }

        public static IQueryable<T> ByAckingAgent<T>(this IQueryable<T> model, string ackingAgentId) where T : Collection
        {
            return model.Where(c => c.AckingAgentId == ackingAgentId);
        }

        public static IQueryable<T> ByPaymentMode<T>(this IQueryable<T> model, string value) where T : Collection
        {
            return model.Where(c => c.CollectionMode == value);
        }

        public static IQueryable<T> IncludeAccount<T>(this IQueryable<T> model) where T : Collection
        {
            return model.FlexInclude(x => x.Account);
        }

        public static IQueryable<T> ByDateRange<T>(this IQueryable<T> model, DateTime FromDate, DateTime ToDate) where T : Collection
        {
            DateTime startDate = FromDate.Date;
            DateTime endDate = ToDate.Date.AddDays(1);

            model = model.Where(c => c.CreatedDate >= startDate && c.CreatedDate < endDate);

            return model;
        }

        public static IQueryable<T> ByProductGroup<T>(this IQueryable<T> model, string value) where T : Collection
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(c => c.Account.ProductGroup == value);
            }
            return model;
        }


        public static IQueryable<T> ByCollectionAgencyId<T>(this IQueryable<T> model, string agencyId) where T : Collection
        {
            if (!string.IsNullOrEmpty(agencyId))
            {
                model = model.Where(c => c.CollectionOrgId == agencyId);
            }
            return model;
        }

        public static IQueryable<T> ByCollector<T>(this IQueryable<T> model, string value) where T : Collection
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CollectorId == value);
            }
            return model;
        }

        public static IQueryable<T> ByReceiptCollector<T>(this IQueryable<T> model, string collectorId) where T : Receipt
        {
            if (!string.IsNullOrEmpty(collectorId))
            {
                model = model.Where(c => c.CollectorId == collectorId);
            }
            return model;
        }

        public static IQueryable<T> ReceiptWorkflowState<T>(this IQueryable<T> model, ReceiptWorkflowState ReceiptAllocatedToCollector) where T : Receipt
        {
            return model.Where(c => c.ReceiptWorkflowState.Name == ReceiptAllocatedToCollector.Name);

            // return model;
        }

        public static IQueryable<T> ByReceiptNo<T>(this IQueryable<T> model, string receiptNo) where T : Collection
        {
            if (!string.IsNullOrEmpty(receiptNo))
            {
                model = model.Where(c => c.CustomId == receiptNo);
            }

            return model;
        }

        public static IQueryable<T> ByReceiptIds<T>(this IQueryable<T> model, List<string> ReceiptIds) where T : Collection
        {
            if (ReceiptIds.Count > 0)
            {
                model = model.Where(c => ReceiptIds.Contains(c.ReceiptId));
            }
            return model;
        }

        public static IQueryable<T> ByCollectionOrgId<T>(this IQueryable<T> model, string orgId) where T : Collection
        {
            if (!String.IsNullOrEmpty(orgId))
            {
                model = model.Where(c => c.CollectionOrgId == orgId);
            }
            return model;
        }

        public static IQueryable<T> ByCollectionAccountNo<T>(this IQueryable<T> model, string agreementId) where T : Collection
        {
            if (!string.IsNullOrEmpty(agreementId))
            {
                model = model.Where(c => c.Account.AGREEMENTID == agreementId || c.Account.CustomId == agreementId);
            }

            return model;
        }

        public static IQueryable<T> ByCollectionAccountId<T>(this IQueryable<T> model, string agreementId) where T : Collection
        {
            if (!string.IsNullOrEmpty(agreementId))
            {
                model = model.Where(c => c.AccountId == agreementId);
            }

            return model;
        }

        public static IQueryable<T> ByCollectionCustomerName<T>(this IQueryable<T> model, string customerName) where T : Collection
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                model = model.Where(c => c.Account.CUSTOMERNAME == customerName);
            }

            return model;
        }

        public static IQueryable<T> OnDate<T>(this IQueryable<T> model, DateTime value) where T : Collection
        {
            //TODO
            // model = model.Where(c => DbFunctions.TruncateTime((c.PaymentDate)) == collectionDate);
            model = model.Where(c => c.CollectionDate >= value.Date && c.CollectionDate < value.Date.AddDays(1));
            return model;
        }

        public static IQueryable<T> ByCollectionsForMonth<T>(this IQueryable<T> model, DateTime value) where T : Collection
        {
            if (value != DateTime.MinValue)
            {
                DateTime StartDate = new DateTime(value.Year, value.Month, 1);
                DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
                model = model.Where(c => c.CollectionDate >= StartDate.Date && c.CollectionDate < EndDate.Date.AddDays(1));
            }
            return model;
        }

        public static IQueryable<T> AckDateRange<T>(this IQueryable<T> model, DateTime FromDate, DateTime ToDate) where T : Collection
        {
            bool IsFromDateNull = false;
            bool IsToDateNull = false;

            if(FromDate == DateTime.MinValue)
            {
                IsFromDateNull = true;
                FromDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, 0, 0, 0, DateTimeKind.Utc);
            }
            if (ToDate == DateTime.MinValue)
            {
                IsToDateNull = true;
                FromDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, 23, 59, 59, DateTimeKind.Utc);
            }

            model = model.Where(c =>
                    (IsFromDateNull || c.AcknowledgedDate >= FromDate)
                    && (IsToDateNull || c.AcknowledgedDate <= ToDate));

            return model;
        }

        public static IQueryable<T> CollectionDateRange<T>(this IQueryable<T> model, DateTime? FromDate, DateTime? ToDate) where T : Collection
        {
            if (FromDate != null && FromDate != DateTime.MinValue)
            {
                DateTime startDate = FromDate.Value.Date;
                model = model.Where(c => c.CollectionDate >= startDate);
            }
            if (ToDate != null && ToDate != DateTime.MinValue)
            {
                DateTime endDate = ToDate.Value.Date.AddDays(1);
                model = model.Where(c => c.CollectionDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> WithMonthAndYear<T>(this IQueryable<T> model, Int64 Month, Int64 Year) where T : Collection
        {
            DateTime startDate = new DateTime((int)Year, (int)Month, 1, 0, 0, 0);
            DateTime endDate = startDate.AddMonths(1); // First day of next month

            return model.Where(x => x.CollectionDate >= startDate && x.CollectionDate < endDate);
        }

        public static IQueryable<T> ByCollectionPaymentMode<T>(this IQueryable<T> model, string PaymentMode) where T : Collection
        {
            if (!string.IsNullOrEmpty(PaymentMode))
            {
                model = model.Where(c => c.CollectionMode == PaymentMode);
            }
            return model;
        }


        public static IQueryable<T> ByCollectionBatchId<T>(this IQueryable<T> model, string CollectionBatchId) where T : Collection
        {
            model = model.Where(c => c.CollectionBatchId == CollectionBatchId);

            return model;
        }

        public static IQueryable<T> ByCollectionBatchIds<T>(this IQueryable<T> model, List<string> values) where T : Collection
        {
            return model.Where(i => values.Contains(i.CollectionBatchId));
        }

        public static IQueryable<T> IncludeCollectionOrg<T>(this IQueryable<T> model) where T : Collection
        {
            //TODO
            //model = model.Include(c => c.CollectionOrg);

            return model;
        }

        public static IQueryable<T> IncludeCheque<T>(this IQueryable<T> model) where T : Collection
        {
            //model = model.Include(c => c.Cheque);

            return model;
        }

        public static IQueryable<T> IncludeCash<T>(this IQueryable<T> model) where T : Collection
        {
            //model = model.Include(c => c.Cash);

            return model;
        }

        public static IQueryable<T> IncludeCollector<T>(this IQueryable<T> model) where T : Collection
        {
            model = model.FlexInclude(c => c.Collector);

            return model;
        }

        public static IQueryable<T> IncludeCollectionCollectorEmail<T>(this IQueryable<T> model) where T : Collection
        {
            //TODO
            //model = model.Include(c => c.Collector);
            //model = model.Include(c => c.Collector.Responsibles);

            //model = model.Include(c => c.Collector.Responsibles.FirstOrDefault().Commisioner);
            return model;
        }

        public static IQueryable<T> ByAccountId<T>(this IQueryable<T> model, string value) where T : Collection
        {
            if (!String.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.AccountId == value);
            }
            return model;
        }

        public static IQueryable<T> ByInstrumentDateRange<T>(this IQueryable<T> model, DateTime? FromDate, DateTime? ToDate) where T : Collection
        {
            if(FromDate != null && FromDate != DateTime.MinValue)
            {
                DateTime startDate = FromDate.Value.Date;
                model = model.Where(c => c.Cheque.InstrumentDate >= startDate);
            }
            if (ToDate != null && ToDate != DateTime.MinValue)
            {
                DateTime startDate = ToDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(c => c.Cheque.InstrumentDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByCheque<T>(this IQueryable<T> model) where T : Collection
        {
            model = model.Where(c => c.CollectionMode == CollectionModeEnum.Cheque.Value);

            return model;
        }


        public static IQueryable<T> ByCollectionLoggedInUserId<T>(this IQueryable<T> model, string value) where T : Collection
        {
            model = model.Where(c => c.CreatedBy == value);

            return model;
        }

        public static IQueryable<T> ByCollectionTransactionNumber<T>(this IQueryable<T> model, string value) where T : Collection
        {
            model = model.Where(c => c.TransactionNumber == value);

            return model;
        }

        public static IQueryable<T> ByCollectionTransactionDate<T>(this IQueryable<T> model, DateTime FromDate, DateTime ToDate) where T : Collection
        {
            DateTime startDate = FromDate.Date;
            DateTime endDate = ToDate.Date.AddDays(1);

            return model.Where(c => c.CreatedDate >= startDate && c.CreatedDate < endDate);
        }

        public static IQueryable<T> ByCustomerName<T>(this IQueryable<T> model, string customerName) where T : Collection
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                return model.Where(c => c.Account.CUSTOMERNAME == customerName);
            }
            return model;
        }


        public static IQueryable<T> IncludeCollectionWorkflowState<T>(this IQueryable<T> model) where T : Collection
        {
            return model.FlexInclude(cw => cw.CollectionWorkflowState);
        }

        public static IQueryable<T> ByCollectionWorkflowStateList<T>(this IQueryable<T> model, List<CollectionWorkflowState> value) where T : Collection
        {
            //return collection.Where(c => c.CollectionWorkflowState.GetType() == state.GetType());
            List<string> values = value.Select(c => c.ToString()).ToList();
            return model.Where(c => values.Contains(c.CollectionWorkflowState.Name));
        }

        public static IQueryable<T> ByCollectionWorkflowState<T>(this IQueryable<T> model, CollectionWorkflowState value) where T : Collection
        {
            return model.Where(c => c.CollectionWorkflowState.Name == value.Name);
        }

        public static IQueryable<T> ByCollectionAgencyCode<T>(this IQueryable<T> model, string value) where T : Collection
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CollectionOrg.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByAccountNoProductGroup<T>(this IQueryable<T> model, string value) where T : Collection
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(c => c.Account.ProductGroup == value);
            }
            return model;
        }


        public static IQueryable<T> ByCollectionCollectorId<T>(this IQueryable<T> model, string value) where T : Collection
        {
            return model.Where(c => c.CollectorId == value);
        }

        public static IQueryable<T> ByWorkflowState<T>(this IQueryable<T> model, CollectionWorkflowState value) where T : Collection
        {
            return model.Where(c => c.CollectionWorkflowState.Name == value.Name);
        }
        public static IQueryable<T> ByToday<T>(this IQueryable<T> model) where T : Collection
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);
            return model = model.Where(c => c.CreatedDate >= startDate && c.CreatedDate < endDate);
        }

    }
}