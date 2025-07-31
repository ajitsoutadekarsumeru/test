namespace ENTiger.ENCollect
{
    public static class EnabledPermissionExtensionMethods
    {
        public static IQueryable<T> ByPermissionIds<T>(this IQueryable<T> model, List<string> value) where T : EnabledPermission
        {
            return model = model.Where(c => value.Contains(c.PermissionId));
        }
    }
}
