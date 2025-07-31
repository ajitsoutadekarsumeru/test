namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccount : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual LoanAccount PrimaryAllocationByFilter(LoanAccount loanAccount, string loggedInUserId)
        {
            //Guard.AgainstNull("LoanAccount command cannot be empty", cmd);

            //this.Convert(cmd.Dto);
            //this.CreatedDate = DateTime.Now;
            this.LastModifiedDate = DateTime.Now;
            this.LastModifiedBy = loggedInUserId;
            this.LatestAllocationDate = DateTime.Now;
            //Map any other field not handled by Automapper config

            this.SetAddedOrModified();

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}