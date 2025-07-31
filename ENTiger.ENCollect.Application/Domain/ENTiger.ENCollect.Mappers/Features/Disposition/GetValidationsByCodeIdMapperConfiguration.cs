using Sumeru.Flex;

namespace ENTiger.ENCollect.DispositionModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetValidationsByCodeIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetValidationsByCodeIdMapperConfiguration() : base()
        {
            CreateMap<DispositionValidationMaster, GetValidationsByCodeIdDto>()
                        .ForMember(cm => cm.FieldName, Dm => Dm.MapFrom(dModel => dModel.validationFieldName));
        }
    }
}