using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ENTiger.ENCollect.AgencyUsersModule.AddAgentAgencyUsersPlugins
{
    public partial class ValidateAddAgent : FlexiBusinessRuleBase, IFlexiBusinessRule<AddAgentDataPacket>
    {
        public override string Id { get; set; } = "ValidateAddAgent";
        public override string FriendlyName { get; set; } = "ValidateAddAgent";

        protected readonly ILogger<CheckDuplicateAgent> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public ValidateAddAgent(ILogger<CheckDuplicateAgent> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddAgentDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            if (!string.IsNullOrEmpty(packet.Dto.FirstName))
            {
                if (!Regex.IsMatch(packet.Dto.FirstName, @"^[a-zA-Z]+$"))
                {
                    packet.AddError("Error", "First Name Please enter characters only");
                }

                if (packet.Dto.FirstName.Length > 40)
                {
                    packet.AddError("Error", "First Name cannot be more than 40 characters");
                }
            }
            if (!string.IsNullOrEmpty(packet.Dto.LastName))
            {
                if (!Regex.IsMatch(packet.Dto.LastName, @"^[a-zA-Z]+$"))
                {
                    packet.AddError("Error", "Last Name Please enter characters only");
                }

                if (packet.Dto.LastName.Length > 40)
                {
                    packet.AddError("Error", "Last Name cannot be more than 40 characters");
                }
            }
            if (!string.IsNullOrEmpty(packet.Dto.AgencyId))
            {
                var agency = await _repoFactory.GetRepo().FindAll<Agency>()
                                    .IncludeAgencyWorkflow()
                                    .Where(a => (a.Id == packet.Dto.AgencyId || a.CustomId == packet.Dto.AgencyId) &&
                                                    a.AgencyWorkflowState != null &&
                                                    string.Equals(a.AgencyWorkflowState.Name, "approved"))
                                    .FirstOrDefaultAsync();

                if (agency == null)
                {
                    packet.AddError("Error", "Agency Code does not exist or not approved");
                }
            }
            if (packet.Dto.Roles != null && packet.Dto.Roles.Count() > 0)
            {
                foreach (var role in packet.Dto.Roles)
                {
                    var department = await _repoFactory.GetRepo().FindAll<Department>().Where(a => a.Id == role.DepartmentId).FirstOrDefaultAsync();

                    if (department == null)
                    {
                        packet.AddError("Error", "Department is invalid");
                    }

                    var designation = await _repoFactory.GetRepo().FindAll<Designation>().Where(a => a.Id == role.DesignationId).FirstOrDefaultAsync();

                    if (designation == null)
                    {
                        packet.AddError("Error", "designation is invalid");
                    }
                }
            }

            if (packet.Dto.DateOfBirth != null)
            {
                if (packet.Dto.DateOfBirth > DateTime.Now)
                {
                    packet.AddError("Error", "DOB is invalid");
                }
            }

            if (!string.IsNullOrEmpty(packet.Dto.PrimaryMobileNumber))
            {
                if (!Regex.IsMatch(packet.Dto.PrimaryMobileNumber, @"^[0-1]+$"))
                {
                    packet.AddError("Error", "Mobile Number Only numbers are allowed");
                }

                if (packet.Dto.PrimaryMobileNumber.Length != 10)
                {
                    packet.AddError("Error", "Mobile Number 10 digits are required");
                }
            }

            if (!string.IsNullOrEmpty(packet.Dto.FatherName))
            {
                if (!Regex.IsMatch(packet.Dto.FatherName, @"^[a-zA-Z]+$"))
                {
                    packet.AddError("Error", "Father Name Please enter characters only");
                }

                if (packet.Dto.FatherName.Length > 40)
                {
                    packet.AddError("Error", "Father Name cannot be more than 40 characters");
                }
            }

            if (!string.IsNullOrEmpty(packet.Dto.PrimaryEMail))
            {
                if (!Regex.IsMatch(packet.Dto.PrimaryEMail, @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,4}$"))
                {
                    packet.AddError("Error", "Enter valid email");
                }
            }

            if (packet.Dto.Address != null)
            {
                if (packet.Dto.Address?.AddressLine.Length > 200)
                {
                    packet.AddError("Error", "Local Residential Address cannot be more than 200 characters");
                }
            }

            if (packet.Dto.Address != null)
            {
                if (packet.Dto.Address?.PIN.Length != 6)
                {
                    packet.AddError("Error", "Enter valid 6 digit pincode");
                }
            }

            if (packet.Dto.EmploymentDate != null)
            {
                DateTime d;
                bool validate = DateTime.TryParseExact(packet.Dto.EmploymentDate.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);

                if (!validate)
                {
                    packet.AddError("Error", "Collection employment date cannot be future date.");
                }

                if (packet.Dto.EmploymentDate > DateTime.Now)
                {
                    packet.AddError("Error", "Collection employment date cannot be future date.");
                }
            }

            if (packet.Dto.AuthorizationCardExpiryDate != null)
            {
                DateTime d;
                bool validate = DateTime.TryParseExact(packet.Dto.AuthorizationCardExpiryDate.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);

                if (!validate)
                {
                    packet.AddError("Error", "Authorization card expiry date is invalid");
                }

                if (packet.Dto.AuthorizationCardExpiryDate < DateTime.Now)
                {
                    packet.AddError("Error", "Authorization card expiry cannot be of past date");
                }
            }

            if (!string.IsNullOrEmpty(packet.Dto.SupervisorEmailId))
            {
                if (!Regex.IsMatch(packet.Dto.PrimaryEMail, @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,4}$"))
                {
                    packet.AddError("Error", "Supervisor Enter valid email");
                }

                if (packet.Dto.SupervisorEmailId?.Length > 50)
                {
                    packet.AddError("Error", "Supervisor email id cannot be more than 50 characters");
                }
            }

            if (!string.IsNullOrEmpty(packet.Dto.DiallerId))
            {
                if (packet.Dto.DiallerId?.Length > 10)
                {
                    packet.AddError("Error", "Dialer id cannot be more than 10 characters");
                }
            }

            if (packet.Dto.DRACertificateDate != null)
            {
                if (packet.Dto.DRACertificateDate > DateTime.Now)
                {
                    packet.AddError("Error", "DRACertificationDate Date is invalid");
                }
            }
            if (packet.Dto.DRATrainingDate != null)
            {
                if (packet.Dto.DRACertificateDate > DateTime.Now)
                {
                    packet.AddError("Error", "DRATrainingDate Date is invalid");
                }
            }

            if (packet.Dto.DRAUniqueRegistrationNumber != null)
            {
                if (!Regex.IsMatch(packet.Dto.DRAUniqueRegistrationNumber, @"^[0-1]+$"))
                {
                    packet.AddError("Error", "Please enter Valid DRA Unique Registration Number.");
                }
                if (packet.Dto.DRAUniqueRegistrationNumber.Length < 9)
                {
                    packet.AddError("Error", "DRA Unique Registration Number must be atleast 9 digits long");
                }
                if (packet.Dto.DRAUniqueRegistrationNumber.Length > 16)
                {
                    packet.AddError("Error", "DRA Unique Registration Number cannot be more than 16 digits.");
                }
            }
        }
    }
}