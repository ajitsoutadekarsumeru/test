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
        public virtual CommunicationTemplate DeleteTemplate(DeleteTemplateCommand cmd)
        {
            Guard.AgainstNull("CommunicationTemplate model cannot be empty", cmd);

            this.Id = cmd.Dto.Id;
            this.SetIsDeleted(true);
            //this.CommunicationTemplateDetail.SetIsDeleted(true);

            //Set your appropriate SetDeleted for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}