using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LanguageMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LanguageMapperConfiguration() : base()
        {
            CreateMap<LanguageDto, Language>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDtoWithId, Language>();
            CreateMap<Language, LanguageDtoWithId>();
        }
    }
}