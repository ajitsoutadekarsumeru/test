using ENTiger.ENCollect.CommunicationModule;
using Org.BouncyCastle.Bcpg;
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
        public virtual CommunicationTemplate UpdateTemplateStatus(UpdateTemplateStatusCommand cmd)
        {
            Guard.AgainstNull("CommunicationTemplate model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            if (cmd.Dto.IsDisabled)
                this.IsActive = false;
            else
                this.IsActive = true;

            this.SetModified();
            return this;
        }

        #endregion "Public Methods"
    }
}