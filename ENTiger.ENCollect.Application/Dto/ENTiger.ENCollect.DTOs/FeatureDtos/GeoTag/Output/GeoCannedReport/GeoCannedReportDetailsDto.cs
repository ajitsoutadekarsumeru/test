namespace ENTiger.ENCollect.GeoTagModule
{
    public class GeoCannedReportDetailsDto : DtoBridge
    {
        /// <summary>
        /// Unique identifier for the user in ENCollect system.
        /// </summary>
        public string? UserENCollectId { get; set; }

        /// <summary>
        /// Name of the user.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Indicates whether the user is staff.
        /// </summary>
        public bool IsStaff { get; set; }

        /// <summary>
        /// Identifier for the user's agency.
        /// </summary>
        public string? UsersAgencyId { get; set; }

        /// <summary>
        /// Name of the user's agency.
        /// </summary>
        public string? UsersAgencyName { get; set; }

        /// <summary>
        /// Type of the transaction.
        /// </summary>
        public string? TransactionType { get; set; }

        /// <summary>
        /// Date when the transaction was created.
        /// </summary>
        public string? CreatedDate { get; set; }

        /// <summary>
        /// Time when the transaction was created.
        /// </summary>
        public string? CreatedTime { get; set; }

        /// <summary>
        /// Latitude of the transaction location.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude of the transaction location.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Distance in kilometers.
        /// </summary>
        public double DistanceKm { get; set; }

        /// <summary>
        /// Physical address where the transaction occurred.
        /// </summary>
        public string? PhysicalAddress { get; set; }

        /// <summary>
        /// Zip code of the customer's address.
        /// </summary>
        public string? CustomerAddressZipcode { get; set; }

        /// <summary>
        /// Customer's account number.
        /// </summary>
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Unique identifier of the customer.
        /// </summary>
        public string? CustomerID { get; set; }

        /// <summary>
        /// Name of the customer.
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Allocation owner's unique identifier.
        /// </summary>
        public string? AllocationOwnerId { get; set; }

        /// <summary>
        /// Name of the allocation owner.
        /// </summary>
        public string? AllocationOwnerName { get; set; }
    }
}
