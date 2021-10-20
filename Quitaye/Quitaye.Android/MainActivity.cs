using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Quitaye.Droid.Services;
using Android.Gms.Auth.Api.SignIn;
using Xamarin.Forms;
using Android.Content;
using Firebase.Auth;
using Firebase;
using Acr.UserDialogs;

namespace Quitaye.Droid
{
    [Activity(Label = "Quitaye", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;
        FirebaseAuth firebaseAuth;
        //ICallbackManager callback;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            firebaseAuth = InitFirebaseAuth();
            UserDialogs.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private FirebaseAuth InitFirebaseAuth()
        {
            //var app = FirebaseApp.InitializeApp(this);
            FirebaseAuth auth;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                .SetProjectId("quitaye-98974")
                .SetApplicationId("quitaye-98974")
                .SetApiKey("AIzaSyAvvMadOtRHN4eN_P1HHtOLSU_wtpW-s6E")
                .SetStorageBucket("quitaye-98974.appspot.com")
                .Build();

                //if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
                auth = FirebaseAuth.Instance;
            }
            else
            {
                auth = FirebaseAuth.Instance;
            }
            return auth;
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == AuthService.REQ_AUTH && resultCode == Result.Ok)
            {
                GoogleSignInAccount sg = (GoogleSignInAccount)data.GetParcelableExtra("result");
                MessagingCenter.Send(AuthService.KEY_AUTH, AuthService.KEY_AUTH, sg.IdToken);
            }
        }
    }
}