using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AgencyUserDto : DtoBridge
    {
        [StringLength(200)]
        public string? UserId { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string? MiddleName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(50)]
        public string? PrimaryMobileNumber { get; set; }
        [StringLength(50)]
        public string? SecondaryContactNumber { get; set; }
        [StringLength(500)]
        public string? ProfileImage { get; set; }
        [StringLength(200)]
        public string? PrimaryEMail { get; set; }
        [StringLength(256)]
        public string? ActivationCode { get; set; }
        public bool isOrganization { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}