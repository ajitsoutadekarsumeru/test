using ENTiger.ENCollect.ApplicationUsersModule;

namespace ENTiger.ENCollect
{
    public interface IADAuthProvider
    {
        Task<bool> Authenticate(ADLoginDto model);
    }
}