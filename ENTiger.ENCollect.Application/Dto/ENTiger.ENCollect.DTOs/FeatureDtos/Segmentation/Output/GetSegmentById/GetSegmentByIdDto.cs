using ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Output.GetSegmentById;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSegmentByIdDto : DtoBridge
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? ExecutionType { get; set; }

        public string? Description { get; set; }

        public string? ProductGroup { get; set; }

        public string? Product { get; set; }

        public string? SubProduct { get; set; }

        public string? BOM_Bucket { get; set; }

        public string? CurrentBucket { get; set; }

        public string? NPA_Flag { get; set; }

        public string? Current_DPD { get; set; }

        public string? Zone { get; set; }

        public string? Region { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Branch { get; set; }

        public int? Sequence { get; set; }

        public ViewSegmentationFilterDto SegmentationFilterInputModel { get; set; }

        public string? ClusterName { get; set; }
    }
}