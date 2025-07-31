using ENTiger.ENCollect.UserSearchCriteriaModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserSearchCriteria : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual UserSearchCriteria Add(AddCommand cmd)
        {
            Guard.AgainstNull("UserSearchCriteria command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.UserId = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}