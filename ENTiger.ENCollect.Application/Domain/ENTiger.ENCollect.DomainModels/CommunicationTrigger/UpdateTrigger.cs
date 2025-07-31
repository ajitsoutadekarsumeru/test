using Sumeru.Flex;
using ENTiger.ENCollect.CommunicationModule;
using Org.BouncyCastle.Bcpg;
namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommunicationTrigger : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CommunicationTrigger UpdateTrigger(UpdateTriggerCommand cmd)
        {
            this.RecipientType = cmd.Dto.RecipientType;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }
        #endregion

        #region "Private Methods"
        #endregion

    }
}
