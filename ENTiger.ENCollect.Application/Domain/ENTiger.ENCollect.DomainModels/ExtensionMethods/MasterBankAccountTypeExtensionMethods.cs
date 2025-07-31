namespace ENTiger.ENCollect
{
    public static class MasterBankAccountTypeExtensionMethods
    {
        public static IQueryable<T> ByDeleteBankAccountType<T>(this IQueryable<T> BankAccountTypes) where T : BankAccountType
        {
            BankAccountTypes = BankAccountTypes.Where(c => c.IsDeleted == false);

            return BankAccountTypes;
        }
    }
}