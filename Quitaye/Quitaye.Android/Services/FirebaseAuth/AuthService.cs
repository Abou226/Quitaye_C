using System;
using System.Threading.Tasks;
using Quitaye.Droid.Services;
using Services.Auth;
using Xamarin.Forms;
using Firebase.Auth;
using Android.App;
using Android.Content;
using Quitaye.Droid.Activities;
using System.Net.Http;
using Xamarin.Essentials;
using Services;
using BaseVM;
using Newtonsoft.Json;
using Models;
using System.Net.Http.Headers;

[assembly: Dependency(typeof(AuthService))]
[assembly: Dependency(typeof(InitialService))]
[assembly: Dependency(typeof(DataService<User>))]
[assembly: Dependency(typeof(DataService<RefreshToken>))]

namespace Quitaye.Droid.Services
{
    public class AuthService : IAuthService
    {
        public IDataService<RefreshToken> Token { get; }
        public IDataService<User> User { get; }
        public IInitialService Initial { get; }
        public AuthService()
        {
            User = DependencyService.Get<IDataService<User>>();
            Initial = DependencyService.Get<IInitialService>();
            Token = DependencyService.Get<IDataService<RefreshToken>>();
        }
        public static int REQ_AUTH = 9999;
        public static String KEY_AUTH = "auth";
        public async Task<bool> IsUserSigned()
        {
            var tokens = await SecureStorage.GetAsync("Token");
            if (!string.IsNullOrWhiteSpace(tokens))
            {
                var token = await Token.GetItemsAsync(await SecureStorage.GetAsync("Token"), "auth/TokenCheck");
                if (token == null)
                {
                    var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                    if (result != null)
                    {
                        await SecureStorage.SetAsync("Token", result.Token);
                        await SecureStorage.SetAsync("AwsAccessKey", result.AwsAccessKey);
                        await SecureStorage.SetAsync("AwsSecretKey", result.AwsSecretKey);
                        await SecureStorage.SetAsync("BucketName", result.BucketName);
                        await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                        await SecureStorage.SetAsync("Prenom", result.Prenom);
                        await SecureStorage.SetAsync("Nom", result.Nom);
                        return true;
                    }
                    else return false;
                }
                else return true;
            }
            else return false;
        }
        public async Task<bool> SignUp(string email, string password)
        {
            try
            {
                var user = await User.AddAsync(new User() { Username = email, Password = password }, null);
                if (user != null)
                {
                    var resutl = await SignIn(email, password);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                HttpClient cls = new HttpClient();
                cls.BaseAddress = new Uri(BaseViewModel.baseurl);
                LogInModel user = new LogInModel();
                user.Email = email;
                user.Password = password;
                string jsons = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responses = await cls.PostAsync("api/auth/login", httpContent);
                if (responses.IsSuccessStatusCode)
                {
                    var result = ConvertSingle<Secrets>.FromJson(await responses.Content.ReadAsStringAsync());
                    await SecureStorage.SetAsync("Token", result.Token);
                    await SecureStorage.SetAsync("AwsAccessKey", result.AwsAccessKey);
                    await SecureStorage.SetAsync("AwsSecretKey", result.AwsSecretKey);
                    await SecureStorage.SetAsync("BucketName", result.BucketName);
                    await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                    await SecureStorage.SetAsync("Prenom", result.Prenom);
                    await SecureStorage.SetAsync("Nom", result.Nom);
                }
                //await Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SignInWithGoogle()
        {
            var googleIntent = new Intent(Forms.Context, typeof(GoogleLoginActivity));
            ((Activity)Forms.Context).StartActivityForResult(googleIntent, REQ_AUTH);
        }

        public async Task<bool> SignInWithGoogle(string token)
        {
            try
            {
                AuthCredential credential = GoogleAuthProvider.GetCredential(token, null);
                await Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignInWithCredentialAsync(credential);
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
        public async Task<bool> Logout()
        {
            try
            {
                 Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string getAuthKey()
        {
            return KEY_AUTH;
        }
    }
}
