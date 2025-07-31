using Sumeru.Flex;
using ENTiger.ENCollect.CommunicationModule;

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
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual CommunicationTrigger RunTriggers(RunTriggersCommand cmd)
        {
            Guard.AgainstNull("CommunicationTrigger command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here



            return this;
        }
#endregion


        #region "Private Methods"
        #endregion

    }
}
