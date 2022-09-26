using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Gms.Auth.Api.SignIn;
using Xamarin.Forms;
using Android.Content;
using Firebase.Auth;
using Plugin.GoogleClient;
using Android.Gms.Auth.Api;
using Plugin.FacebookClient;
using Firebase;
using Acr.UserDialogs;
using Java.Security;
using Xamarin.Essentials;
using Quitaye.Views;
using Models;
using Quitaye.Services;
using Quitaye.Droid.Services;
using BaseVM;
using Services;

[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(DataService<Entreprise>))]
[assembly: Dependency(typeof(DataService<User>))]
[assembly: Dependency(typeof(DataService<RefreshToken>))]
[assembly: Dependency(typeof(DataService<Secrets>))]
[assembly: Dependency(typeof(SessionService))]

namespace Quitaye.Droid
{
    [Activity(Label = "Quitaye", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FacebookClientManager.Initialize(this);
            GoogleClientManager.Initialize(this);
            UserDialogs.Init(() => this);
            LoadApplication(new App());
#if DEBUG
            PrintHashKey(this);
#endif
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
            //GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);
            GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(intent);
            if (result != null)
            {
                if (result.IsSuccess)
                {
                    
                    var last = await DependencyService.Get<ISessionService>().GetLastEntrepriseId();
                    if (!string.IsNullOrWhiteSpace(last))
                    {
                        var session = await DependencyService.Get<ISessionService>().GetNewTokenByEmail(result.SignInAccount.Email);
                       
                        App.Current.MainPage = new NavigationPage(new HomePage());
                    }
                    else
                    { 
                        var users = new System.Collections.Generic.List<User>();
                        users.Add(new User()
                        {
                            Email = result.SignInAccount.Email,
                            Prenom = result.SignInAccount.GivenName,
                            Nom = result.SignInAccount.FamilyName,
                        }) ;

                        var session = await DependencyService.Get<ISessionService>().GetNewTokenByEmail(result.SignInAccount.Email);
                        if (session == null)
                        {
                            var user = await DependencyService.Get<IDataService<User>>().AddListAsync(users, null);
                            if(user != null)
                            {
                                await DependencyService.Get<ISessionService>().GetNewTokenByEmail(result.SignInAccount.Email);
                               
                                App.Current.MainPage = new NavigationPage(new InitialPage());
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new InitialPage());
                        }
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Activity result is {0}", resultCode));
                }
            }
        }

        public static void PrintHashKey(Context pContext)
        {
            try
            {
                PackageInfo info = Android.App.Application.Context.PackageManager.GetPackageInfo(Android.App.Application.Context.PackageName, PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());
                    var sha = Convert.ToBase64String(md.Digest());
                    System.Diagnostics.Debug.WriteLine(sha);
                }
            }
            catch (NoSuchAlgorithmException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}