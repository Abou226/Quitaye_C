using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Quitaye.ViewModels;
using Xamarin.Forms;
using Quitaye.Views;
using BaseVM;
using Services.Auth;
using Quitaye.Views.Login;
using Services;
using Models;
using Xamarin.Essentials;
using System.Linq;

[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(DataService<Entreprise>))]
[assembly: Dependency(typeof(DataService<RefreshToken>))]
[assembly: Dependency(typeof(InitialService))]

namespace Quitaye.ViewModels
{
    public class LoginViewModel : BaseVM.BaseViewModel
    {
        private ICommand _signUpCommand;
        private ICommand _loginCommand;
        private ICommand _loginGoogleCommand;
        private ICommand _eyeCommand;
        private ICommand _forgotPasswordCommand;


        public IDataService<Entreprise> EntrepriseData { get; }
        public IDataService<RefreshToken> Token { get; }
        public INavigation Navigation { get; }

        public IInitialService Initial { get; }

        private string _username;
        private string _password;


        private IAuthService _authService;

        public IBaseViewModel BaseVM { get; }

        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                if (isPassword == value)
                    return;
                isPassword = value;
                OnPropertyChanged();
            }
        }

        private string eyeImage = "eye.png";
        public string EyeImage
        {
            get => eyeImage;
            set
            {
                if (eyeImage == value)
                    return;
                eyeImage = value;
                OnPropertyChanged();
            }
        }
        public Command ForgotPasswordTapped { get; }

        public LoginViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }

        public LoginViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            Token = DependencyService.Get<IDataService<RefreshToken>>();
            Initial = DependencyService.Get<IInitialService>();
            EntrepriseData = DependencyService.Get<IDataService<Entreprise>>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            MessagingCenter.Subscribe<string, string>(this, _authService.getAuthKey(), (sender, args) =>
            {
                LoginGoogle(args);
            });
        }
        public ICommand EyeCommand
        {
            get { return _eyeCommand = _eyeCommand ?? new Command(EyeCommandCommandExecute); }
        }

        private void EyeCommandCommandExecute(object obj)
        {
            if (IsPassword)
            {
                IsPassword = false;
                EyeImage = "invisible.png";
            }
            else
            {
                IsPassword = true;
                EyeImage = "eye.png";
            }
        }

        public ICommand SignUpCommand
        {
            get { return _signUpCommand = _signUpCommand ?? new Command(SignUpCommandExecute); }
        }

        private async void SignUpCommandExecute(object obj)
        {
            await Navigation.PushAsync(new SignUpView());
        }

        public ICommand LoginGoogleCommand
        {
            get { return _loginGoogleCommand = _loginGoogleCommand ?? new Command(LoginGoogleCommandExecute); }
        }

        private void LoginGoogleCommandExecute(object obj)
        {
            UserDialogs.Instance.ShowLoading("Chargement....");
            _authService.SignInWithGoogle();
            UserDialogs.Instance.HideLoading();
        }

        public ICommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new Command(ForgotPasswordCommandExecute); }
        }

        private void ForgotPasswordCommandExecute(object obj)
        {

        }

        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new Command(LoginCommandExecute); }
        }

        private async void LoginCommandExecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                return;

            if (await _authService.SignIn(Username, Password))
            {
                var last = await SecureStorage.GetAsync("LastEntreprise");
                if (!string.IsNullOrWhiteSpace(last))
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new InitialPage());
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Nom d'utilisateur où mot de passe incorrecte !");
            }
            UserDialogs.Instance.HideLoading();
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _confirme_password;

        private async Task LoginGoogle(string token)
        {
            if (await _authService.SignInWithGoogle(token))
            {
                var last = await SecureStorage.GetAsync("LastEntreprise");
                if (!string.IsNullOrWhiteSpace(last))
                {
                    Application.Current.MainPage = new NavigationPage( new HomePage());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new InitialPage());
                }
            }
        }
    }
}
