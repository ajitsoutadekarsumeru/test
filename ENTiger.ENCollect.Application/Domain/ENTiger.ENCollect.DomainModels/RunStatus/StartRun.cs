namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class RunStatus : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual RunStatus StartRun(string FileType, string CustomId, string UserId)
        {
            this.CreatedBy = UserId;
            this.ProcessType = FileType;
            this.CustomId = CustomId;
            this.Status = "Started";

            this.SetAdded();

            return this;
        }

        #endregion "Public Methods"
    }
}