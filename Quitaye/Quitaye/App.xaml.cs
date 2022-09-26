using BaseVM;
using Models;
using Plugin.Connectivity;
using Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Microsoft.Extensions.DependencyInjection;
using Quitaye.Views;
using Plugin.FirebasePushNotification;
using Quitaye.Services;

[assembly: Dependency(typeof(BaseViewModel))]
//
[assembly: Dependency(typeof(CheckInternetService<Test>))]
namespace Quitaye
{
    public partial class App : Application
    {
        public IBaseViewModel BaseVM { get; }
        public IDataService<Test> Test { get; }
        public ISessionService SessionService { get; }
        public App()
        {
            InitializeComponent();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            Test = DependencyService.Get<IDataService<Test>>();
            SessionService = DependencyService.Get<ISessionService>();
            Device.StartTimer(TimeSpan.FromSeconds(BaseVM.InternetCheckTime), () =>
            {
                // Do something
                CheckConnection();
                return true; // True = Repeat again, False = Stop the timer
            });
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Resources["Primary"] = Resources["ThirdColor"];
                Resources["ThirdColor"] = Resources["Primary"];
            }
            else if (currentTheme == OSAppTheme.Light)
            {
                Resources["Primary"] = Resources["Primary"];
                Resources["ThirdColor"] = Resources["ThirdColor"];
            }
            //CrossFirebasePushNotification.Current.Subscribe("all");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
            //CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
        }

        private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            
        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Token: {e.Token}");
        }
        private async void CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                do
                {
                    try
                    {
                        var result = await Test.GetItemsAsync(await SessionService.GetToken(), "Tests");
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

        protected override async void OnStart()
        {
            //await Host.StartAsync();
            //SecureStorage.RemoveAll();
            //await CheckConnection();
            var token = await SecureStorage.GetAsync("Token");
            if (string.IsNullOrWhiteSpace(token))
            {
                MainPage = new NavigationPage(new SocialLoginPage());
            }
            else
            {
                var last = await SecureStorage.GetAsync("LastEntreprise");
                if (!string.IsNullOrWhiteSpace(last))
                {
                    MainPage = new NavigationPage(new HomePage());
                }
                else MainPage = new NavigationPage(new InitialPage());
            }
        }

        protected override void OnSleep()
        {
            //Task.Run(async () => await Host.SleepAsync());
        }

        protected override void OnResume()
        {
            //Task.Run(async () => await Host.ResumeAsync()); 

        }
    }
}