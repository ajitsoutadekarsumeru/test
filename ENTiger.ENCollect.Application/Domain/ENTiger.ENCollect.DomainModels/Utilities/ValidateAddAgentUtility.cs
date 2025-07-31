using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.CommonModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.DomainModels.Utilities
{
    public class ValidateAddAgentUtility
    {
        protected static ILogger<ValidateAddAgentUtility> _logger = FlexContainer.ServiceProvider.GetService<ILogger<ValidateAddAgentUtility>>();
        protected readonly IRepoFactory _repoFactory;

        public ValidateAddAgentUtility()
        {
            _repoFactory = FlexContainer.ServiceProvider.GetRequiredService<IRepoFactory>();
        }

        public ValidateAddAgentUtility(ILogger<ValidateAddAgentUtility> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public async Task<List<BulkUserErrorList>> ValidateAddAgentAsync(AddAgentDto dto)
        {
            _repoFactory.Init(dto);
            List<BulkUserErrorList> errors = new List<BulkUserErrorList>();

            if (string.IsNullOrEmpty(dto.FirstName))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 2;
                error.errormessage = "First Name is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.LastName))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 3;
                error.errormessage = "Last Name is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.PrimaryMobileNumber))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 13;
                error.errormessage = "MobileNo is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.PrimaryEMail))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 14;
                error.errormessage = "Email id is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.FatherName))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 15;
                error.errormessage = "Father Name is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.Address?.AddressLine))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 16;
                error.errormessage = "Address is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.Address?.PIN))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 17;
                error.errormessage = "Postal Code is required";
                errors.Add(error);
            }
            else if (!string.IsNullOrEmpty(dto.Address?.PIN))
            {
                string pincode = dto.Address?.PIN;

                if (pincode != null && (pincode.Length != 6 || !Regex.IsMatch(pincode, @"^[0-9]+$")))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 17;
                    error.errormessage = "Enter valid 6 digit pincode";
                    errors.Add(error);
                }                
            }

            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                if (!Regex.IsMatch(dto.FirstName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 2;
                    error.errormessage = "First Name Please enter characters only";
                    errors.Add(error);
                }
                else if (dto.FirstName.Length < 2)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 2;
                    error.errormessage = "First Name cannot be more than 40 characters";
                    errors.Add(error);

                }
                else if (dto.FirstName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 2;
                    error.errormessage = "First Name cannot be more than 40 characters";
                    errors.Add(error);
                }

                var agencyuser = await _repoFactory.GetRepo().FindAll<AgencyUser>().Where(a => a.FirstName == dto.FirstName).FirstOrDefaultAsync();
                if (agencyuser != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 2;
                    error.errormessage = "Agent already exist in Agency";
                    errors.Add(error);
                }
            }
            if (!string.IsNullOrEmpty(dto.LastName))
            {
                if (!Regex.IsMatch(dto.LastName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 3;
                    error.errormessage = "Last Name Please enter characters only";
                    errors.Add(error);
                }

                if (dto.LastName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 3;
                    error.errormessage = "Last Name cannot be more than 40 characters";
                    errors.Add(error);
                }
            }
            if (!string.IsNullOrEmpty(dto.AgencyId))
            {
                var agency = await _repoFactory.GetRepo().FindAll<Agency>()
                                        .IncludeAgencyWorkflow()
                                        .Where(a => a.Id == dto.AgencyId &&
                                                a.AgencyWorkflowState != null &&
                                                a.AgencyWorkflowState.Name == "AgencyApproved")
                                        .FirstOrDefaultAsync();
                if (agency == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 4;
                    error.errormessage = "Agency Code does not exist or not approved";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.PrimaryMobileNumber))
            {
                if (!Regex.IsMatch(dto.PrimaryMobileNumber, @"^[0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 13;
                    error.errormessage = "Mobile Number Only numbers are allowed";
                    errors.Add(error);
                }

                if (dto.PrimaryMobileNumber.Length != 10)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 13;
                    error.errormessage = "Mobile Number 10 digits are required";
                    errors.Add(error);
                }

                var agencyuser = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                            .Where(a => a.PrimaryMobileNumber == dto.PrimaryMobileNumber)
                                            .FirstOrDefaultAsync();
                if (agencyuser != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 13;
                    error.errormessage = "Mobile Number already exists";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.FatherName))
            {
                if (!Regex.IsMatch(dto.FatherName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 15;
                    error.errormessage = "Father Name Please enter characters only";
                    errors.Add(error);
                }

                if (dto.FatherName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 15;
                    error.errormessage = "Father Name cannot be more than 40 characters";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.PrimaryEMail))
            {
                if (!Regex.IsMatch(dto.PrimaryEMail, @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,4}$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Enter valid email";
                    errors.Add(error);
                }
                var agencyuser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.PrimaryEMail == dto.PrimaryEMail).FirstOrDefaultAsync();
                if (agencyuser != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Email Id already exists";
                    errors.Add(error);
                }
            }

            if (dto.Address != null)
            {
                if (dto.Address?.AddressLine.Length > 200)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 16;
                    error.errormessage = "Local Residential Address cannot be more than 200 characters";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.SupervisorEmailId))
            {
                if (!Regex.IsMatch(dto.PrimaryEMail, @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,4}$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 5;
                    error.errormessage = "Supervisor email id Please Enter valid email id";
                    errors.Add(error);
                }

                if (dto.SupervisorEmailId?.Length > 50)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 5;
                    error.errormessage = "Supervisor email id cannot be more than 50 characters";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.DiallerId))
            {
                if (dto.DiallerId?.Length > 10)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 6;
                    error.errormessage = "Dialer id cannot be more than 10 characters";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.DRAUniqueRegistrationNumber))
            {
                if (!Regex.IsMatch(dto.DRAUniqueRegistrationNumber, @"^[0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "Please enter Valid DRA Unique Registration Number.";
                    errors.Add(error);
                }
                if (dto.DRAUniqueRegistrationNumber.Length < 9)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "DRA Unique Registration Number must be atleast 9 digits long";
                    errors.Add(error);
                }
                if (dto.DRAUniqueRegistrationNumber.Length > 16)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "DRA Unique Registration Number cannot be more than 16 digits.";
                    errors.Add(error);
                }

                if (!string.IsNullOrEmpty(dto.Remarks))
                {
                    if (!Regex.IsMatch(dto.Remarks, @"^[\sa-zA-Z0-9]+$"))
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 21;
                        error.errormessage = "Remarks should not contain special character.";
                        errors.Add(error);
                    }
                }
            }

            return errors;
        }
    }
}