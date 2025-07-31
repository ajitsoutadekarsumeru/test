using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Net;
using System.Web;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class UpdateUserLevel : IUpdateUserLevel
    {
        protected readonly ILogger<UpdateUserLevel> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly IApplicationUserQueryRepository _userQueryRepository;
        public UpdateUserLevel(ILogger<UpdateUserLevel> logger,            
            IRepoFactory repoFactory,
            IApplicationUserQueryRepository userQueryRepository )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _userQueryRepository = userQueryRepository;
        }

        public virtual async Task Execute(UserDesignationChangedEvent @event, IFlexServiceBusContext serviceBusContext)
        {           
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            // Fetch using _repoFactory.GetRepo()  from domain model Designation, distinct Levels containing the DesignationIds from the event
            var designationLevels = await _repoFactory.GetRepo().FindAll<Designation>()
                .Where(x => @event.DesignationIds.Contains(x.Id))
                .Select(x => x.Level)
                .Distinct()
                .ToListAsync();
            


            //0. fetch all records from projection for the userid
            var queueProjections = await _userQueryRepository.GetQueueProjectionsByUserId(_flexAppContext, @event.ApplicationUserId);

            // check if the queueProjections is null
            if (queueProjections == null || queueProjections.Count == 0)
            {
                queueProjections = new List<UserLevelProjection>();
            }

            //1. delete all records from projection for the userid
            foreach (var queueItem in queueProjections)
            {
                queueItem.Delete();
            }

            //2. add the new record(s) to the UserLevelProjection
            foreach(var level in designationLevels)
            {
                // Use the constructor taking raw data to avoid extra lookups
                var queueProjection = new UserLevelProjection(
                    applicationUserId: @event.ApplicationUserId,
                    level: level
                );
                queueProjections.Add(queueProjection);
            }

            //3. update the UserLevelProjection
            foreach (var obj in queueProjections)
            {
                _repoFactory.GetRepo().InsertOrUpdate(obj);
            }
            int records = await _repoFactory.GetRepo().SaveAsync();


            //EventCondition = CONDITION_ONSUCCESS;
            //await this.Fire<SendEmailOnAgentApproved>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }

       
    }
}