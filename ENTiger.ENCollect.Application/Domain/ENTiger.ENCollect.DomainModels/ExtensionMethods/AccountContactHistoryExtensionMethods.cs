using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class AccountContactHistoryExtensionMethods
    {
        #region AccountContactHistory Filter Methods

        public static IQueryable<T> ByMobileAndAccountId<T>(this IQueryable<T> contactHistory, string accountId) where T : AccountContactHistory
        {
            if (!string.IsNullOrEmpty(accountId))
            {
                contactHistory = contactHistory.Where(i => i.AccountId == accountId && i.ContactType==ContactTypeEnum.Mobile);
            }
            return contactHistory;
        }
        public static IQueryable<T> ByEmailAndAccountId<T>(this IQueryable<T> contactHistory, string accountId) where T : AccountContactHistory
        {
            if (!string.IsNullOrEmpty(accountId))
            {
                contactHistory = contactHistory.Where(i => i.AccountId == accountId && i.ContactType == ContactTypeEnum.Email);
            }
            return contactHistory;
        }
        public static IQueryable<T> ByAddressAndAccountId<T>(this IQueryable<T> contactHistory, string accountId) where T : AccountContactHistory
        {
            if (!string.IsNullOrEmpty(accountId))
            {
                contactHistory = contactHistory.Where(i => i.AccountId == accountId && i.ContactType == ContactTypeEnum.Address);
            }
            return contactHistory;
        }
        public static IQueryable<T> ByContactSource<T>(this IQueryable<T> contactHistory, ContactSourceEnum? contactSource) where T : AccountContactHistory
        {
            if (contactSource.HasValue)
            {
                contactHistory = contactHistory.Where(i => i.ContactSource == contactSource);
            }
            return contactHistory;
        }
        public static IQueryable<T> ByContactType<T>(this IQueryable<T> contactHistory, ContactTypeEnum? contactType) where T : AccountContactHistory
        {
            if (contactType.HasValue)
            {
               contactHistory = contactHistory.Where(i => i.ContactType == contactType);
            }
            return contactHistory;
        }
        public static IQueryable<T> ByContactValue<T>(this IQueryable<T> contactHistory, string contactValue) where T : AccountContactHistory
        {
            if (!string.IsNullOrEmpty(contactValue))
            {
                contactHistory = contactHistory.Where(i => i.ContactValue == contactValue);
            }
            return contactHistory;
        }
        public static IQueryable<T> ByContactAccountId<T>(this IQueryable<T> contactHistory, string accountId) where T : AccountContactHistory
        {
            if (!string.IsNullOrEmpty(accountId))
            {
                contactHistory = contactHistory.Where(i => i.AccountId == accountId);
            }
            return contactHistory;
        }

        #endregion
    }
}