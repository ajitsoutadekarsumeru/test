using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class CollectionBatchExtensionMethod
    {
        public static IQueryable<T> ByCollectionBatchIds<T>(this IQueryable<T> user, List<string> Ids) where T : CollectionBatch
        {
            return user.Where(i => Ids.Contains(i.Id));
        }

        /*public static IQueryable<T> ByCollectionOrg<T>(this IQueryable<T> CollectionBatchs, string loggedinUserId, string tenantId) where T : CollectionBatch
        {
            string CommisionerId;
            using (IFlexRepository _repoUser = InitFlexEF.GetFactory(tenantId).FlexRepository)
            {
                var loggedinUser = _repoUser.Find<ApplicationUser>(loggedinUserId).FirstOrDefault();
                var loggedinUserAccountability = loggedinUser?.GetAccountabilityAsResponsible(_repoUser).FirstOrDefault();
                CommisionerId = loggedinUserAccountability?.CommisionerId;
                if (CommisionerId != null)
                {
                    CollectionBatchs = CollectionBatchs.Where(cm => cm.CollectionBatchOrgId == CommisionerId);
                }
            }

            return CollectionBatchs;
        }*/

        public static IQueryable<T> ByCollectionOrg<T>(this IQueryable<T> CollectionBatchs, string OrgId, bool isCollectionOrg) where T : CollectionBatch
        {
            if (isCollectionOrg)
            {
                CollectionBatchs = CollectionBatchs.Where(cm => cm.CollectionBatchOrgId == OrgId);
            }
            return CollectionBatchs;
        }

        public static IQueryable<T> ByBatchCustomId<T>(this IQueryable<T> CollectionBatch, string customId) where T : CollectionBatch
        {
            if (!String.IsNullOrEmpty(customId))
            {
                CollectionBatch = CollectionBatch.Where(cm => cm.CustomId == customId);
            }
            return CollectionBatch;
        }

        public static IQueryable<T> ById<T>(this IQueryable<T> CollectionBatch, string Id) where T : CollectionBatch
        {
            CollectionBatch = CollectionBatch.Where(cm => cm.Id == Id);

            return CollectionBatch;
        }

        public static IQueryable<T> ByIds<T>(this IQueryable<T> user, List<string> Ids) where T : CollectionBatch
        {
            return user.Where(i => Ids.Contains(i.Id));
        }

        public static IQueryable<T> GetByDateRange<T>(this IQueryable<T> CollectionBatch, DateTime? FromDate, DateTime? ToDate) where T : CollectionBatch
        {
            bool hasFromDate = FromDate.HasValue && FromDate != DateTime.MinValue;
            bool hasToDate = ToDate.HasValue && ToDate != DateTime.MinValue;

            if (!hasFromDate && !hasToDate)
            {
                return CollectionBatch;
            }
            else if (hasFromDate && hasToDate)
            {
                DateTime startDate = FromDate.Value.Date;
                DateTime endDate = ToDate.Value.Date.AddDays(1);
                return CollectionBatch.Where(c => c.CreatedDate >= startDate && c.CreatedDate < endDate);
            }
            else if (hasFromDate && !hasToDate)
            {
                DateTime startDate = FromDate.Value.Date;
                return CollectionBatch.Where(c => c.CreatedDate >= startDate);
            }
            else if (!hasFromDate && hasToDate)
            {
                DateTime endDate = ToDate.Value.Date.AddDays(1);
                return CollectionBatch.Where(c => c.CreatedDate < endDate);
            }

            return CollectionBatch;
        }

        public static IQueryable<T> ByCollectionBatchPaymentMode<T>(this IQueryable<T> CollectionBatch, string PaymentMode) where T : CollectionBatch
        {
            if (!String.IsNullOrEmpty(PaymentMode))
            {
                CollectionBatch = CollectionBatch.Where(x => x.Collections.Any(y => y.CollectionMode == PaymentMode));
            }
            return CollectionBatch;
        }


        public static IQueryable<T> ByCollectionBatchProductGroup<T>(this IQueryable<T> CollectionBatch, string ProductGroup) where T : CollectionBatch
        {
            if (!String.IsNullOrEmpty(ProductGroup))
            {
                if (string.Equals(ProductGroup, ProductGroupEnum.HFC.Value, StringComparison.OrdinalIgnoreCase))
                {
                    CollectionBatch = CollectionBatch.Where(x => x.Collections.Any(y => y.Account.PRODUCT.Substring(0, 3) == "HFC"));
                }
                else
                {
                    CollectionBatch = CollectionBatch.Where(x => x.Collections.Any(y => y.Account.PRODUCT.Substring(0, 2) != "HF"));
                }
            }
            return CollectionBatch;
        }


        public static IQueryable<T> ByWorkFlowState<T>(this IQueryable<T> CollectionBatch, CollectionBatchWorkflowState BatchCreated) where T : CollectionBatch
        {
            if (BatchCreated != null)
            {
                return CollectionBatch.Where(c => c.CollectionBatchWorkflowState.Name == BatchCreated.Name);
            }
            else
            {
                return CollectionBatch;
            }
        }

        public static IQueryable<T> ByBatchCreatedAndAckWorkFLowState<T>(this IQueryable<T> collection, CollectionBatchWorkflowState BatchCreated, CollectionBatchWorkflowState Ackbatch) where T : CollectionBatch
        {
            return collection.Where(c => c.CollectionBatchWorkflowState.Name == BatchCreated.Name || c.CollectionBatchWorkflowState.Name == Ackbatch.Name);
        }

        public static IQueryable<T> ByBatchCreatedAndBBPayBatchCreatedWorkFLowState<T>(this IQueryable<T> collectionBatch, CollectionBatchWorkflowState BatchCreated, CollectionBatchWorkflowState BBPayBatchCreated) where T : CollectionBatch
        {
            return collectionBatch.Where(c => c.CollectionBatchWorkflowState.Name == BatchCreated.Name || c.CollectionBatchWorkflowState.Name == BBPayBatchCreated.Name);
        }

        public static IQueryable<T> ByCollectionBatchOrgId<T>(this IQueryable<T> CollectionBatch, string CollectionBatchOrgId) where T : CollectionBatch
        {
            if (!String.IsNullOrEmpty(CollectionBatchOrgId))
            {
                CollectionBatch.Where(c => c.CollectionBatchOrgId == CollectionBatchOrgId);
            }
            return CollectionBatch;
        }

        public static IQueryable<T> CollectionBatchWorkflowState<T>(this IQueryable<T> CollectionBatch, CollectionBatchWorkflowState receivebycollector) where T : CollectionBatch
        {
            return CollectionBatch.Where(c => c.CollectionBatchWorkflowState.Name == receivebycollector.Name);
        }

        public static IQueryable<T> ByCreatedUser<T>(this IQueryable<T> CollectionBatch, string loggedinUserId) where T : CollectionBatch
        {
            return CollectionBatch.Where(c => c.CreatedBy == loggedinUserId);
        }

        public static IQueryable<T> ByBatchCreatedWorkFLowState<T>(this IQueryable<T> collection, CollectionBatchWorkflowState BatchCreated) where T : CollectionBatch
        {
            return collection.Where(c => c.CollectionBatchWorkflowState.Name == BatchCreated.Name);
        }

        public static IQueryable<T> ByCollectionBatchdWorkFLowState<T>(this IQueryable<T> collectionBatch, CollectionBatchWorkflowState status) where T : CollectionBatch
        {
            if (status != null)
            {
                return collectionBatch.Where(c => c.CollectionBatchWorkflowState.Name == status.Name);
            }
            return collectionBatch;
        }

        public static IQueryable<T> ByCollectionBatchType<T>(this IQueryable<T> CollectionBatch, string BatchType) where T : CollectionBatch
        {
            if (!String.IsNullOrEmpty(BatchType))
            {
                CollectionBatch = CollectionBatch.Where(x => x.BatchType == BatchType);
            }
            return CollectionBatch;
        }

        public static IQueryable<T> IncludeCollectionBatchWorkflow<T>(this IQueryable<T> model) where T : CollectionBatch
        {
            return model.FlexInclude(x => x.CollectionBatchWorkflowState);
        }

        public static IQueryable<T> ByCollectionBatchPaySlipIds<T>(this IQueryable<T> model, List<string> values) where T : CollectionBatch
        {
            return model.Where(i => values.Contains(i.PayInSlipId));
        }

        public static IQueryable<T> ByCollectionBatchPaySlipId<T>(this IQueryable<T> model,string payinslipid) where T : CollectionBatch
        {
            return model.Where(i => i.PayInSlipId == payinslipid);
        }
    }
}