using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;

namespace ENTiger.ENCollect
{
    public class LDAPAuthProvider : IADAuthProvider
    {
        protected readonly ILogger<LDAPAuthProvider> _logger;

        private readonly ActiveDirectorySettings _activeDirectorySettings;
        public LDAPAuthProvider(ILogger<LDAPAuthProvider> logger, IOptions<ActiveDirectorySettings> activeDirectorySettings)
        {
            _logger = logger;
            _activeDirectorySettings = activeDirectorySettings.Value;
        }

        public async Task<bool> Authenticate(ADLoginDto model)
        {
            var username = model.UserName;
            var password = model.Password;
            var domain = _activeDirectorySettings.Directory.Domain;
            var ldapPath = _activeDirectorySettings.Directory.Path;
            bool validateAD = _activeDirectorySettings.DomainValidation.Enable;
            var adPwd = _activeDirectorySettings.DefaultPassword;
            string errMsg = string.Empty;
            _logger.LogInformation("LDAPAuthProvider : DirectoryDomain - " + domain + " | DirectoryPath - " + ldapPath);
            string userNameWithDomain = $"{domain}\\{username}";
            try
            {
                _logger.LogInformation("LDAPAuthProvider : ValidateAD - " + validateAD);
                if (validateAD)
                {
                    //_logger.LogInformation("LDAPAuthProvider : LdapConnection - Start" );
                    //using (var ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(ldapPath)))
                    //{
                    //    // Set credentials and authenticate using bind
                    //    _logger.LogInformation("LDAPAuthProvider : UserNameWithDomain - " + userNameWithDomain + " | Password - " + password);
                    //    var credentials = new NetworkCredential(userNameWithDomain, password);
                    //    ldapConnection.Credential = credentials;
                    //    //ldapConnection.AuthType = AuthType.Basic;
                    //    //ldapConnection.AuthType = AuthType.Negotiate;
                    //    //ldapConnection.AuthType = AuthType.Anonymous;
                    //    ldapConnection.AuthType = AuthType.Kerberos;
                    //    ldapConnection.Timeout = new TimeSpan(0, 0, 30);
                    //    ldapConnection.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback((con, cer) => false);

                    //    // Bind to the server to force authentication
                    //    _logger.LogInformation("LDAPAuthProvider : Authentication User");
                    //    ldapConnection.Bind(); // If this succeeds, the credentials are correct

                    //    _logger.LogInformation("LDAPAuthProvider : LdapConnection - End");
                    //    return true; // Authentication successful
                    //}
                    //---------------------------------------------------------
                    _logger.LogInformation("LDAPAuthProvider : PrincipalContext - Start");
                    using (var domainContext = new PrincipalContext(ContextType.Domain, domain, username, password))
                    {
                        _logger.LogInformation("LDAPAuthProvider : PrincipalContext - Initialized");
                        using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, username))
                        {
                            if (foundUser != null)
                            {
                                _logger.LogInformation("LDAPAuthProvider : User Found - " + foundUser?.Name);
                                return true;
                            }
                            else
                            {
                                _logger.LogInformation("LDAPAuthProvider : User Not Found");
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return password == adPwd ? true : false;
                }
            }
            catch (LdapException ex)
            {
                errMsg = $"LDAP Exception: {ex?.Message}";
                _logger.LogError("LDAPAuthProvider : LdapException - " + ex + ex?.Message + ex?.StackTrace + ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace);
                _logger.LogError("LDAPAuthProvider : ServerErrorMessage - " + ex?.ServerErrorMessage);
                _logger.LogError("LDAPAuthProvider : ErrorCode - " + ex?.ErrorCode);
                return false;
            }
            catch (Exception ex)
            {
                errMsg = $"General Exception: {ex?.Message}";
                _logger.LogError("LDAPAuthProvider : Exception - " + ex + ex?.Message + ex?.StackTrace + ex?.InnerException + ex?.InnerException?.Message + ex?.InnerException?.StackTrace);
                return false;
            }
        }
    }
}