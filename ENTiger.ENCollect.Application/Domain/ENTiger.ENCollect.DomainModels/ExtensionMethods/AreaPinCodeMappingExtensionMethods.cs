namespace ENTiger.ENCollect
{
    public static class AreaPinCodeMappingExtensionMethods
    {
        public static IQueryable<T> ByPinCodeId<T>(this IQueryable<T> AreaPinCodeMapping, string PinCodeId, string AreaId) where T : AreaPinCodeMapping
        {
            if (!String.IsNullOrEmpty(PinCodeId) || !String.IsNullOrEmpty(AreaId))
            {
                AreaPinCodeMapping = AreaPinCodeMapping.Where(a => a.PinCodeId == PinCodeId && a.AreaId == AreaId);
            }
            return AreaPinCodeMapping;
        }

        public static IQueryable<T> ByPinCodeAreaSearch<T>(this IQueryable<T> AreaPinCodeMapping, string searchParam) where T : AreaPinCodeMapping
        {
            if (!string.IsNullOrWhiteSpace(searchParam))
            {
                return AreaPinCodeMapping.Where(c => c.Area.Name.StartsWith(searchParam) || c.PinCode.Value.StartsWith(searchParam));
            }

            return AreaPinCodeMapping;
        }

        public static IQueryable<T> ByDeleteAreaPinCode<T>(this IQueryable<T> areaPinCodeMapping) where T : AreaPinCodeMapping
        {
            return areaPinCodeMapping.Where(c => c.IsDeleted == false);
        }

        public static IQueryable<T> ByAreaPinCodeMapping<T>(this IQueryable<T> AreaPinCodeMapping, string AreaId) where T : AreaPinCodeMapping
        {
            if (!String.IsNullOrEmpty(AreaId))
            {
                AreaPinCodeMapping = AreaPinCodeMapping.Where(a => a.AreaId == AreaId);
            }
            return AreaPinCodeMapping;
        }
    }
}