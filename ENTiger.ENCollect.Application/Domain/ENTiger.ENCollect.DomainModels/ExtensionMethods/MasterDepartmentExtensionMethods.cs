namespace ENTiger.ENCollect
{
    public static class MasterDepartmentExtensionMethods
    {
        public static IQueryable<T> ByDepartmentName<T>(this IQueryable<T> departments, string name, string code) where T : Department
        {
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(code))
            {
                departments = departments.Where(a => (a.Name ?? "") == name || (a.Code ?? "") == code);
            }

            return departments;
        }


        public static IQueryable<T> ByDepartmentNameSearch<T>(this IQueryable<T> departments, string searchParam) where T : Department
        {
            if (!string.IsNullOrEmpty(searchParam))
            {
                departments = departments.Where(c => c.Name.StartsWith(searchParam) || c.DepartmentTypeId.StartsWith(searchParam));
            }

            return departments;
        }

        public static IQueryable<T> ByDepartmentIdNotEquals<T>(this IQueryable<T> departments, string id) where T : Department
        {
            if (!String.IsNullOrEmpty(id))
            {
                departments = departments.Where(a => a.Id != id);
            }
            return departments;
        }

        public static IQueryable<T> ByDeleteDepartment<T>(this IQueryable<T> departments) where T : Department
        {
            departments = departments.Where(c => c.IsDeleted == false);

            return departments;
        }

        public static IQueryable<T> ByDepartmentCode<T>(this IQueryable<T> departments, string departmentCode) where T : Department
        {
            departments = departments.Where(p => p.Code == departmentCode);

            return departments;
        }

        public static IQueryable<T> CompanyUserDesignations<T>(this IQueryable<T> Department, bool? IsFrontEndStaff) where T : Department
        {
            if (IsFrontEndStaff == true)
            {
                return Department.Where(a => a.DepartmentTypeId != null && a.DepartmentTypeId.StartsWith("FrontEndInternal"));
            }
            else
            {
                return Department.Where(a => a.DepartmentTypeId != null && a.DepartmentTypeId.Contains("Internal"));
            }
        }

        public static IQueryable<T> AgentDepartments<T>(this IQueryable<T> Department) where T : Department
        {
            return Department.Where(a => a.DepartmentTypeId != null && a.DepartmentTypeId.Contains("External"));
        }
    }
}