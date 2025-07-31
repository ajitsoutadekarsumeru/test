using ENTiger.ENCollect.DomainModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    public partial class ApplicationUser : PersonBridge
    {
        [StringLength(100)]
        public string? CustomId { get; protected set; }
        public string? DeactivationReason { get; set; }
        public bool IsDeactivated { get; set; }
        public bool IsBlackListed { get; set; }

        [StringLength(200)]
        public string? BlackListingReason { get; set; }

        [StringLength(200)]
        public string? RejectionReason { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public int ForgotPasswordMailSentCount { get; set; }
        public int ForgotPasswordSMSSentCount { get; set; }
        public int ForgotPasswordCount { get; set; }
        public DateTime? ForgotPasswordDateTime { get; set; }
        public DateTime? UpdateMobileDateTime { get; set; }
        public int UpdateMobileCount { get; set; }

        [StringLength(100)]
        public string? ProductGroup { get; set; }

        [StringLength(100)]
        public string? Product { get; set; }

        [StringLength(200)]
        public string? SubProduct { get; set; }

        [StringLength(50)]
        public string? WorkOfficeLongitude { get; set; }

        [StringLength(50)]
        public string? WorkOfficeLattitude { get; set; }

        [StringLength(50)]
        public string? PrimaryContactCountryCode { get; set; }

        public int? UserLoad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Experience { get; set; }

        [StringLength(32)]
        public string? UserCurrentLocationDetailsId { get; set; }
        public UserCurrentLocationDetails? UserCurrentLocationDetails { get; set; }

        [StringLength(32)]
        public string? CreditAccountDetailsId { get; set; }

        public CreditAccountDetails? CreditAccountDetails { get; set; }

        [StringLength(32)]
        public string? ApplicationUserDetailsId { get; set; }

        public ApplicationUserDetails? ApplicationUserDetails { get; set; }

        [StringLength(5)]
        public string? BloodGroup { get; set; }
        [StringLength(10)]
        public string? EmergencyContactNo { get; set; }
        
        public ICollection<GeoTagDetails>? GeoDetails { get; set; }
        public ICollection<UserAttendanceDetail>? UserAttendanceDetails { get; set; }
        public ICollection<UserAttendanceLog>? UserAttendanceLog { get; set; }
        public ICollection<Language>? Languages { get; set; }
        public ICollection<UserCustomerPersona>? userCustomerPersona { get; set; }
        public ICollection<UserPerformanceBand>? userPerformanceBand { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockedDateTime { get; set; }
        public bool IsPolicyAccepted { get; set; } = false;
        public DateTime? PolicyAcceptedDate { get; set; }
        [StringLength(20)]
        public string? TransactionSource { get; set; }
        [StringLength(20)]
        public string UserType {  get; set; }

        [StringLength(32)]
        public string? ProductLevelId { get; set; }
        public HierarchyLevel? ProductLevel { get; set; }

        [StringLength(32)]
        public string? GeoLevelId { get; set; }
        public HierarchyLevel? GeoLevel { get; set; }

        public ICollection<UserProductScope>? ProductScopes { get; set; }
        public ICollection<UserGeoScope>? GeoScopes { get; set; }
        public ICollection<UserBucketScope>? BucketScopes { get; set; }

        // Navigation to Wallet (1-to-1)
        public Wallet Wallet { get; set; }

        private readonly List<SettlementQueueProjection> _assignedProjections = new();

        /// <summary>
        /// All queue entries assigned to this user.
        /// </summary>
        public IReadOnlyCollection<SettlementQueueProjection> AssignedQueueProjections
            => _assignedProjections.AsReadOnly();

        private readonly List<UserLevelProjection> _userLevelProjections = new();

        /// <summary>
        /// All distinct designation Level entries assigned to this user.
        /// </summary>
        public IReadOnlyCollection<UserLevelProjection> UserLevelProjections
            => _userLevelProjections.AsReadOnly();
        
        public void SetCustomId(string customId)
        {
            this.CustomId = customId;
        }
    }
}