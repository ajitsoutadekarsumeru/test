using System.Linq;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Extension methods for querying Permission.
    /// </summary>
    public static class PermissionExtensionMethods
    {
        public static IQueryable<T> ByIds<T>(this IQueryable<T> model, List<string> values) where T : Permissions
        {
            return model = model.Where(c => values.Contains(c.Id));
        }
        public static IQueryable<T> ByName<T>(this IQueryable<T> model, string value) where T : Permissions
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Name.Contains(value));
            }
            return model;
        }
    }
}
