using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUsersByIdsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? CustomId { get; set; }
        public string? EmployeeId { get; set; }
        public string? PrimaryEmail { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? BaseBranch { get; set; }
    }
}
