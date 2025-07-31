using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class ApplicationUserExtensionMethods
    {
        public static IQueryable<T> ByCustomIds<T>(this IQueryable<T> TFlex, List<string> Ids) where T : ApplicationUser
        {
            return TFlex.Where(i => i.CustomId != null && Ids.Contains(i.CustomId));
        }
    }
}
