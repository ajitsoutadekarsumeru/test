
using Microsoft.IdentityModel.Tokens;

namespace ENTiger.ENCollect
{
    public static class FeedbackExtensionMethods
    {
        public static IQueryable<T> ByFeedbackCollector<T>(this IQueryable<T> model, string value) where T : Feedback
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CollectorId == value);
            }
            return model;
        }

        public static IQueryable<T> ByFeedbackOnDate<T>(this IQueryable<T> model, DateTime value) where T : Feedback
        {
            model = model.Where(c => c.FeedbackDate >= value.Date && c.FeedbackDate < value.Date.AddDays(1));
            return model;
        }

        public static IQueryable<T> ByFeedbackForMonth<T>(this IQueryable<T> model, DateTime value) where T : Feedback
        {
            if (value != DateTime.MinValue)
            {
                DateTime StartDate = new DateTime(value.Year, value.Month, 1);
                DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
                model = model.Where(c => c.FeedbackDate >= StartDate.Date && c.FeedbackDate < EndDate.Date.AddDays(1));
            }
            return model;
        }
        public static IQueryable<T> ByFeedbackToday<T>(this IQueryable<T> model) where T : Feedback
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);
            return model.Where(a => a.FeedbackDate >= startDate && a.FeedbackDate < endDate);
        }
        public static IQueryable<T> ByAccountIds<T>(this IQueryable<T> model, List<string> loanAccountIds) where T : Feedback
        {
            if (loanAccountIds != null && loanAccountIds.Count > 0)
            {
                model = model.Where(a => loanAccountIds.Contains(a.AccountId));
            }
            return model;
        }
    }
}
