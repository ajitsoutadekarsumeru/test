namespace ENTiger.ENCollect
{
    public static class MasterStateExtensionMethods
    {
        public static IQueryable<T> ByStateNickNameOrName<T>(this IQueryable<T> states, string name, string nickName) where T : State
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(nickName))
            {
                states = states.Where(a => a.Name == name || a.NickName == nickName);
            }
            return states;
        }


        public static IQueryable<T> ByStateNickNameOrNameSearch<T>(this IQueryable<T> states, string searchParam) where T : State
        {
            if (!String.IsNullOrEmpty(searchParam))
            {
                states = states.Where(c => c.Name.StartsWith(searchParam) || c.NickName.StartsWith(searchParam));
            }
            return states;
        }

        public static IQueryable<T> ByStateIdNotEquals<T>(this IQueryable<T> states, string id) where T : State
        {
            if (!String.IsNullOrEmpty(id))
            {
                states = states.Where(a => a.Id != id);
            }
            return states;
        }

        public static IQueryable<T> ByDeleteState<T>(this IQueryable<T> states) where T : State
        {
            states = states.Where(c => c.IsDeleted == false);

            return states;
        }

        public static IQueryable<T> ByStateRegionmap<T>(this IQueryable<T> states, string regionId) where T : State
        {
            if (!String.IsNullOrEmpty(regionId))
            {
                states = states.Where(a => a.RegionId == regionId);
            }
            return states;
        }
    }
}