using Microsoft.Extensions.DependencyInjection;

namespace ENTiger.ENCollect
{
    public static class CompanyUserExtensionMethods
    {
        public static IQueryable<T> IncludeDesignation<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.Designation);
        }

        public static IQueryable<T> BuildIncludeDesignation<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude("Designation.Designation");
        }

        public static IQueryable<T> BuildIncludeDepartment<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude("Designation.Department");
        }

        //public static IQueryable<T> IncludeScopeOfWork<T>(this IQueryable<T> model) where T : CompanyUser
        //{
        //    return model.FlexInclude(x => x.ScopeOfWork);
        //}

        //public static IQueryable<T> IncludeARMScopeOfWork<T>(this IQueryable<T> model) where T : CompanyUser
        //{
        //    return model.FlexInclude(x => x.ARMScopeOfWork);
        //}

        public static IQueryable<T> CompanyUserIncludePlaceOfWork<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.PlaceOfWork);
        }

        public static IQueryable<T> CompanyUserCustomerPersona<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.userCustomerPersona);
        }

        public static IQueryable<T> IncludeCompanyUserWFState<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.CompanyUserWorkflowState);
        }

        public static IQueryable<T> IncludeReportingAgencies<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude("ARMScopeOfWork.ReportingAgency");
        }

        public static IQueryable<T> IncludeSupervisingManager<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude("ARMScopeOfWork.SupervisingManager");
        }

        public static IQueryable<T> IncludeScopeOfWorkSupervisingManager<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude("ScopeOfWork.SupervisingManager");
        }

        public static IQueryable<T> ByCompanyUserFirstName<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return model.Where(c => (c.FirstName != null && c.FirstName.StartsWith(value))
                                    || (c.LastName != null && c.LastName.StartsWith(value)));
            }
            return model;
        }


        public static IQueryable<T> ByCompanyUserId<T>(this IQueryable<T> model, string Id) where T : CompanyUser
        {
            return model.Where(c => c.Id == Id);
        }

        public static IQueryable<T> ByBlacklistedUser<T>(this IQueryable<T> model) where T : CompanyUser
        {
            model = model.Where(a => a.IsLocked == true);
            return model;
        }

        public static IQueryable<T> ByCompanyUserEmail<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.PrimaryEMail == value);
            }
            return model;
        }

        public static IQueryable<T> ByCompanyUserWFState<T>(this IQueryable<T> model, CompanyUserWorkflowState state) where T : CompanyUser
        {
            if (state != null)
            {
                model = model.Where(c => c.CompanyUserWorkflowState != null && c.CompanyUserWorkflowState.Name == state.Name
                                                && !c.IsBlackListed);
            }
            return model;
        }

        public static IQueryable<T> ByCompanyUserWorkFlowState<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CompanyUserWorkflowState != null && c.CompanyUserWorkflowState.Name == value && !c.IsBlackListed);
            }
            return model;
        }

        public static IQueryable<T> ByCompanyUserPhone<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.PrimaryMobileNumber == value);
            }
            return model;
        }

        public static IQueryable<T> ByDesignation<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Designation.Any(b => b.DesignationId == value));
            }
            return model;
        }

        public static IQueryable<T> ByDepartment<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Designation.Any(b => b.DepartmentId == value));
            }
            return model;
        }

        public static IQueryable<T> IsDisabled<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.Where(c => !c.IsDeactivated);
        }

        public static IQueryable<T> ByName<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return model.Where(c => (c.FirstName != null && c.FirstName.StartsWith(value))
                                        || (c.LastName != null && c.LastName.StartsWith(value)));
            }
            return model;
        }


        public static IQueryable<T> ByFrontEndUser<T>(this IQueryable<T> model, bool value) where T : CompanyUser
        {
            return model.Where(c => c.IsFrontEndStaff == value);
        }

        public static IQueryable<T> IncludeUserProductScope<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.ProductScopes.Where(i => !i.IsDeleted));
        }

        public static IQueryable<T> IncludeUserGeoScope<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.GeoScopes.Where(i => !i.IsDeleted));
        }

        public static IQueryable<T> IncludeUserBucketScope<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.BucketScopes.Where(w => !w.IsDeleted));
        }

        public static IQueryable<T> ByCompanyUserIds<T>(this IQueryable<T> model, List<string> values) where T : CompanyUser
        {
            return model.Where(c => values.Contains(c.Id));
        }

        public static IQueryable<T> ByCompanyUserCustomId<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(x => x.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByCompanyBranchId<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(c => c.BaseBranchId == value);
            }
            return model;
        }


        public static IQueryable<T> ByCompanyCustomId<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> ByCompanyEmployeeId<T>(this IQueryable<T> model, string value) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.EmployeeId == value);
            }
            return model;
        }

        public static IQueryable<T> IncludeCompanyUserAttendance<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(a => a.UserAttendanceDetails);
        }

        public static IQueryable<T> IncludeCompanyUserAttendanceLog<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(a => a.UserAttendanceLog);
        }

        public static IQueryable<T> ByCompanyUserInactivity<T>(this IQueryable<T> model, int days) where T : CompanyUser
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            CompanyUserWorkflowState approvedState = host.GetFlexStateInstance<CompanyUserApproved>();
            DateTime inactivityStartDate = DateTime.Today.Date.AddDays(-days);

            return model.Where(x => x.CompanyUserWorkflowState.Name == approvedState.Name 
                                    && !x.IsBlackListed 
                                    && !x.IsDeactivated 
                                    && x.UserAttendanceLog.Max(a => a.LogInTime) <= inactivityStartDate);
        }

        public static IQueryable<T> ByCompanyUserAttendanceMonthAndYear<T>(this IQueryable<T> model, int month, int year) where T : CompanyUser
        {
            DateTime startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDate = startDate.AddMonths(1); // First day of next month
            return model.Where(a => a.UserAttendanceDetails.Any(x => x.Date >= startDate && x.Date < endDate));
        }

        public static IQueryable<T> IncludeCompanyUserWorkflow<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.CompanyUserWorkflowState);
        }

        public static IQueryable<T> IncludeBaseBranch<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.BaseBranch);
        }

        public static IQueryable<T> ByCompanyUserAttandenceState<T>(this IQueryable<T> model, CompanyUserWorkflowState value) where T : CompanyUser
        {
            if (value != null)
            {
                model = model.Where(c => c.CompanyUserWorkflowState != null && c.CompanyUserWorkflowState.Name == value.Name);
            }
            return model;
        }

        public static IQueryable<T> ByReportCompanyBranchId<T>(this IQueryable<T> model, string value, List<string> values) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.BaseBranchId == value);
            }
            else if (values != null)
            {
                model = model.Where(c => values.Contains(c.BaseBranchId));
            }
            return model;
        }

        public static IQueryable<T> ByReportDesignation<T>(this IQueryable<T> model, string value1, string value2) where T : CompanyUser
        {
            if (!string.IsNullOrEmpty(value1))
            {
                model = model.Where(c => c.Designation.Any(b => b.DesignationId == value1 || b.DesignationId == value2));
            }
            return model;
        }

        public static IQueryable<T> ByCompanyUserWorkflowStateTFlexId<T>(this IQueryable<T> model, string value) where T : CompanyUserWorkflowState
        {
            return model.Where(x => x.TFlexId == value);
        }

        public static IQueryable<T> IncludeLanguagesStaff<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.Languages);
        }

        public static IQueryable<T> IncludeUserPersona<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.userCustomerPersona);
        }

        public static IQueryable<T> IncludeUserPerformanceBand<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.userPerformanceBand);
        }

        public static IQueryable<T> IncludeWallet<T>(this IQueryable<T> model) where T : ApplicationUser
        {
            return model.FlexInclude(x => x.Wallet);
        }

        public static IQueryable<T> IncludeCompanyUserCreditAccountDetails<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model;//.FlexInclude(x => x.CreditAccountDetails.BankBranch.Bank);
        }

        public static IQueryable<T> IncludeOnlyActiveUsers<T>(this IQueryable<T> model) where T : CompanyUser
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            CompanyUserWorkflowState state = host.GetFlexStateInstance<CompanyUserApproved>();

            return model.Where(c => c.CompanyUserWorkflowState != null && c.CompanyUserWorkflowState.Name == state.Name
                                        && !c.IsBlackListed && !c.IsDeactivated);
        }

        public static IQueryable<T> IncludeAgencyWallet<T>(this IQueryable<T> model) where T : CompanyUser
        {
            return model.FlexInclude(x => x.Wallet);
        }

        public static IQueryable<T> ByCompanyUserType<T>(this IQueryable<T> model, string userType) where T : CompanyUser
        {
            if (userType != null)
            {
                model = model.Where(c => c.UserType == userType);
            }
            return model;
        }

        //     public static IQueryable<T> ByLoggedInuserBaseBranchId<T>(this IQueryable<T> collectionAgencyUser, string userId, string tenantId) where T : CompanyUser
        //{
        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        using IFlexRepository flexRepository = InitFlexDbInstanceBase.I(tenantId).FlexRepository;
        //        CompanyUser loggedInUserParty = flexRepository.Find<CompanyUser>(userId).FirstOrDefault();
        //        if (loggedInUserParty != null && loggedInUserParty.BaseBranchId != null)
        //        {
        //            collectionAgencyUser = collectionAgencyUser.Where((T c) => c.BaseBranchId == loggedInUserParty.BaseBranchId);
        //        }
        //    }

        //    return collectionAgencyUser;
        //}
    }
}