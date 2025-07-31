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
        public virtual RunStatus FinishRun()
        {
            this.Status = "Completed";
            this.SetModified();

            return this;
        }

        #endregion "Public Methods"
    }
}