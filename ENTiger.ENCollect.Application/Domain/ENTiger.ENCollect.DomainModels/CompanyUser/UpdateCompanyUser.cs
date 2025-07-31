using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.DomainModels;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUser : ApplicationUser
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CompanyUser UpdateCompanyUser(UpdateCompanyUserCommand cmd, CompanyUser existingCompanyUser)
        {
            Guard.AgainstNull("CompanyUser model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config
            this.BlackListingReason = existingCompanyUser.BlackListingReason;
            this.UserId = existingCompanyUser.UserId;
            this.CustomId = (existingCompanyUser.CustomId);
            this.CreatedBy = (existingCompanyUser.CreatedBy);
            this.CreatedDate = (existingCompanyUser.CreatedDate);

            UpdateWorkflowState(cmd);
            UpdateDesignation(existingCompanyUser);
            UpdateBucketScope(existingCompanyUser);
            UpdateProductScope(existingCompanyUser);
            UpdateGeoScope(existingCompanyUser);
            UpdatePlaceOfWork(existingCompanyUser);
            UpdateLanguage(existingCompanyUser);
            //UpdatePerformanceBand(existingCompanyUser);
            //UpdateCustomerPersona(existingCompanyUser);
            this.SetWalletLimit(cmd);

            this.SetModified();
            this.SetWalletLimit(cmd);

            //Set your appropriate SetModified for the inner object here
            return this;
        }

        #endregion "Public Methods"

        #region "Private Methods"
        private void SetWalletLimit(UpdateCompanyUserCommand cmd)
        {
            if (this.Wallet == null)
            {
                // Wallet does not exist — create one
                var wallet = new Wallet(this.Id, cmd.Dto.WalletLimit);
                wallet.SetAddedOrModified();
                wallet.SetCreatedBy(this.LastModifiedBy);
                wallet.SetLastModifiedBy(this.LastModifiedBy);
                this.Wallet = wallet;
            }
            else if (this.Wallet.WalletLimit != cmd.Dto.WalletLimit)
            {
                // Update existing wallet limit
                this.Wallet.SetWalletLimit(cmd.Dto.WalletLimit);
                this.Wallet.SetAddedOrModified();
                this.Wallet.SetLastModifiedBy(this.LastModifiedBy);
            }
        }

        private void UpdateWorkflowState(UpdateCompanyUserCommand cmd)
        {
            if (cmd.Dto.IsSaveAsDraft == "False")
            {
                this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserSavedAsDraft>()
                    .SetTFlexId(this.Id).SetStateChangedBy(this.LastModifiedBy ?? "");
            }
            else
            {
                this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserPendingApproval>()
                    .SetTFlexId(this.Id).SetStateChangedBy(this.LastModifiedBy ?? "");
            }
        }

        private void UpdateDesignation(CompanyUser existingCompanyUser)
        {
            //Designation
            var designations = new List<CompanyUserDesignation>();

            existingCompanyUser.Designation?.Where(t => !string.IsNullOrEmpty(t.Id)).ToList()
                    .ForEach(t => t.SetAsDeleted(true));

            if (existingCompanyUser.Designation != null)
            {
                foreach (var design in existingCompanyUser.Designation)
                {
                    var obj = this.Designation.Where(c => c.Id == design.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        design.SetAsDeleted(false);
                        design.DepartmentId = obj.DepartmentId;
                        design.DesignationId = obj.DesignationId;
                        design.IsPrimaryDesignation = obj.IsPrimaryDesignation;
                        design.SetLastModifiedBy(this.LastModifiedBy);
                        design.SetLastModifiedDate(DateTimeOffset.Now);
                    }
                    else
                    {
                        design.SetAsDeleted(true);
                    }
                    design.SetModified();
                    designations.Add(design);
                }
            }
            foreach (var obj in this.Designation.Where(c => string.IsNullOrEmpty(c.Id)))
            {
                obj.SetCreatedBy(this.LastModifiedBy);
                obj.SetLastModifiedBy(this.LastModifiedBy);
                obj.SetAddedOrModified();
                designations.Add(obj);
            }
            this.Designation = designations;
        }

        private void UpdateBucketScope(CompanyUser existingCompanyUser)
        {
            //Scope of Work
            var bucketScopes = new List<UserBucketScope>();

            existingCompanyUser.BucketScopes?.Where(t => !string.IsNullOrEmpty(t.Id)).ToList().ForEach(t => t.SetAsDeleted(true));

            if (existingCompanyUser.BucketScopes != null)
            {
                foreach (var scope in existingCompanyUser.BucketScopes)
                {
                    var obj = this.BucketScopes.Where(c => c.Id == scope.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        scope.SetAsDeleted(false);
                        scope.BucketScopeId = obj.BucketScopeId;
                        scope.SetLastModifiedBy(this.LastModifiedBy);
                        scope.SetLastModifiedDate(DateTimeOffset.Now);
                    }
                    else
                    {
                        scope.SetAsDeleted(true);
                    }
                    scope.SetModified();
                    bucketScopes.Add(scope);
                }
            }

            foreach (var obj in this.BucketScopes.Where(c => string.IsNullOrEmpty(c.Id)))
            {
                obj.SetCreatedBy(this.LastModifiedBy);
                obj.SetLastModifiedBy(this.LastModifiedBy);
                obj.SetAddedOrModified();
                bucketScopes.Add(obj);
            }
            this.BucketScopes = bucketScopes;
        }

        private void UpdateProductScope(CompanyUser existingCompanyUser)
        {
            //Scope of Work
            var productScopes = new List<UserProductScope>();

            existingCompanyUser.ProductScopes?.Where(t => !string.IsNullOrEmpty(t.Id)).ToList().ForEach(t => t.SetAsDeleted(true));

            if (existingCompanyUser.ProductScopes != null)
            {
                foreach (var scope in existingCompanyUser.ProductScopes)
                {
                    var obj = this.ProductScopes.Where(c => c.Id == scope.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        scope.SetAsDeleted(false);
                        scope.ProductScopeId = obj.ProductScopeId;
                        scope.SetLastModifiedBy(this.LastModifiedBy);
                        scope.SetLastModifiedDate(DateTimeOffset.Now);
                    }
                    else
                    {
                        scope.SetAsDeleted(true);
                    }
                    scope.SetModified();
                    productScopes.Add(scope);
                }
            }

            foreach (var obj in this.ProductScopes.Where(c => string.IsNullOrEmpty(c.Id)))
            {
                obj.SetCreatedBy(this.LastModifiedBy);
                obj.SetLastModifiedBy(this.LastModifiedBy);
                obj.SetAddedOrModified();
                productScopes.Add(obj);
            }
            this.ProductScopes = productScopes;
        }

        private void UpdateGeoScope(CompanyUser existingCompanyUser)
        {
            //Scope of Work
            var geoScopes = new List<UserGeoScope>();

            existingCompanyUser.GeoScopes?.Where(t => !string.IsNullOrEmpty(t.Id)).ToList().ForEach(t => t.SetAsDeleted(true));

            if (existingCompanyUser.GeoScopes != null)
            {
                foreach (var scope in existingCompanyUser.GeoScopes)
                {
                    var obj = this.GeoScopes.Where(c => c.Id == scope.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        scope.SetAsDeleted(false);
                        scope.GeoScopeId = obj.GeoScopeId;
                        scope.SetLastModifiedBy(this.LastModifiedBy);
                        scope.SetLastModifiedDate(DateTimeOffset.Now);
                    }
                    else
                    {
                        scope.SetAsDeleted(true);
                    }
                    scope.SetModified();
                    geoScopes.Add(scope);
                }
            }

            foreach (var obj in this.GeoScopes.Where(c => string.IsNullOrEmpty(c.Id)))
            {
                obj.SetCreatedBy(this.LastModifiedBy);
                obj.SetLastModifiedBy(this.LastModifiedBy);
                obj.SetAddedOrModified();
                geoScopes.Add(obj);
            }
            this.GeoScopes = geoScopes;
        }

        private void UpdatePlaceOfWork(CompanyUser existingCompanyUser)
        {
            //Place Of Work
            var companyUserPlaceOfWorkList = new List<CompanyUserPlaceOfWork>();

            existingCompanyUser.PlaceOfWork?.Where(t => !string.IsNullOrEmpty(t.Id)).ToList()
                .ForEach(t => t.SetAsDeleted(true));

            if (existingCompanyUser.PlaceOfWork != null)
            {
                foreach (var placeOfWork in existingCompanyUser.PlaceOfWork)
                {
                    var obj = this.PlaceOfWork.Where(c => c.Id == placeOfWork.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        placeOfWork.SetAsDeleted(false);
                        placeOfWork.SetLastModifiedBy(this.LastModifiedBy);
                        placeOfWork.SetLastModifiedDate(DateTimeOffset.Now);
                        placeOfWork.PIN = obj.PIN;
                    }
                    else
                    {
                        placeOfWork.SetAsDeleted(true);
                    }
                    placeOfWork.SetModified();
                    companyUserPlaceOfWorkList.Add(placeOfWork);
                }
            }

            foreach (var obj in this.PlaceOfWork.Where(c => string.IsNullOrEmpty(c.Id)))
            {
                obj.SetCreatedBy(this.LastModifiedBy);
                obj.SetLastModifiedBy(this.LastModifiedBy);
                obj.SetAddedOrModified();
                companyUserPlaceOfWorkList.Add(obj);
            }

            this.PlaceOfWork = companyUserPlaceOfWorkList;
        }

        private void UpdateLanguage(CompanyUser existingCompanyUser)
        {
            //Languages
            List<Language> languageList = new List<Language>();

            existingCompanyUser.Languages?.Where(t => !string.IsNullOrEmpty(t.Id)).ToList().ForEach(t => t.SetAsDeleted(true));
            if (existingCompanyUser.Languages != null)
            {
                foreach (var language in existingCompanyUser.Languages)
                {
                    //language.SetAsDeleted(this.Languages.Where(a => a.Id == language.Id).Select(a => a.IsDeleted).FirstOrDefault());
                    var obj = this.Languages.Where(c => c.Id == language.Id).FirstOrDefault();
                    if (obj != null && obj.IsDeleted == true)
                    {
                        language.SetAsDeleted(false);
                        language.SetLastModifiedBy(this.LastModifiedBy);
                        language.SetLastModifiedDate(DateTimeOffset.Now);
                    }
                    language.SetModified();
                    languageList.Add(language);
                }
            }

            foreach (var obj in this.Languages.Where(c => string.IsNullOrEmpty(c.Id)))
            {
                obj.SetCreatedBy(this.LastModifiedBy);
                obj.SetLastModifiedBy(this.LastModifiedBy);
                obj.ApplicationUserId = this.Id;
                obj.SetAddedOrModified();
                languageList.Add(obj);
            }

            this.Languages = languageList;
        }

        //private void UpdatePerformanceBand(CompanyUser existingCompanyUser)
        //{
        //    //User Performance Band
        //    List<UserPerformanceBand> userPerformancBandList = new List<UserPerformanceBand>();

        //    if (existingCompanyUser.userPerformanceBand != null)
        //    {
        //        foreach (var performanceBand in existingCompanyUser.userPerformanceBand)
        //        {
        //            //performanceBand.SetAsDeleted(this.userPerformanceBand.Where(a => a.Id == performanceBand.Id).Select(a => a.IsDeleted).FirstOrDefault());
        //            var obj = this.userPerformanceBand.Where(c => c.Id == performanceBand.Id).FirstOrDefault();
        //            if (obj != null && obj.IsDeleted == true)
        //            {
        //                performanceBand.SetDeleted();
        //            }
        //            performanceBand.SetModified();
        //            userPerformancBandList.Add(performanceBand);
        //        }
        //    }

        //    foreach (var obj in this.userPerformanceBand.Where(c => string.IsNullOrEmpty(c.Id)))
        //    {
        //        obj.SetCreatedBy(this.LastModifiedBy);
        //        obj.SetLastModifiedBy(this.LastModifiedBy);
        //        obj.SetAddedOrModified();
        //        userPerformancBandList.Add(obj);
        //    }

        //    this.userPerformanceBand = userPerformancBandList;
        //}

        //private void UpdateCustomerPersona(CompanyUser existingCompanyUser)
        //{
        //    //User Customer Persona
        //    var userCustomerPersonaList = new List<UserCustomerPersona>();

        //    if (existingCompanyUser.userCustomerPersona != null)
        //    {
        //        foreach (var userPersona in existingCompanyUser.userCustomerPersona)
        //        {
        //            //userPersona.SetIsDeleted(this.userCustomerPersona.Where(a => a.Id == userPersona.Id).Select(a => a.IsDeleted).FirstOrDefault());
        //            var obj = this.userCustomerPersona.Where(c => c.Id == userPersona.Id).FirstOrDefault();
        //            if (userPersona.IsDeleted == true)
        //            {
        //                userPersona.SetDeleted();
        //            }
        //            userPersona.SetModified();
        //            userCustomerPersonaList.Add(userPersona);
        //        }
        //    }

        //    foreach (var obj in this.userCustomerPersona.Where(c => string.IsNullOrEmpty(c.Id)))
        //    {
        //        obj.SetCreatedBy(this.LastModifiedBy);
        //        obj.SetLastModifiedBy(this.LastModifiedBy);
        //        obj.SetAddedOrModified();
        //        userCustomerPersonaList.Add(obj);
        //    }

        //    this.userCustomerPersona = userCustomerPersonaList;
        //}

        #endregion "Private Methods"
    }
}