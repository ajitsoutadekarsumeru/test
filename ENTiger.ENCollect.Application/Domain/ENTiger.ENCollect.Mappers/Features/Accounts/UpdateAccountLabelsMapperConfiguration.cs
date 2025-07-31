using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateAccountLabelsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateAccountLabelsMapperConfiguration() : base()
        {
            CreateMap<UpdateAccountLabelsDto, AccountLabels>();
        }
    }
}