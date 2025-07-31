namespace ENTiger.ENCollect
{
    public static class AccountFeedbackExtensionMethods
    {
        public static ICollection<T> ByTodaysPTP<T>(this ICollection<T> feedback) where T : Feedback
        {
            var currentDate = DateTime.Now.Date;

            //TODO
            //feedback = feedback
            //    .Where(a =>
            //            (a.PTPDate.HasValue ? a.PTPDate.Value.Date == currentDate : false
            //            )
            //        ).ToList();

            return feedback;
        }

        public static IQueryable<T> ByTodaysPTP<T>(this IQueryable<T> feedback) where T : Feedback
        {
            var currentDate = DateTime.Now.Date;

            //TODO
            //feedback = feedback
            //    .Where(a =>
            //            DbFunctions.TruncateTime(a.PTPDate) == currentDate
            //            );

            return feedback;
        }

        public static IQueryable<T> ByFeedbackAccountId<T>(this IQueryable<T> model, string value) where T : Feedback
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.AccountId == value);
            }
            return model;
        }

        public static IQueryable<T> ByFeedbackCreateddate<T>(this IQueryable<T> model, DateTime? StartDate) where T : Feedback
        {
            if (StartDate != null)
            {
                model = model.Where(c => c.CreatedDate >= StartDate && c.CreatedDate <= DateTime.Now);
            }
            return model;
        }

        public static IQueryable<T> ByFeedbackAccountIds<T>(this IQueryable<T> model, List<string> accountids) where T : Feedback
        {
            if (accountids.Count() > 0)
            {
                model = model.Where(c => accountids.Contains(c.AccountId));
            }
            return model;
        }
        public static IQueryable<T> ByExcludeAutoTrail<T>(this IQueryable<T> model, string value) where T : Feedback
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => !a.CollectorId.Contains(value));
            }
            return model;
        }


    }
}