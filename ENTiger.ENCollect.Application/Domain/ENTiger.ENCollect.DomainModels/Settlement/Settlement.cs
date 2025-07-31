using ENTiger.ENCollect.SettlementModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Settlement : DomainModelBridge
    {
        protected readonly ILogger<Settlement> _logger;

        protected Settlement()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<Settlement>>();
        }

        public Settlement(ILogger<Settlement> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        [StringLength(50)]
        public string CustomId { get; set; }
        [StringLength(100)]
        public string Status { get; set; }

        // Link back to loanAccount
        [StringLength(32)]
        public string? LoanAccountId { get; set; }

        public LoanAccount LoanAccount { get; set; }

        // --- Snapshot of LoanAccount ---
        [StringLength(50)]
        public string? CURRENT_BUCKET { get; set; }
        public decimal PrincipalOutstanding { get; set; }
        public int NumberOfEmisDue { get; set; }
        public decimal ChargesOutstanding { get; set; }
        public decimal InterestOutstanding { get; set; }

        // --- Settlement Info entered by user ---
        public decimal SettlementAmount { get; set; }
        public decimal? RenegotiationAmount { get; private set; }
        public int NumberOfInstallments { get; set; }
        public DateTime SettlementDateForDuesCalc { get; set; }
        [StringLength(1000)]
        public string? SettlementRemarks { get; set; }
        [StringLength(1000)]
        public string? RejectionReason { get; set; }
        [StringLength(1000)]
        public string? CancellationReason { get; set; }

        // --- Death settlement (optional) ---
        public bool IsDeathSettlement { get; set; }
        public DateTimeOffset? DateOfDeath { get; set; }

        public DateTimeOffset StatusUpdatedOn { get; set; }
        [StringLength(50)]
        public string? TOS { get; set; }
        public long? CURRENT_DPD { get; set; }
        [StringLength(50)]
        public string? NPA_STAGEID { get; set; }

        // --- Waiver grid ---        
        private readonly List<WaiverDetail> _waiverDetails = new();
        public IReadOnlyCollection<WaiverDetail> WaiverDetails => _waiverDetails;
        //public IReadOnlyCollection<WaiverDetail> WaiverDetails => _waiverDetails.AsReadOnly();

        // --- Tranche configuration ---
        [StringLength(50)]
        public string TrancheType { get; set; }
        private readonly List<InstallmentDetail> _installments = new();
        public IReadOnlyCollection<InstallmentDetail> Installments => _installments;

        private readonly List<SettlementStatusHistory> _statusHistory = new();
        public IReadOnlyCollection<SettlementStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

        [StringLength(32)]
        public string? LatestHistoryId { get; set; }

        public SettlementStatusHistory LatestHistory { get; set; }

        public DateTime SettlementDate { get; set; }

        private readonly List<SettlementDocument> _Documents = new();
        public IReadOnlyCollection<SettlementDocument> Documents => _Documents;

        private readonly List<SettlementQueueProjection> _queueProjections = new();

        /// <summary>
        /// All current queue entries for this settlement.
        /// </summary>
        public IReadOnlyCollection<SettlementQueueProjection> QueueProjections
            => _queueProjections.AsReadOnly();


        #endregion

        #region "Protected"
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion
        #region "Public Methods"
        public void AddWaiverDetail(string chargeType,
                               decimal amountAsPerCBS,
                               decimal apportionmentAmount,
                               decimal waiverAmount)
        {
            var waiver = new WaiverDetail(
                chargeType: chargeType,
                amountAsPerCBS: amountAsPerCBS,
                apportionmentAmount: apportionmentAmount,
                waiverAmount: waiverAmount
               );
            _waiverDetails.Add(waiver);
            
        }

        // --- Installments helper ---
        public void AddInstallment(decimal installmentAmount, DateTimeOffset installmentDueDate)
        {
            //if (_installments.Count >= 3)
            //    throw new InvalidOperationException("Cannot have more than 3 installments.");

            var inst = new InstallmentDetail(

                installmentAmount: installmentAmount,
                installmentDueDate: installmentDueDate
            );
            _installments.Add(inst);
        }

        public void AddStatus(string newStatus,
                                string changedBy,
                                string comment = null)
        {
            if (newStatus == null) throw new ArgumentNullException(nameof(newStatus));
            if (changedBy == string.Empty) throw new ArgumentException("changedBy");

            var previousStatus = "Initial";
            //var now = DateTimeOffset.Now;
            

            var nowWithoutOffset = new DateTimeOffset(
              DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
              TimeSpan.Zero
            );

            var historyEntry = new SettlementStatusHistory(
                settlementId: Id,
                fromStatus: previousStatus, 
                toStatus: newStatus,                
                changedBy: changedBy,
                changedDate: nowWithoutOffset,
                comment: comment                
            );
            _statusHistory.Add(historyEntry);

            // Update the LatestHistory reference
            //LatestHistoryId = historyEntry.Id;
            //LatestHistory = historyEntry;

            Status = newStatus;
            StatusUpdatedOn = nowWithoutOffset;
            LastModifiedBy = changedBy;
            LastModifiedDate = nowWithoutOffset;
            TrackingState = TrackingState.Modified;
        }

        public void ChangeStatus(string newStatus,                               
                                 string changedBy,
                                 UpdateStatusOfSettlementDto dto,
                                 string? action = null)
        {

            if (newStatus == null) throw new ArgumentNullException(nameof(newStatus));
            if (changedBy == string.Empty) throw new ArgumentException("changedBy");

            var previousStatus = Status;
            var now = DateTime.Now;
            var nowWithoutOffset = new DateTimeOffset(now,
                    TimeSpan.Zero);
            string comment = dto.Remarks?.Trim() ?? string.Empty;
            
            string rejectionReason = dto.RejectionReason?.Trim() ?? string.Empty;
            string settlementLetterFileName = dto.CustomerSignedLetter?.Trim() ?? string.Empty;
            decimal renegotiationAmount = dto.RenegotiateAmount ?? this.SettlementAmount;

            var historyEntry = new SettlementStatusHistory(
                settlementId: Id,
                fromStatus: previousStatus,
                toStatus: newStatus,
                changedBy: changedBy,
                changedDate: nowWithoutOffset,
                comment: comment,
                rejectionReason: rejectionReason,
                action: action ?? newStatus
            );
            _statusHistory.Add(historyEntry);

            // Update the LatestHistory reference
            LatestHistoryId = historyEntry.Id;
            LatestHistory = historyEntry;

            Status = newStatus;
            SettlementRemarks = comment;
            RejectionReason = rejectionReason;
            RenegotiationAmount = renegotiationAmount;
            if (!string.IsNullOrWhiteSpace(settlementLetterFileName))
            {
                AddDocument("CustomerSettlementLetter", "Customer Signed Settlement Letter", settlementLetterFileName);
            }

            StatusUpdatedOn = nowWithoutOffset;
            LastModifiedBy = changedBy;
            LastModifiedDate = nowWithoutOffset;
            TrackingState = TrackingState.Modified;
        }
        public void ChangeStatus(string newStatus, string userId, string? cancelReason = null)
        {
            var nowWithoutOffset = new DateTimeOffset(
              DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
              TimeSpan.Zero
            );

            var previousStatus = Status;

            var historyEntry = new SettlementStatusHistory(
                settlementId: Id,
                fromStatus: previousStatus,
                toStatus: newStatus,
                changedBy: userId,
                changedDate: nowWithoutOffset,                
                comment: cancelReason ?? string.Empty,
                rejectionReason: string.Empty
            );
            _statusHistory.Add(historyEntry);

            // Update the LatestHistory reference
            LatestHistoryId = historyEntry.Id;
            LatestHistory = historyEntry;

            Status = newStatus;
            StatusUpdatedOn = nowWithoutOffset;
            LastModifiedBy = userId;
            LastModifiedDate = nowWithoutOffset;
            CancellationReason = cancelReason ?? string.Empty;
            TrackingState = TrackingState.Modified;
        }

        public void AddDocument(string documentType,
                               string documentName,
                               string fileName)
        {
            var document = new SettlementDocument(
                documentType: documentType,
                documentName: documentName,
                fileName: fileName
               );
            _Documents.Add(document);
        }

        public void RequestRenegotiation(decimal amount)
        {            
            RenegotiationAmount = amount;
        }
        #endregion

    }

    public class WaiverDetail : IObjectWithState
    {
        [StringLength(32)]
        public string Id { get; private set; }
        [StringLength(50)]
        public string ChargeType { get; private set; }
        public decimal AmountAsPerCBS { get; private set; }
        public decimal ApportionmentAmount { get; private set; }
        public decimal WaiverAmount { get; private set; }
        public decimal WaiverPercent { get; private set; }
        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }

        // EF Core
        private WaiverDetail() { }

        public WaiverDetail(string chargeType,
                            decimal amountAsPerCBS,
                            decimal apportionmentAmount,
                            decimal waiverAmount
                            )
        {
            if (string.IsNullOrWhiteSpace(chargeType))
                throw new ArgumentException("ChargeType is required", nameof(chargeType));
            if (amountAsPerCBS < 0 || apportionmentAmount < 0 || waiverAmount < 0)
                throw new ArgumentException("Amounts cannot be negative");
            if (apportionmentAmount > amountAsPerCBS)
                throw new ArgumentException("Apportionment amount must be less than or equal to AmountAsPerCBS");
            if (waiverAmount > amountAsPerCBS)
                throw new ArgumentException("Waiver amount must be less than or equal to AmountAsPerCBS");
            if (apportionmentAmount + waiverAmount != amountAsPerCBS)
                throw new ArgumentException("Apportionment amount plus Waiver amount must equal AmountAsPerCBS");


            Id = SequentialGuid.NewGuidString();
            ChargeType = chargeType;
            AmountAsPerCBS = amountAsPerCBS;
            ApportionmentAmount = apportionmentAmount;
            WaiverAmount = waiverAmount;
            WaiverPercent = amountAsPerCBS > 0
                             ? Math.Round((waiverAmount / amountAsPerCBS) * 100, 2)
                             : 0;
            TrackingState = TrackingState.Added;
        }

        internal void Delete()
        {
            TrackingState = TrackingState.Deleted;           
        }

        internal void Update(WaiverDetailDto waiverDto)
        {
            //this.Id = waiverDto.Id;
            this.WaiverAmount = waiverDto.WaiverAmount;
            this.ChargeType = waiverDto.ChargeType;
            this.AmountAsPerCBS = waiverDto.AmountAsPerCBS;
            this.ApportionmentAmount = waiverDto.ApportionmentAmount;
            this.WaiverPercent = AmountAsPerCBS > 0
                             ? Math.Round((WaiverAmount / AmountAsPerCBS) * 100, 2)
                             : 0;

            TrackingState = TrackingState.Modified;
        }
    }
    public class InstallmentDetail : IObjectWithState
    {
        [StringLength(32)]
        public string Id { get; private set; }
        public decimal InstallmentAmount { get; set; }
        public DateTimeOffset InstallmentDueDate { get; set; }
        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }

        // EF Core
        private InstallmentDetail() { }
        public InstallmentDetail(decimal installmentAmount,
                                 DateTimeOffset installmentDueDate)
        {
            if (installmentAmount < 0)
                throw new ArgumentException("Installment amount cannot be negative", nameof(installmentAmount));
            if (installmentDueDate == default)
                throw new ArgumentException("Installment due date is required", nameof(installmentDueDate));

            Id = SequentialGuid.NewGuidString();
            InstallmentAmount = installmentAmount;
            InstallmentDueDate = installmentDueDate;
            TrackingState = TrackingState.Added;
        }

        internal void Update(InstallmentDetailDto installmentDto)
        {
            Id = installmentDto.Id;
            InstallmentAmount = installmentDto.InstallmentAmount;
            InstallmentDueDate = installmentDto.InstallmentDueDate;
            TrackingState = TrackingState.Modified;
        }
        internal void Delete()
        {
            TrackingState = TrackingState.Deleted;           
        }

       
    }

    /// <summary>
    /// A history entry recording a status transition and its assignments.
    /// </summary>
    public class SettlementStatusHistory : IObjectWithState
    {
        [StringLength(32)]
        public string Id { get; private set; }
        [StringLength(32)]
        public string SettlementId { get; private set; }
        public Settlement Settlement { get; private set; }
        [StringLength(50)]
        public string FromStatus { get; private set; }
        [StringLength(50)]
        public string ToStatus { get; private set; }
        [StringLength(50)]
        public string Action { get; private set; }
        public decimal? RenegotiationAmount { get; private set; }
        [StringLength(50)]
        public string ChangedByUserId { get; private set; } //need to add FK to ApplicationUser
        public ApplicationUser ChangedByUser { get; private set; } // navigation

        public DateTimeOffset ChangedDate { get; set; }
        [StringLength(500)]
        public string? Comment { get; private set; }
        [StringLength(200)]
        public string? RejectionReason { get; private set; }


        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }
        private SettlementStatusHistory() { }

        public SettlementStatusHistory(string settlementId,
                                       string fromStatus,
                                       string toStatus,
                                       string changedBy,
                                       DateTimeOffset changedDate,
                                       string? comment,
                                       string rejectionReason = null,
                                       string action = null)
        {
            if (settlementId == string.Empty) throw new ArgumentException("settlementId");
            if (changedBy == string.Empty) throw new ArgumentException("changedBy");

            Id = SequentialGuid.NewGuidString();
            SettlementId = settlementId;
            FromStatus = fromStatus;
            ToStatus = toStatus;
            Action = action ?? toStatus; //if action is empty then set toStatus              
            ChangedByUserId = changedBy;
            ChangedDate = changedDate;
            Comment = comment;
            RejectionReason = rejectionReason;
            TrackingState = TrackingState.Added;
        }



    }

    public class SettlementDocument : IObjectWithState
    {
        [StringLength(32)]
        public string Id { get; private set; }

        [StringLength(32)]
        public string SettlementId { get; private set; }

        [StringLength(50)]
        public string DocumentType { get; private set; }

        [StringLength(50)]
        public string DocumentName { get; private set; }

        [StringLength(100)]
        public string? FileName { get; private set; }

        public DateTimeOffset UploadedOn { get; private set; }

        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }

        // EF Core requires a parameterless constructor
        private SettlementDocument() { }

        public SettlementDocument(string documentType, string documentName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(documentType)) throw new ArgumentException("document type is required", nameof(documentType));
            if (string.IsNullOrWhiteSpace(documentName)) throw new ArgumentException("document name is required", nameof(documentName));
            //if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("fileName is required", nameof(fileName));

            var nowWithoutOffset = new DateTimeOffset(
              DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
              TimeSpan.Zero
            );

            Id = SequentialGuid.NewGuidString();
            SettlementId = Id;
            DocumentType = documentType;
            DocumentName = documentName;
            FileName = fileName;
            UploadedOn = nowWithoutOffset;
            TrackingState = TrackingState.Added;
        }
        internal void Delete()
        {
            TrackingState = TrackingState.Deleted;
        }

        internal void Update(DocumentsDto dto)
        {
            //Id = dto.Id;
            var nowWithoutOffset = new DateTimeOffset(
             DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
             TimeSpan.Zero
           );

            DocumentType = dto.DocumentType;
            DocumentName = dto.DocumentName;
            FileName = dto.FileName;
            UploadedOn = nowWithoutOffset;
            TrackingState = TrackingState.Modified;
        }
    }
}
