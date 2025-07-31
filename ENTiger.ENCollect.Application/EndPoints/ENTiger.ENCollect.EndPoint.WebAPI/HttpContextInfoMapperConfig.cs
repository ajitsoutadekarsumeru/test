using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class HttpContextInfoMapperConfig : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public HttpContextInfoMapperConfig() : base()
        {
            #region Input

            CreateMap<FlexDefaultHttpContextAccessorBridge, FlexAppContextBridge>();

            #endregion Input
        }
    }
}