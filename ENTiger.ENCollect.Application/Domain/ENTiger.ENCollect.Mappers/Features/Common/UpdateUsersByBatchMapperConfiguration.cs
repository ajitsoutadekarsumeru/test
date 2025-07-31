using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateUsersByBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateUsersByBatchMapperConfiguration() : base()
        {
            CreateMap<UpdateUsersByBatchDto, UsersUpdateFile>();
        }
    }
}