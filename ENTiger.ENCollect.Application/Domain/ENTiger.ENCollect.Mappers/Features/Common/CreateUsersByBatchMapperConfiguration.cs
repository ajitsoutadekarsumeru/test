using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateUsersByBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CreateUsersByBatchMapperConfiguration() : base()
        {
            CreateMap<CreateUsersByBatchDto, UsersCreateFile>();
        }
    }
}