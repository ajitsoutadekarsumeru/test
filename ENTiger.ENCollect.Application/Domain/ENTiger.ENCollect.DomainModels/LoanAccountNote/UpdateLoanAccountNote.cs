using ENTiger.ENCollect.AccountsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountNote : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual LoanAccountNote UpdateLoanAccountNote(UpdateLoanAccountNoteCommand cmd)
        {
            Guard.AgainstNull("LoanAccountNote model cannot be empty", cmd);
            //this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.Code = cmd.Dto.Code;
            this.Description = cmd.Dto.Description;

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}