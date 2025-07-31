using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public class CoreMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CoreMapperConfiguration() : base()
        {
            #region Input

            //Sample:
            //CreateMap<YourAPIModel, YourDomainModel>();

            #endregion Input

            #region Output

            //Sample:
            //CreateMap<YourDomainModel, YourOutputAPIModel>();

            #endregion Output
        }
    }
}