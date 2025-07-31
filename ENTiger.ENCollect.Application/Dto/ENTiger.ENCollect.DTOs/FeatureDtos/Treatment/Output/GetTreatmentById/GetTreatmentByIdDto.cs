namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTreatmentByIdDto : DtoBridge
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public string Description { get; set; }
        public string Mode { get; set; }
        public bool? IsDisabled { get; set; }

        public string PaymentStatusToStop { get; set; }

        public DateTime? ExecutionStartdate { get; set; }

        public DateTime? ExecutionEnddate { get; set; }

        public List<ViewSubTreatmentOutputDto> subTreatment { get; set; }
        public List<ViewTreatmentSegmentMappingTreatmentOutputDto> segmentMapping { get; set; }
    }
    public enum TreatmentRules
    {
        SamePBGasAccount = 1,
        LatestAgency = 2,
        LatestCreatedStaff = 3,
        LatestCreatedStaffBasedOnPBG = 4,
        LatestCreatedStaffInDepartmentAndDesignation = 5,
        SameBranchAsAccount = 6,
        Top1BasedOnPBGAndSkill = 7,
        Top1BasedOnDepartmentAndDesignation = 8,
        StaffWhoHasCreatedTheLoan = 9,
        RoundRobin = 10,
        SamePincodeAsAccount = 11,
        SamePersonaInSkillAsAccount = 12,
        AllocateTillLoadIsReached = 13,
        SameSubProductAsAccount = 14
    }
}