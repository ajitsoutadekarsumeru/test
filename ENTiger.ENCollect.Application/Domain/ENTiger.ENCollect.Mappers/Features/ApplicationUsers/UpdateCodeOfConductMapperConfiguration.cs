using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateCodeOfConductMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateCodeOfConductMapperConfiguration() : base()
        {
            CreateMap<UpdateCodeOfConductDto, ApplicationUser>();

        }
    }
}
