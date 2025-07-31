using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class RequestSettlementDto : DtoBridge
    {
        // Link to the loan account
        [Required]
        public string LoanAccountId { get; set; }

        // Snapshot of current dues
        [Required]
        public string CurrentBucket { get; set; }
        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal PrincipalOutstanding { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NumberOfEmisDue { get; set; }
        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal ChargesOutstanding { get; set; }
        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal InterestOutstanding { get; set; }

        // Settlement details
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Settlement amount must be greater than zero.")]
        public decimal SettlementAmount { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Number of installments must be between 1 and 3.")]
        public int NumberOfInstallments { get; set; }
        [Required]
        public DateTime SettlementDateForDuesCalc { get; set; }

        // Death settlement specifics
        [Required]
        public bool IsDeathSettlement { get; set; }
        public DateTime? DateOfDeath { get; set; }

        // Remarks
        [Required]
        [StringLength(1000, ErrorMessage = "Remarks cannot exceed 1000 characters.")]
        public string SettlementRemarks { get; set; }

        // Tranche configuration
        [Required]       
        public string TrancheType { get; set; }

        // Waiver grid
        [Required]
        [MinLength(1, ErrorMessage = "At least one waiver detail is required.")]
        public List<WaiverDetailDto> WaiverDetails { get; set; }

        // Installment plan
        [Required]
        [MinLength(1, ErrorMessage = "At least one installment detail is required.")]
        [MaxLength(3, ErrorMessage = "At most 3 installment details are allowed.")]
        public List<InstallmentDetailDto> Installments { get; set; }

        //documents upload
      
        public List<DocumentsDto> Documents { get; set; }


    }

    public class WaiverDetailDto
    {
        [ValidateNever]
        public string Id { get; set; }
        [Required]
        public string ChargeType { get; set; }
        [Required]
        public decimal AmountAsPerCBS { get; set; }
        [Required]
        public decimal ApportionmentAmount { get; set; }
        [Required]
        public decimal WaiverAmount { get; set; }
        [Required]
        public decimal WaiverPercent { get; set; }
    }
    public class InstallmentDetailDto
    {
        [ValidateNever]
        public string Id { get; set; }
        [Required]
        public decimal InstallmentAmount { get; set; }
        [Required]
        public DateTimeOffset InstallmentDueDate { get; set; }
    }

    public class DocumentsDto
    {
        [ValidateNever]
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "Document type cannot exceed 50 characters.")]
        public string DocumentType { get; set; }


       
        [MaxLength(50, ErrorMessage = "Document name cannot exceed 50 characters.")]
        public string DocumentName { get; set; }


       
        [MaxLength(100, ErrorMessage = "File name cannot exceed 100 characters.")]
        public string FileName { get; set; }
    }
}
