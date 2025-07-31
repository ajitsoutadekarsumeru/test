using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class IdConfigMaster : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual IdConfigMaster UpdateSequence(string name, int value)
        {
            Guard.AgainstNull("UpdateSequence model cannot be empty", name);
            this.CodeType = name;
            this.LatestValue = value;

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}