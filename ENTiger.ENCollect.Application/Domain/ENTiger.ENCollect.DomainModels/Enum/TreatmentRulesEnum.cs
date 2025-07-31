namespace ENTiger.ENCollect
{
    public enum TreatmentRulesEnum
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