using ENTiger.ENCollect.AgencyModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Agency : ApplicationOrg
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Agency EditCollectionAgency(EditCollectionAgencyCommand cmd)
        {
            Guard.AgainstNull("Agency model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config

            this.SetModified();

            //this.ScopeOfWork.SetAddedOrModified();
            //this.PlaceOfWork.SetAddedOrModified();
            //this.Address.SetAddedOrModified();
            //this.AgencyWorkflowState.SetModified();
            //this.CreditAccountDetails.SetAddedOrModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}