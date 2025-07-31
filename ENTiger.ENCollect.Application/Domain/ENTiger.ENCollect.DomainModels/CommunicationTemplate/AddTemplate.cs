using ENTiger.ENCollect.CommunicationModule;

namespace ENTiger.ENCollect
{
    public partial class CommunicationTemplate : DomainModelBridge
    {
        #region "Public Methods"
        public virtual CommunicationTemplate AddTemplate(AddTemplateCommand cmd)
        {
            Guard.AgainstNull("CommunicationTemplate command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.Version = 1;

            this.SetAdded(cmd.Dto.GetGeneratedId());

            this.CommunicationTemplateDetails.SetAddedOrModified();

            //default version added for template details
            foreach (var detail in CommunicationTemplateDetails)
            {
                detail.Version = 1;
            }
            return this;
        }

        #endregion "Public Methods"
    }
}