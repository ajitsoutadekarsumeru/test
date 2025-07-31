using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.CommonModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.DomainModels.Utilities
{
    public class ValidateAddAgencyUtility
    {
        protected static ILogger<ValidateAddAgencyUtility> _logger = FlexContainer.ServiceProvider.GetService<ILogger<ValidateAddAgencyUtility>>();
        protected readonly IRepoFactory _repoFactory;

        public ValidateAddAgencyUtility()
        {
            _repoFactory = FlexContainer.ServiceProvider.GetRequiredService<IRepoFactory>();
        }

        public ValidateAddAgencyUtility(ILogger<ValidateAddAgencyUtility> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public async Task<List<BulkUserErrorList>> ValidateAddAgencyAsync(AddAgencyDto dto)
        {
            _repoFactory.Init(dto);
            List<BulkUserErrorList> errors = new List<BulkUserErrorList>();

            if (string.IsNullOrEmpty(dto.FirstName))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 4;
                error.errormessage = "Agency Name is required";
                errors.Add(error);
            }

            if (string.IsNullOrEmpty(dto.Address?.AddressLine))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 12;
                error.errormessage = "Address is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.TIN))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 8;
                error.errormessage = "TIN number is Required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.PrimaryOwnerFirstName))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 10;
                error.errormessage = "Primary Owner First Name  is Required";
                errors.Add(error);
            }
            if (!string.IsNullOrEmpty(dto.PrimaryOwnerFirstName))
            {
                if (!Regex.IsMatch(dto.PrimaryOwnerFirstName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 10;
                    error.errormessage = "Primary Owner First Name should not contain special characters and numbers";
                    errors.Add(error);
                }
                if (dto.PrimaryOwnerFirstName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 10;
                    error.errormessage = "Primary Owner First Name cannot be more than 40 characters";
                    errors.Add(error);
                }
            }
            if (string.IsNullOrEmpty(dto.PrimaryOwnerLastName))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 11;
                error.errormessage = "Primary Owner Last Name  is Required";
                errors.Add(error);
            }
            if (!string.IsNullOrEmpty(dto.PrimaryOwnerLastName))
            {
                if (!Regex.IsMatch(dto.PrimaryOwnerLastName, @"^[\sa-zA-Z]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 11;
                    error.errormessage = "Primary Owner Last Name should not contain special characters and numbers";
                    errors.Add(error);
                }
                if (dto.PrimaryOwnerLastName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 11;
                    error.errormessage = "Primary Owner Last Name cannot be more than 40 characters";
                    errors.Add(error);
                }
            }
            if (dto.Address != null && string.IsNullOrEmpty(dto.Address?.AddressLine))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 12;
                error.errormessage = "Registered Agency Address  is Required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.PrimaryMobileNumber))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 14;
                error.errormessage = "Mobile Number is required";
                errors.Add(error);
            }
            if (string.IsNullOrEmpty(dto.PrimaryEMail))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 15;
                error.errormessage = "Email id is required";
                errors.Add(error);
            }

            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                if (!Regex.IsMatch(dto.FirstName, @"^[\sa-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 4;
                    error.errormessage = "Agency Name Should not contain special character";
                    errors.Add(error);
                }

                if (dto.FirstName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 4;
                    error.errormessage = "Agency Name cannot be more than 40 characters";
                    errors.Add(error);
                }

                var agency = await _repoFactory.GetRepo().FindAll<Agency>().FirstOrDefaultAsync(a => a.FirstName == dto.FirstName);

                if (agency != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 4;
                    error.errormessage = "Agency name should be unique";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.TIN))
            {
                if (!Regex.IsMatch(dto.TIN, @"^[a-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 8;
                    error.errormessage = "TIN Number Should not contain special character";
                    errors.Add(error);
                }

                if (dto.TIN.Length > 20)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 8;
                    error.errormessage = "TIN Number can not  be more than 20 characters";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.PrimaryOwnerFirstName))
            {
                if (!Regex.IsMatch(dto.PrimaryOwnerFirstName, @"^[\sa-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 10;
                    error.errormessage = "Primary Owner First Name not contain special character";
                    errors.Add(error);
                }

                if (dto.PrimaryOwnerFirstName.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 10;
                    error.errormessage = "Primary Owner First Name can not  be more than 40 characters";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.Address?.AddressLine))
            {
                if (dto.Address?.AddressLine.Length > 250)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 12;
                    error.errormessage = "Registered Agency Address can not be more than 250 characters.";
                    errors.Add(error);
                }
            }
            if (!string.IsNullOrEmpty(dto.PrimaryMobileNumber))
            {
                if (!Regex.IsMatch(dto.PrimaryMobileNumber, @"^[0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Mobile Number Only numbers are allowed";
                    errors.Add(error);
                }

                if (dto.PrimaryMobileNumber.Length != 10)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Mobile Number 10 digits are required";
                    errors.Add(error);
                }

                var agencyuser = await _repoFactory.GetRepo().FindAll<ApplicationOrg>()
                                            .Where(a => a.PrimaryMobileNumber == dto.PrimaryMobileNumber)
                                            .FirstOrDefaultAsync();
                if (agencyuser != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Mobile Number already exists";
                    errors.Add(error);
                }
            }
            if (!string.IsNullOrEmpty(dto.PrimaryEMail))
            {
                if (!Regex.IsMatch(dto.PrimaryEMail, @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,4}$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 15;
                    error.errormessage = "Enter valid email";
                    errors.Add(error);
                }
                var agency = await _repoFactory.GetRepo().FindAll<ApplicationOrg>()
                                        .Where(a => a.PrimaryEMail == dto.PrimaryEMail)
                                        .FirstOrDefaultAsync();
                if (agency != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 15;
                    error.errormessage = "Email Id already exists";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.Address?.LandMark))
            {
                if (!Regex.IsMatch(dto.Address?.LandMark, @"^[\sa-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 13;
                    error.errormessage = "LandMark Should not contain special character.";
                    errors.Add(error);
                }
            }

            if (!string.IsNullOrEmpty(dto.Remarks))
            {
                if (!Regex.IsMatch(dto.Remarks, @"^[\sa-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 19;
                    error.errormessage = "Remarks should not contain special character.";
                    errors.Add(error);
                }
            }

            return errors;
        }
    }
}