using Sumeru.Flex;
using System.Data;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserDetailsMapperConfiguration() : base()
        {
            CreateMap<CompanyUser, CompanyUserDetailsDto>()
            .ForMember(o => o.Roles, opt => opt.MapFrom(o => o.Designation.Where(a => !a.IsDeleted)))
            .ForMember(o => o.experiance, opt => opt.MapFrom(o => o.Experience))
            .ForMember(o => o.companyUserPlaceOfWorks, opt => opt.MapFrom(o => o.PlaceOfWork.Where(a => !a.IsDeleted)))
            .ForMember(o => o.ProductScopes, opt => opt.MapFrom(o => o.ProductScopes.Where(a => !a.IsDeleted)))
            .ForMember(o => o.GeoScopes, opt => opt.MapFrom(o => o.GeoScopes.Where(a => !a.IsDeleted)))
            .ForMember(o => o.BucketScopes, opt => opt.MapFrom(o => o.BucketScopes.Where(a => !a.IsDeleted)))
            .ForMember(o => o.companyUserPlaceOfWorks, opt => opt.MapFrom(o => o.PlaceOfWork.Where(a => !a.IsDeleted)))
            .ForMember(o => o.WalletLimit, opt => opt.MapFrom(o => o.Wallet != null ? o.Wallet.WalletLimit : 0))
              .ForMember(o => o.UserType, opt => opt.MapFrom(o => o.UserType));
            CreateMap<CompanyUserDesignation, UserDesignationDto>();

            CreateMap<UserProductScope, UserProductScopeDto>();
            CreateMap<UserGeoScope, UserGeoScopeDto>();
            CreateMap<UserBucketScope, UserBucketScopeDto>();

            CreateMap<CompanyUserPlaceOfWork, CompanyUserPlaceOfWorkDto>();

            CreateMap<CompanyUserWorkflowState, ChangeLogInfoDto>()
                .ForMember(o => o.ChangedByUserId, opt => opt.MapFrom(o => o.StateChangedBy))
                .ForMember(o => o.Status, opt => opt.MapFrom(o => o.Name))
                .ForMember(o => o.Remarks, opt => opt.MapFrom(o => o.Remarks))
                .ForMember(o => o.LastModifiedOn, opt => opt.MapFrom(o => o.StateChangedDate.GetValueOrDefault().DateTime));
        }
    }
}