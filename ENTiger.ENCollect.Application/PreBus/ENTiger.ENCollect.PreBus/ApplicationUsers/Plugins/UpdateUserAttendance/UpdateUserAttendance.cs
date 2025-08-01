﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.ApplicationUsersModule.UpdateUserAttendanceApplicationUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateUserAttendance : FlexiBusinessRuleBase, IFlexiBusinessRule<UpdateUserAttendanceDataPacket>
    {
        public override string Id { get; set; } = "3a13241958ad15f30ae3421ee26ca361";
        public override string FriendlyName { get; set; } = "UpdateUserAttendance";

        protected readonly ILogger<UpdateUserAttendance> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public UpdateUserAttendance(ILogger<UpdateUserAttendance> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(UpdateUserAttendanceDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            _flexAppContext = packet.Dto.GetAppContext();
            string partyId = _flexAppContext.UserId;

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            UserAttendanceLog log = await _repoFactory.GetRepo().FindAll<UserAttendanceLog>()
                                            .ByUserAttendanceLogUserId(partyId)
                                            .OrderByDescending(t => t.CreatedDate)
                                            .FirstOrDefaultAsync();

            log.IsSessionValid = false;
            log.LogOutTime = packet.Dto?.LogOutTime ?? DateTime.Now;
            log.LogOutLatitude = packet.Dto?.LogOutLatitude;
            log.LogOutLatitude = packet.Dto?.LogOutLongitude;
            log.TransactionSource = _flexAppContext?.RequestSource;
            log.SetLastModifiedBy(partyId);
            log.SetLastModifiedDate(DateTimeOffset.Now);
            log.SetModified();

            _repoFactory.GetRepo().InsertOrUpdate(log);
            int result = await _repoFactory.GetRepo().SaveAsync();
        }
    }
}
