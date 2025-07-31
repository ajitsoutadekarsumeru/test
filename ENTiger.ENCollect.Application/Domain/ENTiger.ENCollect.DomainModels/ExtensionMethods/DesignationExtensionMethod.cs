namespace ENTiger.ENCollect
{
    public static class DesignationExtensionMethod
    {
        public static IQueryable<T> ByPermissionSchemeIds<T>(this IQueryable<T> model, List<string> value) where T : Designation
        {
            return model = model.Where(c => value.Contains(c.PermissionSchemeId));
        }
    }
}
