using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using Android.Support.V7.App;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using System.Threading.Tasks;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Firebase.Auth;
using AndroidX.AppCompat.App;
using Xamarin.Forms;
using Services;
using Models;
using Xamarin.Essentials;
using BaseVM;
using Acr.UserDialogs.Infrastructure;
using Plugin.Connectivity;

[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(CheckInternetService<Test>))]

namespace Quitaye.Droid.Activities
{
    [Activity(Label = "Quitaye", Theme = "@style/MyTheme.Splash", Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        public IBaseViewModel BaseVM { get; }
        public IDataService<Test> Test { get; }
        public SplashActivity()
        {
            //Test = DependencyService.Get<IDataService<Test>>();
            //BaseVM = DependencyService.Get<IBaseViewModel>();
            //Device.StartTimer(TimeSpan.FromSeconds(BaseVM.InternetCheckTime), () =>
            //{
            //    // Do something
            //    //Task t =  new Task(CheckConnection());
            //    CheckConnection();
            //    return true; // True = Repeat again, False = Stop the timer
            //});
        }
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup();  });
            startupWork.Start();

            Task startupWorks = new Task(() => { CheckConnection(); });
            startupWorks.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            //await Task.Delay(8000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Android.App.Application.Context, typeof(MainActivity)));
        }

        private async Task CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                do
                {
                    try
                    {
                        var result = await Test.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Tests");
                        //if(result == null)
                        {
                            BaseVM.IsInternetOn = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("host"))
                        {
                            BaseVM.IsInternetOn = false;
                        }
                        else BaseVM.IsInternetOn = true;
                    }
                } while (!BaseVM.IsInternetOn);
            }
            else
            {
                BaseVM.IsInternetOn = false;
            }
        }
    }
}