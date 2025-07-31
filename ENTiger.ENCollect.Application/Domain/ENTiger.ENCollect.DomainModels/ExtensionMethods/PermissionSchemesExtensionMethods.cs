namespace ENTiger.ENCollect
{
    /// <summary>
    /// Extension methods for querying PermissionSchemes.
    /// </summary>
    public static class PermissionSchemesExtensionMethods
    {
        public static IQueryable<T> ByPermissionSchemeName<T>(this IQueryable<T> model, string value) where T : PermissionSchemes
        {
            return model = model.Where(c => c.Name == value);
        }
    }
}
