using ENTiger.ENCollect.CommunicationModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CommunicationTemplate : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CommunicationTemplate UpdateTemplate(UpdateTemplateCommand cmd, CommunicationTemplate existingTemplate)
        {
            Guard.AgainstNull("CommunicationTemplate model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.Version += 1;
            this.SetModified();

            UpdateTemplateDetails(existingTemplate);

            return this;
        }

        private void UpdateTemplateDetails(CommunicationTemplate existingTemplate)
        {
            var templateDetails = new List<CommunicationTemplateDetail>();

            foreach (var detail in this.CommunicationTemplateDetails)
            {
                var obj = existingTemplate.CommunicationTemplateDetails.Where(w => w.Id == detail.Id).FirstOrDefault();
                if (obj == null)
                {
                    //added new template details
                    detail.Version = 1;
                    detail.SetCreatedBy(this.LastModifiedBy);
                    detail.SetLastModifiedBy(this.LastModifiedBy);
                    detail.SetAdded();
                    templateDetails.Add(detail);
                }
                else
                {
                    //If the detailed content is changed, the version will be increased
                    if (detail.Body != obj.Body || detail.Subject != obj.Subject)
                    {
                        detail.Version = obj.Version + 1;
                    }                        
                    else
                    {
                        //if the detailed content is not changed, the same version will be maintained
                        detail.Version = obj.Version;
                    }

                    detail.SetLastModifiedBy(this.LastModifiedBy);
                    detail.SetLastModifiedDate(DateTimeOffset.Now);
                    detail.SetModified();
                    templateDetails.Add(detail);
                }
            }

            this.CommunicationTemplateDetails = templateDetails;
        }
        #endregion "Public Methods"
    }
}