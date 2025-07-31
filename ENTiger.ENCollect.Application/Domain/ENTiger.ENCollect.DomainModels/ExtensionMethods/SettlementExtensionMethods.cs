using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class SettlementExtensionMethods
    {
        public static IQueryable<T> ById<T>(this IQueryable<T> settlements, string id) where T : Settlement
        {
            if (!string.IsNullOrEmpty(id))
            {
                settlements = settlements.Where(a => a.Id == id);
            }
            return settlements;
        }
        public static IQueryable<T> ByCreatedBy<T>(this IQueryable<T> settlements, string createdBy) where T : Settlement
        {
            if (!string.IsNullOrEmpty(createdBy))
            {
                settlements = settlements.Where(a => a.CreatedBy == createdBy);
            }
            return settlements;
        }

        public static IQueryable<T> ByStatus<T>(this IQueryable<T> settlements, HashSet<string> status) where T : Settlement
        {
            if (status?.Count > 0)
            {
                settlements = settlements.Where(s => status.Contains(s.Status));
            }
            return settlements;
        }

        public static IQueryable<T> ByStatus<T>(this IQueryable<T> settlements, string status) where T : Settlement
        {
            if (!string.IsNullOrEmpty(status))
            {
                settlements = settlements.Where(a => a.Status == status);
            }
            return settlements;
        }
        public static IQueryable<T> ByCustomId<T>(this IQueryable<T> settlements, string customId) where T : Settlement
        {
            if (!string.IsNullOrEmpty(customId))
            {
                settlements = settlements.Where(a => a.CustomId == customId);
            }
            return settlements;
        }

      

        public static IQueryable<T> ByCreatedDateRange<T>(this IQueryable<T> model, DateTime? FromDate, DateTime? ToDate) where T : Settlement
        {
            if (FromDate != null && FromDate != DateTime.MinValue)
            {
                DateTime startDate = FromDate.Value.Date;
                model = model.Where(c => c.CreatedDate >= startDate.ToDateOnlyOffset());
            }
            if (ToDate != null && ToDate != DateTime.MinValue)
            {
                DateTime endDate = ToDate.Value.Date.AddDays(1);
                model = model.Where(c => c.CreatedDate < endDate.ToDateOnlyOffset());
            }
            return model;
        }

        public static IQueryable<T> ByAgingSinceCreated<T>(this IQueryable<T> query, int? agingFrom, int? agingTo)
            where T : Settlement
        {
            var today = DateTime.Now.Date;

            if (agingFrom.HasValue)
            {
                var createdOnOrBefore = today.AddDays(-agingFrom.Value);
                query = query.Where(q => q.CreatedDate <= createdOnOrBefore.ToDateOnlyOffset());
            }

            if (agingTo.HasValue)
            {
                var createdOnOrAfter = today.AddDays(-agingTo.Value);
                query = query.Where(q => q.CreatedDate >= createdOnOrAfter.ToDateOnlyOffset());
            }

            return query;
        }

        public static IQueryable<T> ByAgingSinceUpdated<T>(this IQueryable<T> query, int? agingFrom, int? agingTo)
            where T : Settlement
        {
            var today = DateTime.Now.Date;

            if (agingFrom.HasValue)
            {
                var createdOnOrBefore = today.AddDays(-agingFrom.Value);
                query = query.Where(q => q.StatusUpdatedOn <= createdOnOrBefore.ToDateOnlyOffset());
            }

            if (agingTo.HasValue)
            {
                var createdOnOrAfter = today.AddDays(-agingTo.Value);
                query = query.Where(q => q.StatusUpdatedOn >= createdOnOrAfter.ToDateOnlyOffset());
            }

            return query;
        }

    }
}