using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class AreaExtensionMethods
    {
        public static IQueryable<T> ByAreaNickNameOrName<T>(this IQueryable<T> areas, string name, string nickName) where T : Area
        {
            if (!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(nickName))
            {
                areas = areas.Where(a => a.Name == name || a.NickName == nickName);
            }
            return areas;
        }


        public static IQueryable<T> ByAreaNickNameOrNameSearch<T>(this IQueryable<T> areas, string searchParam) where T : Area
        {
            if (!string.IsNullOrWhiteSpace(searchParam))
            {
                return areas.Where(c => c.Name.StartsWith(searchParam) || c.NickName.StartsWith(searchParam));
            }
            return areas;
        }

        public static IQueryable<T> ByAreaIdNotEquals<T>(this IQueryable<T> areas, string id) where T : Area
        {
            if (!String.IsNullOrEmpty(id))
            {
                areas = areas.Where(a => a.Id != id);
            }
            return areas;
        }

        public static IQueryable<T> ByDeleteArea<T>(this IQueryable<T> areas) where T : Area
        {
            return areas.Where(c => c.IsDeleted == false);
        }

        public static IQueryable<T> ByAreaCityMapp<T>(this IQueryable<T> areas, string cityid) where T : Area
        {
            if (!String.IsNullOrEmpty(cityid))
            {
                areas = areas.Where(a => a.CityId == cityid);
            }
            return areas;
        }

        public static IQueryable<T> ByIncludeBaseBranch<T>(this IQueryable<T> areas, string cityid) where T : Area
        {
            if (!String.IsNullOrEmpty(cityid))
            {
                areas = areas.FlexInclude(x => x.BaseBranch).Where(a => a.CityId == cityid);
            }
            return areas;
        }
    }
}