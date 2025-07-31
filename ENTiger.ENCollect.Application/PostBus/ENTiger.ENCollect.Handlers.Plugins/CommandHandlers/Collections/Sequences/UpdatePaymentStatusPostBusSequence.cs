using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public class UpdatePaymentStatusPostBusSequence : FlexiPluginSequenceBase<UpdatePaymentStatusPostBusDataPacket>
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdatePaymentStatusPostBusSequence()
        {
            this.Add<UpdatePaymentStatusPlugin>();
        }
    }
}
