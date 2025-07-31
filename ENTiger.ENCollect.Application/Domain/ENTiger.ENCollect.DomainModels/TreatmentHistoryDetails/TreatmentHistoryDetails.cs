using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentHistoryDetails : DomainModelBridge
    {
        protected readonly ILogger<TreatmentHistoryDetails> _logger;

        public TreatmentHistoryDetails()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentHistoryDetails>>();
        }

        public TreatmentHistoryDetails(ILogger<TreatmentHistoryDetails> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(20)]
        public string? bucket { get; set; }

        [StringLength(100)]
        public string? agreementid { get; set; }

        [StringLength(300)]
        public string? customername { get; set; }

        [StringLength(50)]
        public string? allocationownerid { get; set; }

        [StringLength(300)]
        public string? AllocationOwnerName { get; set; }

        [StringLength(50)]
        public string? telecallingagencyid { get; set; }

        [StringLength(300)]
        public string? TCallingAgencyName { get; set; }

        [StringLength(10)]
        public string? current_dpd { get; set; }

        [StringLength(50)]
        public string? telecallerid { get; set; }

        [StringLength(50)]
        public string? agencyid { get; set; }

        [StringLength(300)]
        public string? AgencyName { get; set; }

        [StringLength(50)]
        public string? collectorid { get; set; }

        [StringLength(300)]
        public string? AgentName { get; set; }

        [StringLength(50)]
        public string? treatmentid { get; set; }

        [StringLength(200)]
        public string? TreatmentName { get; set; }

        [StringLength(200)]
        public string? SegmentationName { get; set; }

        [StringLength(300)]
        public string? TCallingAgentName { get; set; }

        [StringLength(50)]
        public string? treatmenthistoryid { get; set; }

        [StringLength(200)]
        public string? npa_stageid { get; set; }

        [StringLength(200)]
        public string? productgroup { get; set; }

        [StringLength(50)]
        public string? latestmobileno { get; set; }

        [StringLength(100)]
        public string? state { get; set; }

        [StringLength(100)]
        public string? zone { get; set; }

        [StringLength(100)]
        public string? segmentationid { get; set; }

        public double? bom_pos { get; set; }
        public decimal? current_pos { get; set; }

        [StringLength(50)]
        public string? current_bucket { get; set; }

        [StringLength(100)]
        public string? loan_amount { get; set; }

        [StringLength(100)]
        public string? tos { get; set; }

        [StringLength(100)]
        public string? dispcode { get; set; }

        [StringLength(200)]
        public string? branch { get; set; }

        [StringLength(200)]
        public string? city { get; set; }

        [StringLength(200)]
        public string? product { get; set; }

        [StringLength(200)]
        public string? subproduct { get; set; }

        [StringLength(200)]
        public string? region { get; set; }

        [StringLength(200)]
        public string? principal_od { get; set; }

        [StringLength(200)]
        public string? customerid { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}