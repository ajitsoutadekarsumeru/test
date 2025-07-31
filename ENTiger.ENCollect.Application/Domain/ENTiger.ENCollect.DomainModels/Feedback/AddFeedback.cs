using ENTiger.ENCollect.FeedbackModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class Feedback : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual Feedback AddFeedback(AddFeedbackCommand cmd)
        {
            Guard.AgainstNull("Feedback command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.FeedbackDate = DateTime.Now;
            this.DispositionDate = DateTime.Now;
            this.CollectorId = cmd.Dto.GetAppContext()?.UserId;
            this.UserId = cmd.Dto.GetAppContext()?.UserId;
            this.TransactionSource = cmd.Dto.GetAppContext()?.RequestSource;
            this.SetAdded();

            this.SetAdded(cmd.Dto.GetGeneratedId());

            return this;
        }

        #endregion "Public Methods"
    }
}