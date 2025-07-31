using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class GetUserByAccountTypeExtensionMethods
    {
        public static List<string> ByAccountType<T>(this IQueryable<T> accountability, string accountabilityType) where T : Accountability
        {
            List<string> responsibleIds = new List<string>();
            if (!string.IsNullOrEmpty(accountabilityType))
            {
                responsibleIds = accountability.Where(a => a.AccountabilityTypeId == accountabilityType)
                                                    .Select(x => x.ResponsibleId).ToList();
            }
            return responsibleIds;
        }
    }
}