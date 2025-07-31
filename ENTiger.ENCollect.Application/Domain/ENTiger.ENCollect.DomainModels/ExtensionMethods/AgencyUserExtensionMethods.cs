using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;
using static OfficeOpenXml.ExcelErrorValue;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class AgencyUserExtensionMethods
    {
        public static IQueryable<T> ByLoggedInUser<T>(this IQueryable<T> model, string value, string tenantId) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {

            }
            return model;
        }

        public static IQueryable<AgencyUser> ByLoggedInUserId(this IQueryable<AgencyUser> model, string value)
        {
            return model.Where(cm => cm.Id == value);
        }

        public static IQueryable<T> ByAgentName<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                var searchValue = value.Trim();  // No need to lowercase the search value

                model = model.Where(a => string.Concat(a.FirstName, " ", a.LastName) == searchValue); // Case-insensitive comparison

            }
            return model;
        }


        public static IQueryable<T> ByAgentEmail<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.PrimaryEMail == value);
            }
            return model;
        }

        public static IQueryable<T> ByCollectionAgencyName<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                var searchValue = value.Trim();  // Trim whitespace from the search value

                model = model.Where(c => string.Concat(c.Agency.FirstName) == searchValue);
            }
            return model;
        }

        public static IQueryable<T> ByAgencyId<T>(this IQueryable<T> model, string Id, ApplicationUser usertype) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(Id))
            {
                if (usertype?.GetType() == typeof(AgencyUser))
                {
                    model = model.Where(c => c.AgencyId == Id);
                }

            }
            return model;
        }

        public static IQueryable<T> ByAgentPhoneNo<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.PrimaryMobileNumber == value);
            }
            return model;
        }

        public static IQueryable<T> ByBlacklistedAgencyUser<T>(this IQueryable<T> model) where T : AgencyUser
        {
            model = model.Where(a => a.IsLocked == true);
            return model;
        }

        public static IQueryable<T> ByAgencyUserDesignation<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Designation.Any(b => b.DesignationId == value));
            }
            return model;
        }

        public static IQueryable<T> ByOrgId<T>(this IQueryable<T> model, string value, string tenantId) where T : AgencyUser
        {
            string loggedInUserOrgId;
            //var user = _repo.FindAll<AgencyUser>().Where(a => a.Id == value).ToList().Select(a => a.GetAccountabilityAsResponsible(_repo).FirstOrDefault());
            //if (user.Count() > 0)
            //{
            //    loggedInUserOrgId = user.FirstOrDefault().CommisionerId;
            //    model = model.Where(a => a.AgencyId == loggedInUserOrgId);
            //}
            return model;
        }

        public static IQueryable<T> ByAgencyOrgId<T>(this IQueryable<T> model, string value, string tenantId) where T : AgencyUser
        {
            string loggedInUserOrgId;

            return model;
        }

        public static IQueryable<T> ByCardExpiryDate<T>(this IQueryable<T> model, DateTime? value) where T : AgencyUser
        {
            if (value != null && value != DateTime.MinValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(a => a.AuthorizationCardExpiryDate >= startDate && a.AuthorizationCardExpiryDate < endDate);
            }
            return model;
        }

        public static IQueryable<T> ByWorkflowState<T>(this IQueryable<T> model, AgencyUserWorkflowState value) where T : AgencyUser
        {
            if (value != null)
            {
                model = model.Where(c => c.AgencyUserWorkflowState.Name == value.Name);
            }
            return model.Where(c => !c.IsBlackListed);
        }

        public static IQueryable<T> ByAgencyUserType<T>(this IQueryable<T> model, string userType) where T : AgencyUser
        {
            if (userType != null)
            {
                model = model.Where(c => c.UserType == userType);
            }
            return model;
        }

        public static IQueryable<T> IncludeAgencyUserWorkflow<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.AgencyUserWorkflowState);
        }

        public static IQueryable<T> IncludeLanguages<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.Languages);
        }

        public static IQueryable<T> IncludeUserCustomerPersona<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.userCustomerPersona);
        }

        public static IQueryable<T> IncludeAgencyUserPerformanceBand<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.userPerformanceBand);
        }

        public static IQueryable<T> IncludeAgencyUserBankBranch<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.CreditAccountDetails.BankBranch);
        }

        public static IQueryable<T> IncludeAgencyUserBank<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.CreditAccountDetails.BankBranch.Bank);
        }

        public static IQueryable<T> IncludeAgencyUserDesignation<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.Designation);
        }

        public static IQueryable<T> IncludeAgencyUserDepartmentName<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude("Designation.Department");
        }

        public static IQueryable<T> IncludeAgencyUserDesignationName<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude("Designation.Designation");
        }

        public static IQueryable<T> IncludeAgencyUserAddress<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.Address);
        }

        public static IQueryable<T> IncludeAgencyUserAgencyAddress<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.Agency.Address);
        }
        //public static IQueryable<T> IncludeAgencyUserScopeOfWork<T>(this IQueryable<T> model) where T : AgencyUser
        //{
        //    return model.FlexInclude(x => x.ScopeOfWork);
        //}

        public static IQueryable<T> IncludeAgencyUserPlaceOfWork<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.PlaceOfWork);
        }

        public static IQueryable<T> IncludeAgencyUserCreditAccountDetails<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.CreditAccountDetails);
        }

        public static IQueryable<T> IncludeAgencyUserCollectionAgency<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.Agency);
        }

        public static IQueryable<T> IncludeAgencyUserIdentifications<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.AgencyUserIdentifications);
        }

        public static IQueryable<T> IncludeAgencyUserIdentificationDocs<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude("AgencyUserIdentifications.TFlexIdentificationDocs");
        }

        public static IQueryable<T> ByUserId<T>(this IQueryable<T> model, string userId) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(userId))
            {
                model = model.Where(c => c.Id == userId);
            }
            return model;
        }

        public static IQueryable<T> ByAgencyUserIds<T>(this IQueryable<T> model, List<string> values) where T : AgencyUser
        {
            return model.Where(c => values.Contains(c.Id));
        }

        public static IQueryable<T> LoggedInuserAgencyId<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            return model.Where(c => c.AgencyId == value);
        }

        public static IQueryable<T> ByCustomId<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(x => x.CustomId == value);
            }
            return model;
        }
        public static IQueryable<T> ByUserCustomIds<T>(this IQueryable<T> model, List<string> values) where T : AgencyUser
        {
            return model.Where(x => values.Contains(x.CustomId));
         
        }

        public static IQueryable<T> ByAgencyUserWorkflowState<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (value != null)
            {
                model = model.Where(c => c.AgencyUserWorkflowState.Name == value && !c.IsBlackListed);
            }
            return model;
        }

        public static IQueryable<T> ByAgencyUserName<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                var searchValue = value.Trim(); // Trim whitespace from the search value

                model = model.Where(c => c.FirstName == searchValue);                // Case-insensitive comparison
            }
            return model;
        }


        public static IQueryable<T> ByAgencyUserId<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.Id == value);
            }
            return model;
        }

        public static IQueryable<T> ByAgencyUserCustomId<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.CustomId == value);
            }
            return model;
        }

        public static IQueryable<T> IncludeAgencyUserAttendanceLog<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(a => a.UserAttendanceLog);
        }

        public static IQueryable<T> IncludeAgencyUserAttendance<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(a => a.UserAttendanceDetails);
        }

        public static IQueryable<T> ByAgencyUserAttendanceByMonthAndYear<T>(this IQueryable<T> model, int month, int year) where T : AgencyUser
        {
            DateTime startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDate = startDate.AddMonths(1); // First day of next month
            return model.Where(a => a.UserAttendanceDetails.Any(x => x.Date >= startDate && x.Date < endDate));
        }

        public static IQueryable<T> ByAgencyUserAgency<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(c => c.AgencyId == value);
            }
            return model;
        }

        public static IQueryable<T> IncludeAgencyUserAgency<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(a => a.Agency);
        }

        public static IQueryable<T> ByAgencyUserByRegion<T>(this IQueryable<T> model, string value) where T : AgencyUser
        {
            if (!string.IsNullOrEmpty(value))
            {
                var searchValue = value.Trim(); // Trim whitespace from the search value
                model = model.Where(c => c.GeoScopes != null && c.GeoScopes.Any(a => a.GeoScopeId == searchValue));
                // Case-insensitive comparison
            }
            return model;
        }


        public static IQueryable<T> BuildIncludeAgencyUserDesignation<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude("Designation.Designation");
        }

        public static IQueryable<T> BuildIncludeAgencyUserDepartment<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude("Designation.Department");
        }

        public static IQueryable<T> IncludeAgencyWallet<T>(this IQueryable<T> model) where T : AgencyUser
        {
            return model.FlexInclude(x => x.Wallet);
        }

        public static IQueryable<T> ByAgencyUserInactivity<T>(this IQueryable<T> model, int days) where T : AgencyUser
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            AgencyUserWorkflowState approvedState = host.GetFlexStateInstance<AgencyUserApproved>();

            DateTime inactivityStartDate = DateTime.Today.Date.AddDays(-days);

            return model.Where(x => x.AgencyUserWorkflowState.Name == approvedState.Name
                                    && !x.IsBlackListed
                                    && !x.IsDeactivated
                                    && x.UserAttendanceLog.Max(a => a.LogInTime) <= inactivityStartDate);
        }
        public static IQueryable<T> ByAgencyUserCustomIds<T>(this IQueryable<T> model, List<string> values) where T : AgencyUser
        {
            return model.Where(c => values.Contains(c.CustomId));
        }
    }
}