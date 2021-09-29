using System;
using System.Threading.Tasks;

namespace Services.Auth
{
    public interface IAuthService
    {
        String getAuthKey();
        Task<bool> IsUserSigned();
        Task<bool> SignUp(string email, string password);
        Task<bool> SignIn(string email, string password);
        void SignInWithGoogle();
        Task<bool> SignInWithGoogle(string token);
        Task<bool> Logout();
    }
}
