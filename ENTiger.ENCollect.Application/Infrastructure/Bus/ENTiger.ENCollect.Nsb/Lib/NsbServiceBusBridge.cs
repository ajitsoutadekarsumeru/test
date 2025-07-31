using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbServiceBusBridge : NsbMessageSession, IFlexServiceBusBridge
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="endpointName"></param>
        /// <param name="routes"></param>
        public NsbServiceBusBridge(IMessageSession endpointSession) : base(endpointSession)
        {
        }
    }
}