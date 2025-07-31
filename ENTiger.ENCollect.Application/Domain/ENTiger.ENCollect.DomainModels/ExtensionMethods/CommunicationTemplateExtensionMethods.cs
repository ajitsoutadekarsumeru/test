using Sumeru.Flex;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENTiger.ENCollect
{
    public static class CommunicationTemplateExtensionMethods
    {
        #region CommunicationTemplate Include Methods

        public static IQueryable<T> IncludeTemplateDetails<T>(this IQueryable<T> communicationTemplate) where T : CommunicationTemplate
        {
            return communicationTemplate.FlexInclude(x => x.CommunicationTemplateDetails);
        }

        #endregion CommunicationTemplate Include Methods

        #region CommunicationTemplate Filter Methods

        public static IQueryable<T> ByTemplateName<T>(this IQueryable<T> communicationTemplate, string? Name) where T : CommunicationTemplate
        {
            if (!string.IsNullOrEmpty(Name))
            {
                communicationTemplate = communicationTemplate.Where(w => w.Name.StartsWith(Name));
            }
            return communicationTemplate;
        }
        public static IQueryable<T> ByTemplateIds<T>(this IQueryable<T> communicationTemplate, List<string> ids) where T : CommunicationTemplate
        {
            if (ids != null && ids.Any())
            {
                communicationTemplate = communicationTemplate.Where(i => ids.Contains(i.Id));
            }
            return communicationTemplate;
        }


        public static IQueryable<T> ByTemplateType<T>(this IQueryable<T> communicationTemplate, string? Type) where T : CommunicationTemplate
        {
            if (!string.IsNullOrEmpty(Type))
            {
                communicationTemplate = communicationTemplate.Where(i => i.TemplateType == Type);
            }
            return communicationTemplate;
        }

        public static IQueryable<T> ByEntryPoint<T>(this IQueryable<T> communicationTemplate, string? entryPoint) where T : CommunicationTemplate
        {
            if (!string.IsNullOrEmpty(entryPoint))
            {
                communicationTemplate = communicationTemplate.Where(i => i.EntryPoint == entryPoint);
            }
            return communicationTemplate;
        }
        public static IQueryable<T> ByRecipientType<T>(this IQueryable<T> communicationTemplate, string? recipientType) where T : CommunicationTemplate
        {
            if (!string.IsNullOrEmpty(recipientType))
            {
                communicationTemplate = communicationTemplate.Where(i => i.RecipientType == recipientType);
            }
            return communicationTemplate;
        }

        public static IQueryable<T> ByActiveTemplate<T>(this IQueryable<T> communicationTemplate) where T : CommunicationTemplate
        {
            return communicationTemplate.Where(w => w.IsActive);
        }
        public static IQueryable<T> ByAvailAccountsTemplate<T>(this IQueryable<T> communicationTemplate) where T : CommunicationTemplate
        {
            return communicationTemplate.Where(w => w.IsAvailableInAccountDetails);
        }

        #endregion CommunicationTemplate Filter Methods

        #region Communication Template Trigger Include methods
        public static IQueryable<T> IncludeTemplateTriggers<T>(this IQueryable<T> communicationTemplate) where T : CommunicationTemplate
        {
            return communicationTemplate.FlexInclude(x => x.TemplateTriggers);
        }
        #endregion
    }
}