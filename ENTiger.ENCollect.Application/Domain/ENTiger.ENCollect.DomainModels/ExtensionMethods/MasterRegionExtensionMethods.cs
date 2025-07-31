namespace ENTiger.ENCollect
{
    public static class MasterRegionExtensionMethods
    {
        public static IQueryable<T> ByRegionNickNameOrName<T>(this IQueryable<T> regions, string name, string nickName) where T : Regions
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(nickName))
            {
                regions = regions.Where(a => a.Name == name || a.NickName == nickName);
            }
            return regions;
        }


        public static IQueryable<T> ByRegionNickNameOrNameSearch<T>(this IQueryable<T> regions, string searchParam) where T : Regions
        {
            if (!String.IsNullOrEmpty(searchParam))
            {
                return regions.Where(c => c.Name.StartsWith(searchParam) || c.NickName.StartsWith(searchParam));
            }
            return regions;
        }

        public static IQueryable<T> ByCountryRegionMap<T>(this IQueryable<T> regions, string CountryId) where T : Regions
        {
            if (!String.IsNullOrEmpty(CountryId))
            {
                regions = regions.Where(a => a.CountryId == CountryId);
            }
            return regions;
        }

        public static IQueryable<T> ByRegionIdNotEquals<T>(this IQueryable<T> regions, string id) where T : Regions
        {
            if (!String.IsNullOrEmpty(id))
            {
                regions = regions.Where(a => a.Id != id);
            }
            return regions;
        }

        public static IQueryable<T> ByDeleteRegion<T>(this IQueryable<T> regions) where T : Regions
        {
            regions = regions.Where(c => c.IsDeleted == false);

            return regions;
        }
    }
}