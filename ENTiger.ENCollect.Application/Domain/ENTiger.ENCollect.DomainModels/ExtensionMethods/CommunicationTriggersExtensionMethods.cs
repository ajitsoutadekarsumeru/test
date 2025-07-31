using Sumeru.Flex;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class CommunicationTriggersExtensionMethods
    {
        #region CommunicationTrigger Include Methods

        public static IQueryable<T> IncludeTemplateDetails<T>(this IQueryable<T> communicationTrigger) where T : CommunicationTrigger
        {
            return communicationTrigger.FlexInclude(x => x.TriggerTemplates);
        }

        public static IQueryable<T> ExcludeDeletedTrigger<T>(this IQueryable<T> communicationTrigger) where T : CommunicationTrigger
        {
            return communicationTrigger = communicationTrigger.Where(c => c.IsDeleted == false);
        }



        #endregion CommunicationTrigger Include Methods

        #region CommunicationTriggers Filter Methods
        public static IQueryable<T> ByTriggersId<T>(this IQueryable<T> communicationTrigger, string Id) where T : CommunicationTrigger
        {
            if (!string.IsNullOrEmpty(Id))
            {
                communicationTrigger = communicationTrigger.Where(i => i.Id == Id);
            }
            return communicationTrigger;
        }
        public static IQueryable<T> ByTriggerIds<T>(this IQueryable<T> communicationTrigger, List<string> ids) where T : CommunicationTrigger
        {
            if (ids != null && ids.Any())
            {
                communicationTrigger = communicationTrigger.Where(i => ids.Contains(i.Id));
            }
            return communicationTrigger;
        }
        public static IQueryable<T> ByExcludeTriggersId<T>(this IQueryable<T> communicationTrigger, string Id) where T : CommunicationTrigger
        {
            if (!string.IsNullOrEmpty(Id))
            {
                communicationTrigger = communicationTrigger.Where(i => i.Id != Id);
            }
            return communicationTrigger;
        }
        public static IQueryable<T> ByTriggersName<T>(this IQueryable<T> communicationTrigger, string Name) where T : CommunicationTrigger
        {
            if (!string.IsNullOrEmpty(Name))
            {
                communicationTrigger = communicationTrigger.Where(w => w.Name.StartsWith(Name));
            }
            return communicationTrigger;
        }
        public static IQueryable<T> ByTriggerType<T>(this IQueryable<T> communicationTrigger, string TriggerType) where T : CommunicationTrigger
        {
            if (!string.IsNullOrEmpty(TriggerType))
            {
                communicationTrigger = communicationTrigger.Where(i => i.TriggerTypeId == TriggerType);
            }
            return communicationTrigger;
        }
        public static IQueryable<T> ByTriggerDaysOffset<T>(this IQueryable<T> communicationTrigger, int DaysOffset) where T : CommunicationTrigger
        {
            if (DaysOffset > 0)
            {
                communicationTrigger = communicationTrigger.Where(i => i.DaysOffset == DaysOffset);
            }
            return communicationTrigger;
        }
        public static IQueryable<T> ByActiveCommunicationTrigger<T>(this IQueryable<T> communicationTrigger) where T : CommunicationTrigger
        {
            communicationTrigger = communicationTrigger.Where(i => i.IsActive == true);

            return communicationTrigger;
        }

        public static IQueryable<T> ByTriggerStatus<T>(this IQueryable<T> communicationTrigger, bool? status) where T : CommunicationTrigger
        {
            if(status != null)
            {
                communicationTrigger = communicationTrigger.Where(i => i.IsActive == status);
            }

            return communicationTrigger;
        }

        public static IQueryable<T> ByTemplateType<T>(this IQueryable<T> communicationTrigger, string Type) where T : CommunicationTrigger
        {
            if (!string.IsNullOrEmpty(Type) && !string.Equals(Type, "all", StringComparison.OrdinalIgnoreCase))
            {
                communicationTrigger = communicationTrigger.Where(w => w.TriggerTemplates.Any(t => t.CommunicationTemplate.TemplateType == Type));
            }
            return communicationTrigger;
        }

        #endregion CommunicationTriggers Filter Methods

        #region Communication Trigger Template include methods
        public static IQueryable<T> IncludeTriggerDetails<T>(this IQueryable<T> communicationTriggerTemplates) where T : TriggerDeliverySpec
        {
            return communicationTriggerTemplates.FlexInclude(x => x.CommunicationTrigger);
        }
        #endregion

        #region Communication Trigger Template Filter methods
        public static IQueryable<T> ByTemplateId<T>(this IQueryable<T> communicationTriggerTemplates, string templateId) where T : TriggerDeliverySpec
        {
            if (!string.IsNullOrEmpty(templateId))
            {
                communicationTriggerTemplates = communicationTriggerTemplates.Where(i => i.CommunicationTemplateId == templateId);
            }
            return communicationTriggerTemplates;
        }
      

        public static IQueryable<T> ByActiveTrigger<T>(this IQueryable<T> communicationTriggerTemplates) where T : TriggerDeliverySpec
        {
            return communicationTriggerTemplates = communicationTriggerTemplates.Where(w => w.CommunicationTrigger.IsActive && !w.CommunicationTrigger.IsDeleted);
        }
       
        #endregion
    }
}