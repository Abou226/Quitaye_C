﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;
using Android.App;
using Android.Content;
using Services;
using Gatonini.Droid.Services;
//using System.Net.Http;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using Android.Gms.Auth.Api.SignIn;
using System.Linq;
using System.Net.Http;
using Gatonini.Droid;
using BaseVM;
using Gatonini.Droid.Activities;

[assembly: Dependency(typeof(AuthService))]
[assembly: Dependency(typeof(InitialService))]
[assembly: Dependency(typeof(DataService<User>))]
[assembly: Dependency(typeof(DataService<object, Secrets>))]

[assembly: Dependency(typeof(DataService<RefreshToken>))]
namespace Gatonini.Droid.Services
{
    public class AuthService : IAuthService
    {
        public IDataService<RefreshToken> Token { get; }
        public IDataService<User> User { get; }
        public IDataService<object, Secrets> Secret { get; }
        public IInitialService Initial { get; }
        public AuthService()
        {
            User = DependencyService.Get<IDataService<User>>();
            Initial = DependencyService.Get<IInitialService>();
            Token = DependencyService.Get<IDataService<RefreshToken>>();
            Secret = DependencyService.Get<IDataService<object, Secrets>>();
        }
        public static int REQ_AUTH = 9999;
        public static string KEY_AUTH = "auth";
        
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

        public async Task<bool> SignUpGoogle(object account)
        {
            var google = (GoogleSignInResult)account;

            User user = new User();
            user.Email = google.SignInAccount.Email;
            user.Prenom = google.SignInAccount.GivenName;
            user.Nom = google.SignInAccount.FamilyName;
            user.PhotoUrl = google.SignInAccount.PhotoUrl.Scheme.ToString() + "" + google.SignInAccount.PhotoUrl.EncodedSchemeSpecificPart.ToString();

            var users = await User.AddAsync(user, null);
            if (users != null)
            {
                var token = await Secret.GetAsync(null, "auth/useremail/" + google.SignInAccount.Email);
                if (token != null)
                {
                    await SecureStorage.SetAsync("Token", token.First().Token);
                    await SecureStorage.SetAsync("AwsAccessKey", token.First().AwsAccessKey);
                    await SecureStorage.SetAsync("AwsSecretKey", token.First().AwsSecretKey);
                    await SecureStorage.SetAsync("BucketName", token.First().BucketName);
                }
            }
            else
            {
                var token = await Secret.GetAsync(null, "auth/useremail/" + google.SignInAccount.Email);
                if (token != null)
                {
                    await SecureStorage.SetAsync("Token", token.First().Token);
                    await SecureStorage.SetAsync("AwsAccessKey", token.First().AwsAccessKey);
                    await SecureStorage.SetAsync("AwsSecretKey", token.First().AwsSecretKey);
                    await SecureStorage.SetAsync("BucketName", token.First().BucketName);
                }
            }
            return true;
        }

        //async Task<bool> IsUserSigned()
        //{
        //    var user = Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).CurrentUser;
        //    return user != null;
        //}

        async Task<bool> IAuthService.IsUserSigned()
        {
            var user = Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).CurrentUser;
            return user != null;
        }
    }
}
