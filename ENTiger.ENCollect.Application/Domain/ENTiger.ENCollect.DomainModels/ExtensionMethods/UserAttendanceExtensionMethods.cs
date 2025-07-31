using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using static OfficeOpenXml.ExcelErrorValue;

namespace ENTiger.ENCollect
{
    public static class UserAttendanceExtensionMethods
    {
        public static IQueryable<T> ByUserattendanceDetailsMonthAndYear<T>(this IQueryable<T> model, int month, int year) where T : UserAttendanceDetail
        {
            DateTime startDate = new DateTime(year, month, 1, 0, 0, 0);
            DateTime endDate = startDate.AddMonths(1); // First day of next month

            return model.Where(x => x.Date >= startDate && x.Date < endDate);
        }

        public static IQueryable<T> ByUserAttendanceLogUserId<T>(this IQueryable<T> model, string userId) where T : UserAttendanceLog
        {
            return model.Where(x => x.ApplicationUserId == userId);
        }

        public static IQueryable<T> ByUserAttendanceIsSessionValid<T>(this IQueryable<T> model) where T : UserAttendanceLog
        {
            return model.Where(x => x.IsSessionValid);
        }

        public static IQueryable<T> ByUserAttendanceLogMonthAndYear<T>(this IQueryable<T> model, int month, int year) where T : UserAttendanceLog
        {
            DateTime startDate = new DateTime(year, month, 1, 0, 0, 0);
            DateTime endDate = startDate.AddMonths(1); // First day of next month

            return model.Where(x => x.CreatedDate >= startDate && x.CreatedDate < endDate);
        }

        public static Task<bool> HasUserLoggedInToday(this IQueryable<UserAttendanceLog> query)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return query.AnyAsync(x => x.LogInTime >= today && x.LogInTime < tomorrow);
        }

        public static IQueryable<T> ByUserInactivity<T>(this IQueryable<T> model, int days) where T : UserAttendanceLog
        {
            DateTime inactivityStartDate = DateTime.Today.Date.AddDays(-days);
            return model.Where(x => x.LogInTime <= inactivityStartDate).Distinct();
        }
        // <summary>
        /// Filters the query to include only users who logged in on the given date.
        /// Assumes loginDate is already T-1 (yesterday).
        /// </summary>
        /// <typeparam name="T">Type that inherits from UserAttendanceLog.</typeparam>
        /// <param name="query">Queryable source of UserAttendanceLog.</param>
        /// <param name="loginDate">The specific date to match logins (T-1).</param>
        /// <returns>Queryable filtered for login records on that date.</returns>
        public static IQueryable<T> HasUserLoggedInOnDate<T>(this IQueryable<T> query, DateTime loginDate)
        where T : UserAttendanceLog
        {
            var startOfDay = loginDate.Date;              // T-1 00:00
            var endOfDay = startOfDay.AddDays(1);         // T-1 23:59:59.999 (exclusive)

            return query.Where(x => x.LogInTime >= startOfDay && x.LogInTime < endOfDay);
        }

        /// <summary>
        /// Filters users who logged in via mobile.
        /// </summary>
        public static IQueryable<T> IsMobileLogin<T>(this IQueryable<T> query) where T : UserAttendanceLog
        {
            return query.Where(x => x.TransactionSource == TransactionSourceEnum.Mobile.Value);
        }

        /// <summary>
        /// Filters users whose login is marked as the first login of the day.
        /// </summary>
        public static IQueryable<T> IsFirstLogin<T>(this IQueryable<T> query) where T : UserAttendanceLog
        {
            return query.Where(x => x.IsFirstLogin == true);
        }

        /// <summary>
        /// Filters the query to include only users who logged out on the specified date (assumed T-1).
        /// </summary>
        /// <typeparam name="T">Type that inherits from UserAttendanceLog.</typeparam>
        /// <param name="query">Queryable source of UserAttendanceLog.</param>
        /// <param name="logoutDate">The date to match logout time (typically T-1).</param>
        /// <returns>Queryable filtered for logout records on the specified date.</returns>
        public static IQueryable<T> HasUserLoggedOutOnDate<T>(this IQueryable<T> query, DateTime logoutDate)
            where T : UserAttendanceLog
        {
            var startOfDay = logoutDate.Date;            // T-1 00:00
            var endOfDay = startOfDay.AddDays(1);        // T 00:00 (exclusive)

            return query.Where(x =>
                x.LogOutTime.HasValue &&
                x.LogOutTime.Value >= startOfDay &&
                x.LogOutTime.Value < endOfDay);
        }
        public static IQueryable<T> ByUserAttendanceLogUserIds<T>(this IQueryable<T> model, List<string> values) where T : UserAttendanceLog
        {
            return model.Where(a => a.ApplicationUserId != null && values.Contains(a.ApplicationUserId));
        }

    }
}