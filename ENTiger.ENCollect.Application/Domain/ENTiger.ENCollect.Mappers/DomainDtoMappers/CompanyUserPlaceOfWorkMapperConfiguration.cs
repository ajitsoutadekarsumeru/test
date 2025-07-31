using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserPlaceOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserPlaceOfWorkMapperConfiguration() : base()
        {
            CreateMap<CompanyUserPlaceOfWorkDto, CompanyUserPlaceOfWork>();
            CreateMap<CompanyUserPlaceOfWork, CompanyUserPlaceOfWorkDto>();
            CreateMap<CompanyUserPlaceOfWorkDtoWithId, CompanyUserPlaceOfWork>();
            CreateMap<CompanyUserPlaceOfWork, CompanyUserPlaceOfWorkDtoWithId>();
        }
    }
}