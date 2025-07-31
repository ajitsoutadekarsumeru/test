using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class TFlexCommonExtensionMethod
    {
        public static IQueryable<T> ByTFlexId<T>(this IQueryable<T> TFlex, string id) where T : TFlex
        {
            if (!string.IsNullOrEmpty(id))
            {
                TFlex = TFlex.Where(a => a.Id == id);
            }
            return TFlex;
        }

        public static IQueryable<T> ByTFlexIds<T>(this IQueryable<T> TFlex, List<string> Ids) where T : TFlex
        {
            return TFlex.Where(i => Ids.Contains(i.Id));
        }
    }
}