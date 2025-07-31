using ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Input.UpdateSegment;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class UpdateSegmentDto : DtoBridge
    {
        [StringLength(50)]
        public string? Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(20)]
        public string? ExecutionType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? ProductGroup { get; set; }

        [StringLength(200)]
        public string? Product { get; set; }

        [StringLength(200)]
        public string? SubProduct { get; set; }

        [StringLength(50)]
        public string? BOM_Bucket { get; set; }

        [StringLength(50)]
        public string? CurrentBucket { get; set; }

        [StringLength(100)]
        public string? NPA_Flag { get; set; }

        [StringLength(100)]
        public string? Current_DPD { get; set; }

        [StringLength(100)]
        public string? Zone { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Branch { get; set; }

        public int? Sequence { get; set; }

        public EditSegmentationFilterDto SegmentationFilterInputModel { get; set; }

        public string? ClusterName { get; set; }
    }
}