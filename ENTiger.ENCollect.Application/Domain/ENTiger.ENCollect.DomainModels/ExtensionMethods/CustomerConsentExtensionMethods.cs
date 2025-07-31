
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class CustomerConsentExtensionMethods
    {
        public static IQueryable<T> ByConsentAccountId<T>(this IQueryable<T> model, string value) where T : CustomerConsent
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.AccountId == value);
            }
            return model;
        }

        public static IQueryable<T> ByConsentId<T>(this IQueryable<T> model, string value) where T : CustomerConsent
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Id == value);
            }
            return model;
        }

        public static IQueryable<T> ByUserId<T>(this IQueryable<T> model, string value) where T : CustomerConsent
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.UserId == value);
            }
            return model;
        }

        public static IQueryable<T> ByConsentStatus<T>(this IQueryable<T> model, string value) where T : CustomerConsent
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Status == value);
            }
            return model;
        }

        public static IQueryable<T> ByAppointmentDate<T>(this IQueryable<T> model, DateTime? appointmentDate) where T : CustomerConsent
        {
            model = model.Where(a => a.IsActive == true);
            if (appointmentDate != null && appointmentDate != DateTime.MinValue)
            {
                model = model.Where(a => a.RequestedVisitTime == appointmentDate && a.ExpiryTime >= appointmentDate);
            }           
            return model;
        }

        public static IQueryable<T> ByDateToExpire<T>(this IQueryable<T> model, DateTime? expiryDate) where T : CustomerConsent
        {
            model = model.Where(a => a.IsActive == true);
            if (expiryDate != null && expiryDate != DateTime.MinValue)
            {
                model = model.Where(a => a.RequestedVisitTime < expiryDate && a.Status == CustomerConsentStatusEnum.Pending.Value);
            }
            return model;
        }

        public static IQueryable<T> IncludeApplicationUser<T>(this IQueryable<T> customerConsent) where T : CustomerConsent
        {
            return customerConsent.FlexInclude(x => x.User);
        }

        public static IQueryable<T> IncludeAccount<T>(this IQueryable<T> customerConsent) where T : CustomerConsent
        {
            return customerConsent.FlexInclude(x => x.Account);
        }
        public static IQueryable<T> BySecureToken<T>(this IQueryable<T> model, string value) where T : CustomerConsent
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.SecureToken == value);
            }
            return model;
        }
    }
}
