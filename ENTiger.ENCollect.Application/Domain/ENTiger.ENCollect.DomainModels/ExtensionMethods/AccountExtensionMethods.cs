using Sumeru.Flex;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ENTiger.ENCollect
{
    public static class AccountExtensionMethods
    {
        public static IQueryable<T> ByCustomerName<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CUSTOMERNAME.StartsWith(value));
            }
            return model;
        }

        public static IQueryable<T> ByCustomerId<T>(this IQueryable<T> model, string? value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CUSTOMERID == value);
            }
            return model;
        }

        public static IQueryable<T> ByUserType<T>(this IQueryable<T> model, ApplicationUser usertype, string agencyid) where T : LoanAccount
        {
            if (usertype?.GetType() == typeof(AgencyUser))
            {
                model = model.Where(a => (a.AgencyId == agencyid || a.TeleCallingAgencyId == agencyid));
            }
            return model;
        }


        public static IQueryable<T> ByAccountNo<T>(this IQueryable<T> model, string? value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.AGREEMENTID == value);
            }
            return model;
        }

        public static IQueryable<T> ByAccountId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Id == value);
            }
            return model;
        }

        public static IQueryable<T> ByLoanAccountId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Id == value);
            }
            return model;
        }

        public static IQueryable<T> ByAccountLatestMobileNo<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.LatestMobileNo == value);
            }
            return model;
        }

        public static IQueryable<T> ByLenderId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            return model = model.Where(a => a.LenderId == value);
        }

        public static IQueryable<T> ByBucket<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
                {
                    return model.Where(a => a.BUCKET.ToString() == value);
                }
            }
            return model;
        }


        public static IQueryable<T> ByLoggedInId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!String.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CollectorId == value);
            }
            return model;
        }

        public static IQueryable<T> byZone<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!String.IsNullOrEmpty(value) && value.ToLower() != "all")

            {
                model = model.Where(a => a.ZONE == value);
            }
            return model;
        }

        public static IQueryable<T> byDPD<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!String.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CURRENT_DPD.ToString() == value);
            }
            return model;
        }

        public static IQueryable<T> ByMobileNumber<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.MAILINGMOBILE == value || a.LatestMobileNo == value);
            }
            return model;
        }

        public static IQueryable<T> ByCustomerID<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CUSTOMERID == value);
            }
            return model;
        }

        public static IQueryable<T> ByGroupID<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.GroupId == value);
            }
            return model;
        }

        public static IQueryable<T> ByCardNumber<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.ProductCode == ProductCodeEnum.CreditCard.Value);

                if (!value.Contains("*"))
                {
                    model = model.Where(a => a.PRIMARY_CARD_NUMBER == value);
                }
                else
                {
                    var creditLength = value.Length;
                    if (creditLength == 16)
                    {
                        string str = value.Substring(0, 6);
                        string str1 = value.Substring(12, 4);

                        if (!str.Contains("*") && !str1.Contains("*"))
                        {
                            string reverseStr1 = new string(str1.Trim().Reverse().ToArray());

                            model = model.Where(a => !string.IsNullOrEmpty(a.PRIMARY_CARD_NUMBER) &&
                                        (a.PRIMARY_CARD_NUMBER.StartsWith(str)
                                        || a.ReverseOfPrimaryCard.StartsWith(reverseStr1)));
                        }
                    }
                }
            }
            return model;
        }

        public static IQueryable<T> ByLastXDigitsOfCardNumber<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                string reverseValue = new string(value.Trim().Reverse().ToArray());
                model = model.Where(a => a.ProductCode == ProductCodeEnum.CreditCard.Value && a.ReverseOfPrimaryCard.StartsWith(reverseValue));
            }
            return model;
        }

        public static IQueryable<T> ByCustomerReferenceNumber<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                return model.Where(a => a.CUSTOMERID == value);
            }
            return model;
        }

        public static IQueryable<T> byCity<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.CITY == value);
            }
            return model;
        }

        public static IQueryable<T> byBranch<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.BRANCH == value);
            }
            return model;
        }

        public static IQueryable<T> byBucket<T>(this IQueryable<T> model, string bucket) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(bucket) && !string.Equals(bucket, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.BUCKET.ToString() == bucket);
            }
            return model;
        }

        public static IQueryable<T> byBillingCycle<T>(this IQueryable<T> model, string cycle) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(cycle) && !string.Equals(cycle, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.BILLING_CYCLE == cycle);
            }
            return model;
        }

        public static IQueryable<T> byBranchCode<T>(this IQueryable<T> model, string branchCode) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(branchCode) && !string.Equals(branchCode, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.BranchCode == branchCode);
            }
            return model;
        }


        public static IQueryable<T> byPaymentStatus<T>(this IQueryable<T> model, string status) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(status) && !string.Equals(status, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.PAYMENTSTATUS == status);
            }
            return model;
        }


        public static IQueryable<T> ByDOB<T>(this IQueryable<T> model, DateTime? value) where T : LoanAccount
        {
            if (value.HasValue)
            {
                DateTime startDate = value.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                model = model.Where(a => a.DateOfBirth.HasValue && a.DateOfBirth >= startDate && a.DateOfBirth < endDate);
            }
            return model;
        }

        public static IQueryable<T> byStates<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.STATE == value);
            }
            return model;
        }


        public static IQueryable<T> byCitys<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(a => a.CITY == value);
            }
            return model;
        }


        public static IQueryable<T> byRegions<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!String.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Region == value);
            }
            return model;
        }

        public static IQueryable<T> BySecondaryAllocation<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (value != null)
            {
                model = model.Where(a => a.CollectorId == value);
            }
            return model;
        }

        public static IQueryable<T> ByLoanAccountNumber<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            bool isAccountNo = !string.IsNullOrEmpty(value?.Trim());
            if (isAccountNo)
            {
                return model.Where(a => a.AGREEMENTID == value && a.ProductGroup != ProductGroupEnum.CreditCard.Value);
            }
            else
            {
                return model;
            }
        }


        public static IQueryable<T> ByPartnerLoanId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.Partner_Loan_ID == value);
            }
            return model;
        }

        public static IQueryable<T> ByCentreID<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.CentreID == value);
            }
            return model;
        }

        public static IQueryable<T> ByCCAccount<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.Where(a => a.ProductCode == ProductCodeEnum.CreditCard.Value);
        }

        public static IQueryable<T> ByCCAccountNo<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value?.Trim())) // Direct check for null or empty
            {
                return model.Where(a => a.AGREEMENTID == value);
            }
            return model;
        }

        public static IQueryable<T> ByLoanAccount<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.Where(a => a.ProductCode != ProductCodeEnum.CreditCard.Value);
        }


        public static IQueryable<T> IsLoanAccount<T>(this IQueryable<T> model, bool value) where T : LoanAccount
        {
            if (value)
            {
                model = model.Where(a => a.ProductCode == null || a.ProductCode != ProductCodeEnum.CreditCard.Value);
            }
            else
            {
                model = model.Where(a => a.ProductCode != null && a.ProductCode == ProductCodeEnum.CreditCard.Value);
            }
            return model;
        }

        public static IQueryable<T> ByIsLoanAccount<T>(this IQueryable<T> model, bool isloanAccount) where T : LoanAccount
        {
            if (isloanAccount == true)
            {
                return model.Where(a => a.ProductCode != ProductCodeEnum.CreditCard.Value);
            }
            else
            {
                return model.Where(a => a.ProductCode == ProductCodeEnum.CreditCard.Value);
            }
        }

        public static IQueryable<T> ByLoanAccountNo<T>(this IQueryable<T> model, string value, bool isloanAccount) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => a.AGREEMENTID == value);
            }
            return model;
        }

        public static IQueryable<T> ByLastXDigitsOfAccountNo<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                string reverseValue = new string(value.Trim().Reverse().ToArray());
                model = model.Where(a => a.ReverseOfAgreementId.StartsWith(reverseValue));
            }
            return model;
        }

        public static IQueryable<T> ByMyAccountAllocations<T>(this IQueryable<T> model, string userType, string loggedinUserId) where T : LoanAccount
        {
            if (loggedinUserId == null) return model;

            if (userType.GetType() == typeof(AgencyUser))
            {
                return model.Where(a => a.TeleCallerId == loggedinUserId);
            }

            return model.Where(a => a.CollectorId == loggedinUserId);
        }

        public static IQueryable<T> ByCollectorId<T>(this IQueryable<T> Accounts, string CollectorId) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(CollectorId))
            {
                return Accounts.Where(a => a.CollectorId == CollectorId);
            }
            return Accounts;
        }

        public static IQueryable<T> WithMonthAndYear<T>(this IQueryable<T> Accounts, Int64 Month, Int64 Year) where T : LoanAccount
        {
            Accounts = Accounts.Where(p => (p.MONTH == Month && p.YEAR == Year));

            return Accounts;
        }

        public static IQueryable<T> ByLoanaccountProductGroup<T>(this IQueryable<T> accounts, string productGroup) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(productGroup) &&
                !string.Equals(productGroup.Trim(), ProductGroupEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.ProductGroup.Trim() == productGroup.Trim());
            }
            return accounts;
        }


        public static IQueryable<T> ByLoanaccountProduct<T>(this IQueryable<T> accounts, string product) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(product) &&
                !string.Equals(product.Trim(), ProductCodeEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.PRODUCT.Trim() == product.Trim());
            }
            return accounts;
        }


        public static IQueryable<T> ByLoanaccountSubProduct<T>(this IQueryable<T> accounts, string subProduct) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(subProduct) &&
                !string.Equals(subProduct.Trim(), SubProductEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.SubProduct.Trim() == subProduct.Trim());
            }
            return accounts;
        }


        public static IQueryable<T> ByLoanAccountBucket<T>(this IQueryable<T> Accounts, IList<string> Bucket) where T : LoanAccount
        {
            //bool isBucket = Bucket != null && !(Bucket.Trim().Equals("")) ? true : false;
            bool isBucket = Bucket != null ? true : false;
            if (isBucket)
            {
                return Accounts.Where(a => Bucket.Contains(a.BUCKET.ToString()));
            }
            else
            {
                return Accounts;
            }
        }

        public static IQueryable<T> ByLoanAccountBucket<T>(this IQueryable<T> accounts, string bucket) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(bucket) && !string.Equals(bucket, "all", StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.BUCKET.ToString() == bucket);
            }
            return accounts;
        }


        public static IQueryable<T> WithNpaStageid<T>(this IQueryable<T> accounts, bool? stageid) where T : LoanAccount
        {
            if (stageid.HasValue)
            {
                var stageValue = stageid.Value ? "Y" : "N";
                return accounts.Where(p => p.NPA_STAGEID == stageValue);
            }
            return accounts;
        }


        public static IQueryable<T> ByCreditCardNumber<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                return model.Where(a => a.PRIMARY_CARD_NUMBER == value);
            }
            return model;
        }

        public static IQueryable<T> ByTodaysPTP<T>(this IQueryable<T> account) where T : LoanAccount
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);
            return account.Where(a => a.LatestPTPDate >= startDate && a.LatestPTPDate < endDate);
        }
        public static IQueryable<T> ByPTPDate<T>(this IQueryable<T> account, DateTime? PTPDate) where T : LoanAccount
        {
            if (PTPDate.HasValue && PTPDate != DateTime.MinValue)
            {
                DateTime startDate = PTPDate.Value;
                DateTime endDate = startDate.AddDays(1);
                account=account.Where(a => a.LatestPTPDate >= startDate && a.LatestPTPDate < endDate);
            }
            return account;
        }
        public static IQueryable<T> ByPastDueDate<T>(this IQueryable<T> account, int daysOffSet) where T : LoanAccount
        {
            var dueStartDate = DateTime.UtcNow.Date.AddDays(-daysOffSet).ToString("yyyy-MM-dd"); // today date 25 and day of set 7 then due date will 18
            var dueEndDate = DateTime.UtcNow.Date.AddDays((-daysOffSet + 1)).ToString("yyyy-MM-dd");  //  end date will 19

            return account.Where(a =>
                string.Compare(a.DueDate, dueStartDate) >= 0 &&
                string.Compare(a.DueDate, dueEndDate) < 0);
        }
        public static IQueryable<T> ByBeforeDueDate<T>(this IQueryable<T> account, int daysOffSet) where T : LoanAccount
        {
            var dueStartDate = DateTime.UtcNow.Date.AddDays(daysOffSet).ToString("yyyy-MM-dd"); // today date 18 and day of set 7 then current date will 25
            var dueEndDate = DateTime.UtcNow.Date.AddDays(daysOffSet + 1).ToString("yyyy-MM-dd");  // end date will 26

            return account.Where(a =>
                string.Compare(a.DueDate, dueStartDate) >= 0 &&
                string.Compare(a.DueDate, dueEndDate) < 0);
           
        }
        public static IQueryable<T> ByAfterStatementDate<T>(this IQueryable<T> account, int daysOffSet) where T : LoanAccount
        {
            DateTime todayDate = DateTime.UtcNow.Date;
            DateTime startDate = todayDate.AddDays(-daysOffSet); // today date 18 and day of set 7 then due start date will 11
            DateTime endDate = startDate.AddDays(1);  //  end date will 12

            return account.Where(a => a.LAST_STATEMENT_DATE >= startDate && a.LatestPTPDate < endDate);
        }
        public static IQueryable<T> ByTodaysPTPAndNoPayment<T>(this IQueryable<T> model) where T : LoanAccount
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);

            model = model.Where(a => a.LatestPTPDate >= startDate.ToDateOnlyOffset() && a.LatestPTPDate < endDate.ToDateOnlyOffset() &&
                            (a.LatestPaymentDate == null || a.LatestPaymentDate < startDate.ToDateOnlyOffset()));

            return model;
        }
        public static IQueryable<T> ByPrevDayPTPAndNoPayment<T>(this IQueryable<T> model) where T : LoanAccount
        {
            DateTime startDate = DateTime.Now.Date.AddDays(-1);
            DateTime endDate = startDate.AddDays(1);

            model = model.Where(a => a.LatestPTPDate >= startDate.ToDateOnlyOffset() && a.LatestPTPDate < endDate.ToDateOnlyOffset() &&
                            (a.LatestPaymentDate == null || a.LatestPaymentDate < startDate.ToDateOnlyOffset()));

            return model;
        }
        public static IQueryable<T> ByPTP<T>(this IQueryable<T> Accounts, DateTime? FromDate, DateTime? ToDate) where T : LoanAccount
        {
            if (FromDate.HasValue && FromDate != DateTime.MinValue)
            {
                DateTime startDate = FromDate.Value.Date;
                Accounts = Accounts.Where(c => c.LatestPTPDate >= startDate);
            }
            if (ToDate.HasValue && ToDate != DateTime.MinValue)
            {
                DateTime startDate = ToDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                Accounts = Accounts.Where(c => c.LatestPTPDate < endDate);
            }
            return Accounts;
        }
        public static IQueryable<T> ByCodePTP<T>(this IQueryable<T> account) where T : LoanAccount
        {
            return account.Where(a => !string.IsNullOrEmpty(a.DispCode) && a.DispCode == "ptp");
        }
        public static IQueryable<T> byproduct<T>(this IQueryable<T> accounts, string product) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(product) &&
                !string.Equals(product.Trim(), ProductCodeEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.PRODUCT != null &&
                                    a.PRODUCT == product);
            }
            return accounts;
        }
        public static IQueryable<T> byProductGroup<T>(this IQueryable<T> accounts, string productGroup) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(productGroup) &&
                !string.Equals(productGroup, ProductGroupEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.ProductGroup != null && a.ProductGroup == productGroup);
            }
            return accounts;
        }


        public static IQueryable<T> bysubProduct<T>(this IQueryable<T> accounts, string subProduct) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(subProduct) &&
                !string.Equals(subProduct.Trim(), SubProductEnum.All.Value, StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.SubProduct != null && a.SubProduct == subProduct);

            }
            return accounts;
        }


        public static IQueryable<T> bydownloadBucket<T>(this IQueryable<T> accounts, string bucket) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(bucket) && !string.Equals(bucket, "all", StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.BUCKET != null && a.BUCKET.ToString() == bucket);

            }
            return accounts;
        }


        public static IQueryable<T> byRegion<T>(this IQueryable<T> accounts, string region) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(region) && !string.Equals(region, "all", StringComparison.OrdinalIgnoreCase))
            {
                return accounts.Where(a => a.Region != null &&
                            a.Region == region);

            }
            return accounts;
        }


        public static IQueryable<T> byState<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                return model.Where(a => a.STATE != null &&
                        a.STATE == value);

            }
            return model;
        }


        public static IQueryable<T> bycity<T>(this IQueryable<T> Accounts, string city) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(city) && !string.Equals(city, "all", StringComparison.OrdinalIgnoreCase))
            {
                return Accounts.Where(a => a.CITY != null && a.CITY == city);

            }
            return Accounts;
        }
        public static IQueryable<T> ByScope<T>(this IQueryable<T> accountsQuery,
            EffectiveScope effectiveScope, string userId) where T : LoanAccount
        {
            string scope = effectiveScope.Filter.scope;

            if (string.IsNullOrEmpty(scope))
            {
                // If no role scope is found, return all accounts (no restrictions)
                return accountsQuery;
            }

            accountsQuery = scope switch
            {
                var u when u == AccountAccessScopeEnum.All.DisplayName => accountsQuery, // No filtering, return all accounts
                var u when u == AccountAccessScopeEnum.Parent.DisplayName => accountsQuery.ByParentAgency(effectiveScope.ParentId),
                var u when u == AccountAccessScopeEnum.Self.DisplayName => accountsQuery.BySelfAllocated(userId),
                _ => accountsQuery // Default: No restriction (shouldn't happen)
            };
            return accountsQuery;
        }
        public static IQueryable<T> ByRoleSearchScope<T>(this IQueryable<T> accounts, string scope, string userId, string parentId) where T : LoanAccount
        {
            if (string.IsNullOrEmpty(scope))
            {
                // If no role scope is found, return all accounts (no restrictions)
                return accounts;
            }

            accounts = scope switch
            {
                var u when u == AccountAccessScopeEnum.All.DisplayName => accounts, // No filtering, return all accounts
                var u when u == AccountAccessScopeEnum.Parent.DisplayName => accounts.ByParentAgency(parentId),
                var u when u == AccountAccessScopeEnum.Self.DisplayName => accounts.BySelfAllocated(userId),
                _ => accounts // Default: No restriction (shouldn't happen)
            };
            return accounts;
        }
        public static IQueryable<T> ByParentAgency<T>(this IQueryable<T> query, string? parentId) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(parentId))
            {
                query = query.Where(a => (a.AgencyId == parentId || a.TeleCallingAgencyId == parentId));
            }
            return query;
        }

        public static IQueryable<T> BySelfAllocated<T>(this IQueryable<T> query, string userId) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(a => (a.CollectorId == userId || a.TeleCallerId == userId));
            }
            return query;
        }

        public static IQueryable<T> ByPrimaryAllocation<T>(this IQueryable<T> accounts, bool isAllocated, bool isUnallocated, ApplicationUser loggedInUserParty, Accountability responsibleUser, string commissionerId, string loggedinUserId) where T : LoanAccount
        {
            if (isAllocated == isUnallocated)
            {
                if (loggedInUserParty is AgencyUser)
                {
                    accounts = accounts.Where(b => b.AgencyId == commissionerId);
                }
                else if (loggedInUserParty is CompanyUser && !string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
                {
                    accounts = accounts.Where(a => a.AllocationOwnerId == loggedinUserId);
                }
            }
            else if (isAllocated)
            {
                if (loggedInUserParty is AgencyUser)
                {
                    accounts = accounts.Where(b => b.AgencyId == commissionerId);
                }
                else if (loggedInUserParty is CompanyUser && string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
                {
                    accounts = accounts.Where(a => !string.IsNullOrEmpty(a.TeleCallingAgencyId) || !string.IsNullOrEmpty(a.AgencyId));
                }
                else if (loggedInUserParty is CompanyUser)
                {
                    accounts = accounts.Where(a => a.AllocationOwnerId == loggedinUserId
                                    && (!string.IsNullOrEmpty(a.TeleCallingAgencyId) || !string.IsNullOrEmpty(a.AgencyId)));
                }
            }
            else if (isUnallocated)
            {
                if (loggedInUserParty is AgencyUser)
                {
                    accounts = accounts.Where(b => 1 == 2); // Always false
                }
                else if (loggedInUserParty is CompanyUser && string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
                {
                    accounts = accounts.Where(a => string.IsNullOrEmpty(a.TeleCallingAgencyId) || string.IsNullOrEmpty(a.AgencyId));
                }
                else if (loggedInUserParty is CompanyUser)
                {
                    accounts = accounts.Where(a => a.AllocationOwnerId == loggedinUserId
                                                && (string.IsNullOrEmpty(a.TeleCallingAgencyId) || string.IsNullOrEmpty(a.AgencyId)));
                }
            }

            return accounts;
        }


        public static IQueryable<T> BySecondAllocation<T>(this IQueryable<T> accounts, bool isAllocated, bool isUnallocated, ApplicationUser loggedInUserParty, Accountability responsibleUser, string commissionerId, string loggedinUserId) where T : LoanAccount
        {
            if (isAllocated == isUnallocated)
            {
                if (loggedInUserParty is AgencyUser)
                {
                    accounts = accounts.Where(b => b.AgencyId == commissionerId || b.TeleCallingAgencyId == commissionerId);
                }
                else if (loggedInUserParty is CompanyUser && string.Equals(responsibleUser.AccountabilityTypeId, AccountabilityTypeEnum.BankToBackEndInternalBIHP.Value, StringComparison.OrdinalIgnoreCase))
                {
                    accounts = accounts.Where(a => !string.IsNullOrEmpty(a.AgencyId) || !string.IsNullOrEmpty(a.TeleCallingAgencyId));
                }
                else if (loggedInUserParty is CompanyUser)
                {
                    accounts = accounts.Where(a => a.AllocationOwnerId == loggedinUserId
                                                    && (!string.IsNullOrEmpty(a.AgencyId) || !string.IsNullOrEmpty(a.TeleCallingAgencyId)));
                }
            }

            else if (isAllocated)
            {
                if (loggedInUserParty.GetType() == typeof(AgencyUser))
                {
                    accounts = accounts.Where(b => (b.AgencyId == commissionerId || b.TeleCallingAgencyId == commissionerId)
                                                   && (!string.IsNullOrEmpty(b.CollectorId) || !string.IsNullOrEmpty(b.TeleCallerId)));
                }
                else if (loggedInUserParty.GetType() == typeof(CompanyUser) && responsibleUser.AccountabilityTypeId.Contains("BankToBackEndInternalBIHP"))
                {
                    accounts = accounts.Where(a => !string.IsNullOrEmpty(a.CollectorId) || !string.IsNullOrEmpty(a.TeleCallerId));
                }
                else if (loggedInUserParty.GetType() == typeof(CompanyUser))
                {
                    accounts = accounts.Where(a => a.AllocationOwnerId == loggedinUserId
                                                    && (!string.IsNullOrEmpty(a.CollectorId) || !string.IsNullOrEmpty(a.TeleCallerId)));
                }
            }
            else if (isUnallocated)
            {
                if (loggedInUserParty.GetType() == typeof(AgencyUser))
                {
                    accounts = accounts.Where(b => (b.AgencyId == commissionerId || b.TeleCallingAgencyId == commissionerId)
                                                   && (string.IsNullOrEmpty(b.CollectorId) && string.IsNullOrEmpty(b.TeleCallerId)));
                }
                else if (loggedInUserParty.GetType() == typeof(CompanyUser) && responsibleUser.AccountabilityTypeId.Contains("BankToBackEndInternalBIHP"))
                {
                    accounts = accounts.Where(a => string.IsNullOrEmpty(a.CollectorId) && string.IsNullOrEmpty(a.TeleCallerId));
                }
                else if (loggedInUserParty.GetType() == typeof(CompanyUser))
                {
                    accounts = accounts.Where(a => a.AllocationOwnerId == loggedinUserId
                                                    && (string.IsNullOrEmpty(a.CollectorId) && string.IsNullOrEmpty(a.TeleCallerId)));
                }
            }
            return accounts;
        }


        public static IQueryable<T> byAccountUploadedDate<T>(this IQueryable<T> Accounts, DateTime? FromDate, DateTime? ToDate) where T : LoanAccount
        {
            if (FromDate.HasValue && FromDate != DateTime.MinValue)
            {
                DateTime startDate = FromDate.Value.Date;
                Accounts = Accounts.Where(c => c.LastUploadedDate >= startDate);
            }
            if (ToDate != null && ToDate != DateTime.MinValue)
            {
                DateTime startDate = ToDate.Value.Date;
                DateTime endDate = startDate.AddDays(1);
                Accounts = Accounts.Where(c => c.LastUploadedDate < endDate);
            }
            return Accounts;
        }

        public static IQueryable<T> ByAccountAgencyId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(c => c.AgencyId == value);
            }

            return model;
        }



        public static IQueryable<T> ByAccountTeleCallingAgencyId<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value) && !string.Equals(value, "all", StringComparison.OrdinalIgnoreCase))
            {
                model = model.Where(c => c.TeleCallingAgencyId == value);
            }

            return model;
        }


        public static IQueryable<T> ByTreatmentFetchLoanAccountsByPincodes<T>(this IQueryable<T> Accounts, bool IsPincodeCondition, List<string> Pincodes) where T : LoanAccount
        {
            if (IsPincodeCondition == true)
            {
                if (Pincodes.Count() > 0)
                {
                    return Accounts.Where(a => Pincodes.Contains(a.MAILINGZIPCODE));
                }
                else
                {
                    return Accounts.Take(0);
                }
            }
            return Accounts;
        }

        public static IQueryable<T> ByTreatmentFetchLoanAccountsByBranch<T>(this IQueryable<T> Accounts, bool IsBranchCondition, List<string> Branches) where T : LoanAccount
        {
            if (IsBranchCondition == true)
            {
                if (Branches.Count() > 0)
                {
                    return Accounts.Where(a => Branches.Contains(a.BRANCH));
                }
                else
                {
                    return Accounts.Take(0);
                }
            }
            return Accounts;
        }

        public static IQueryable<T> ByTreatmentFetchLoanAccountsBySubProduct<T>(this IQueryable<T> Accounts, bool IsSubProductCondition, List<string> subproducts) where T : LoanAccount
        {
            if (IsSubProductCondition == true)
            {
                if (subproducts.Count() > 0)
                {
                    return Accounts.Where(a => subproducts.Contains(a.SubProduct));
                }
                else
                {
                    return Accounts.Take(0);
                }
            }
            return Accounts;
        }

        public static IQueryable<T> ByTreatmentFetchLoanAccountsByCustomerPersona<T>(this IQueryable<T> Accounts, bool IsCustomerPersonaCondition, List<string> customerpersona) where T : LoanAccount
        {
            if (IsCustomerPersonaCondition == true)
            {
                if (customerpersona.Count() > 0)
                {
                    return Accounts.Where(a => customerpersona.Contains(a.CustomerPersona));
                }
                else
                {
                    return Accounts.Take(0);
                }
            }
            return Accounts;
        }

        public static IQueryable<T> IncludeAccountJson<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.AccountJSON);
        }

        public static IQueryable<T> ByLoanAccountStatus<T>(this IQueryable<T> model, string value) where T : LoanAccount
        {
            if (!string.IsNullOrEmpty(value))
            {
                model = model.Where(a => (a.LOAN_STATUS ?? LoanAccountStatusEnum.Live.Value) == value); //column has null values : should this be regarded as Live?
            }
            return model;
        }

        public static IQueryable<T> ByAccountLastModifieddate<T>(this IQueryable<T> Accounts, DateTime? StartDate) where T : LoanAccount
        {
            if (StartDate != null)
            {
                Accounts = Accounts.Where(c => c.LastModifiedDate >= StartDate && c.LastModifiedDate <= DateTime.Now);
            }
            return Accounts;
        }

        public static IQueryable<T> GetLinkedAccountsByCustomerId<T>(this IQueryable<T> Accounts, string? CustomerId, string? AccountId) where T : LoanAccount
        {
            if (CustomerId != null && AccountId != null)
            {
                Accounts = Accounts.Where(a => a.CUSTOMERID == CustomerId && a.Id != AccountId);
            }
            return Accounts;
        }

        public static IQueryable<T> ByDPDRange<T>(this IQueryable<T> model, long? fromDPD, long? toDPD) where T : LoanAccount
        {
            if (fromDPD.HasValue)
                model = model.Where(a => a.CURRENT_DPD >= fromDPD.Value);

            if (toDPD.HasValue)
                model = model.Where(a => a.CURRENT_DPD <= toDPD.Value);

            return model;
        }

        public static IQueryable<T> ByTotalOutStanding<T>(this IQueryable<T> model, decimal? fromTotalOutStanding, decimal? toTotalOutStanding) where T : LoanAccount
        {
            if (fromTotalOutStanding.HasValue)
                model = model.Where(a => !string.IsNullOrEmpty(a.TOS) && Convert.ToDecimal(a.TOS) >= fromTotalOutStanding.Value);

            if (toTotalOutStanding.HasValue)
                model = model.Where(a => !string.IsNullOrEmpty(a.TOS) && Convert.ToDecimal(a.TOS) <= toTotalOutStanding.Value);

            return model;
        }

        public static IQueryable<T> ByTotalOverDue<T>(this IQueryable<T> model, decimal? fromTotalOutStanding, decimal? toTotalOutStanding) where T : LoanAccount
        {
            if (fromTotalOutStanding.HasValue)
                model = model.Where(a => a.CURRENT_TOTAL_AMOUNT_DUE >= fromTotalOutStanding.Value);

            if (toTotalOutStanding.HasValue)
                model = model.Where(a => a.CURRENT_TOTAL_AMOUNT_DUE <= toTotalOutStanding.Value);

            return model;
        }

        public static IQueryable<T> IncludeAgency<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.Agency);
        }

        public static IQueryable<T> IncludeTeleCallingAgency<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.TeleCallingAgency);
        }

        public static IQueryable<T> IncludeTeleCaller<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.TeleCaller);
        }

        public static IQueryable<T> IncludeAllocationOwner<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.AllocationOwner);
        }

        public static IQueryable<T> IncludeCollector<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.Collector);
        }

        public static IQueryable<T> ByEligibleForSettlement<T>(this IQueryable<T> model, bool? isEligibleForSettlement) where T : LoanAccount
        {
            if (isEligibleForSettlement.HasValue)
                model = model.Where(a => a.IsEligibleForSettlement == isEligibleForSettlement);

            return model;
        }

        public static IQueryable<T> ByUserBranch<T>(this IQueryable<T> model, List<string?>? userBranches) where T : LoanAccount
        {
            if (userBranches != null && userBranches.Count > 0 && !userBranches.Contains("All"))
            {
                model = model.Where(a => userBranches.Contains(a.BRANCH));
            }
            return model;
        }

        public static IQueryable<T> ByAccountIds<T>(this IQueryable<T> model, List<string> loanAccountIds) where T : LoanAccount
        {
            if (loanAccountIds != null && loanAccountIds.Count > 0)
            {
                model = model.Where(a => loanAccountIds.Contains(a.Id));
            }
            return model;
        }

        public static IQueryable<T> IncludeSettlements<T>(this IQueryable<T> model) where T : LoanAccount
        {
            return model.FlexInclude(x => x.Settlements);
        }
    }
}