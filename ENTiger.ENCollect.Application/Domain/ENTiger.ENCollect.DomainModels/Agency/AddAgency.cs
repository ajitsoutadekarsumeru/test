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
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual Agency AddAgency(AddAgencyCommand cmd)
        {
            Guard.AgainstNull("Agency command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.SetAdded();

            //Map any other field not handled by Automapper config
            this.Address?.SetAdded();
            this.AgencyWorkflowState?.SetAdded();
            this.CreditAccountDetails?.SetAdded();
            //this.PlaceOfWork?.SetAddedOrModified();
            this.PlaceOfWork?.ToList().ForEach(x =>
            {
                x.SetCreatedBy(this.CreatedBy);
                x.AgencyId = this.Id;
                x.SetAdded();
            });
            //this.ScopeOfWork?.SetAddedOrModified();
            this.ScopeOfWork?.ToList().ForEach(x =>
            {
                x.SetCreatedBy(this.CreatedBy);
                x.AgencyId = this.Id;
                x.SetAdded();
            });

            return this;
        }

        #endregion "Public Methods"
    }
}