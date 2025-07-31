using Sumeru.Flex;
using ENTiger.ENCollect.SettlementModule;
namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Settlement : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Settlement UpdateStatus(UpdateStatusCommand cmd)
        {
            Guard.AgainstNull("Settlement model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
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
