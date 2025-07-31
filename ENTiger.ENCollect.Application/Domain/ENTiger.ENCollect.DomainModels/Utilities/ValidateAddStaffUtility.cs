using ENTiger.ENCollect.CommonModule;
using ENTiger.ENCollect.CompanyUsersModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.DomainModels.Utilities
{
    public class ValidateAddStaffUtility
    {
        protected static ILogger<ValidateAddStaffUtility> _logger = FlexContainer.ServiceProvider.GetService<ILogger<ValidateAddStaffUtility>>();
        protected readonly IRepoFactory _repoFactory;

        public ValidateAddStaffUtility()
        {
            _repoFactory = FlexContainer.ServiceProvider.GetRequiredService<IRepoFactory>();
        }

        public ValidateAddStaffUtility(ILogger<ValidateAddStaffUtility> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public async Task<List<BulkUserErrorList>> ValidateAddStaffAsync(AddCompanyUserDto dto)
        {
            _repoFactory.Init(dto);
            List<BulkUserErrorList> errors = new List<BulkUserErrorList>();

            if (string.IsNullOrEmpty(dto.FirstName))
            {
                BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                bulkUserErrorList.sequence = 2;
                bulkUserErrorList.errormessage = "First Name is required";
                errors.Add(bulkUserErrorList);
            }
            if (string.IsNullOrEmpty(dto.LastName))
            {
                BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                bulkUserErrorList.sequence = 3;
                bulkUserErrorList.errormessage = "Last Name is required";
                errors.Add(bulkUserErrorList);
            }
            if (string.IsNullOrEmpty(dto.EmployeeID))
            {
                BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                bulkUserErrorList.sequence = 1;
                bulkUserErrorList.errormessage = "Employee Id is required";
                errors.Add(bulkUserErrorList);
            }
            if (string.IsNullOrEmpty(dto.PrimaryEMail))
            {
                BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                bulkUserErrorList.sequence = 4;
                bulkUserErrorList.errormessage = "Email id is required";
                errors.Add(bulkUserErrorList);
            }
            if (string.IsNullOrEmpty(dto.PrimaryMobileNumber))
            {
                BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                bulkUserErrorList.sequence = 5;
                bulkUserErrorList.errormessage = "Mobile Number is required";
                errors.Add(bulkUserErrorList);
            }

            if (!string.IsNullOrEmpty(dto.EmployeeID))
            {
                if (!Regex.IsMatch(dto.EmployeeID, @"^[a-zA-Z0-9]+$"))
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 1;
                    bulkUserErrorList.errormessage = "Please enter valid Employee Id";
                    errors.Add(bulkUserErrorList);
                }

                if (dto.EmployeeID.Length > 25)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 1;
                    bulkUserErrorList.errormessage = "Employee ID cannot be more than 25 characters";
                    errors.Add(bulkUserErrorList);
                }
            }
            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                if (!Regex.IsMatch(dto.FirstName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 2;
                    bulkUserErrorList.errormessage = "Please enter valid First Name";
                    errors.Add(bulkUserErrorList);
                }

                if (dto.FirstName.Length > 40)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 2;
                    bulkUserErrorList.errormessage = "First Name cannot be more than 40 characters";
                    errors.Add(bulkUserErrorList);
                }

                var companyuser = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(a => a.EmployeeId == dto.EmployeeID).FirstOrDefaultAsync();
                if (companyuser != null)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 1;
                    bulkUserErrorList.errormessage = "Employee Id has to be unique";
                    errors.Add(bulkUserErrorList);
                }
            }
            if (!string.IsNullOrEmpty(dto.LastName))
            {
                if (!Regex.IsMatch(dto.LastName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 3;
                    bulkUserErrorList.errormessage = "Please enter valid Last Name";
                    errors.Add(bulkUserErrorList);
                }

                if (dto.LastName.Length > 40)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 3;
                    bulkUserErrorList.errormessage = "Last Name cannot be more than 40 characters";
                    errors.Add(bulkUserErrorList);
                }
            }
            if (!string.IsNullOrEmpty(dto.PrimaryEMail))
            {
                if (!Regex.IsMatch(dto.PrimaryEMail, @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,4}$"))
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 4;
                    bulkUserErrorList.errormessage = "Enter valid email";
                    errors.Add(bulkUserErrorList);
                }
                var companyuser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.PrimaryEMail == dto.PrimaryEMail).FirstOrDefaultAsync();
                if (companyuser != null)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 4;
                    bulkUserErrorList.errormessage = "Email Id already exists";
                    errors.Add(bulkUserErrorList);
                }
            }

            if (!string.IsNullOrEmpty(dto.PrimaryMobileNumber))
            {
                if (!Regex.IsMatch(dto.PrimaryMobileNumber, @"^[0-9]+$"))
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 5;
                    bulkUserErrorList.errormessage = "Mobile Number Only numbers are allowed";
                    errors.Add(bulkUserErrorList);
                }

                if (dto.PrimaryMobileNumber.Length != 10)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 5;
                    bulkUserErrorList.errormessage = "Mobile Number 10 digits are required";
                    errors.Add(bulkUserErrorList);
                }

                var companyuser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.PrimaryMobileNumber == dto.PrimaryMobileNumber).FirstOrDefaultAsync();
                if (companyuser != null)
                {
                    BulkUserErrorList bulkUserErrorList = new BulkUserErrorList();
                    bulkUserErrorList.sequence = 5;
                    bulkUserErrorList.errormessage = "Mobile Number already exists";
                    errors.Add(bulkUserErrorList);
                }
            }

            return errors;
        }
    }
}