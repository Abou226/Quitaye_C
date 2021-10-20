using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BaseVM;
using Quitaye.Views;
using Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(Secure))]

namespace Quitaye.ViewModels
{
    public class SignUpViewModel : BaseVM.BaseViewModel
    {
        private ICommand _loginCommand;
        private ICommand _signupCommand;

        public ISecure Secure { get; }
        private string _username;
        public IMessage Toast { get; }
        public INavigation Navigation { get; }
        private string _password;
        public IBaseViewModel BaseVM { get; }
        private IUserDialogs _userDialogService;
        private ICommand _loginGoogleCommand;

        private IAuthService _authService;

        
        public SignUpViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }
        public SignUpViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            Secure = DependencyService.Get<ISecure>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            Toast = DependencyService.Get<IMessage>();
            MessagingCenter.Subscribe<string, string>(this, _authService.getAuthKey(), (sender, args) =>
            {
                LoginGoogle(args);
            });
        }

        public ICommand LoginGoogleCommand
        {
            get { return _loginGoogleCommand = _loginGoogleCommand ?? new Command(LoginGoogleCommandExecute); }
        }

        private async Task LoginGoogle(string token)
        {
            if (await _authService.SignInWithGoogle(token))
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
        }

        private void LoginGoogleCommandExecute(object obj)
        {
            _authService.SignInWithGoogle();
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new Command(LoginCommandExecute); }
        }
        public ICommand SignUpCommand
        {
            get { return _signupCommand = _signupCommand ?? new Command(SignUpCommandExecute); }
        }
        private async void LoginCommandExecute(object obj)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        private async void SignUpCommandExecute(object obj)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Confirm_Password))
                return;

            if ( Secure.Encryption(Password) != Secure.Encryption(Confirm_Password))
            {
                Toast.LongAlert("Mots de passe non conforme. Veillez confirmer le mot de passe");
                return;
            }
                

            if (await _authService.SignUp(Username, Password))
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                Toast.ShortAlert("Nom d'utilisateur ou mot de passe incorrect");
            }
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

        public string Confirm_Password
        {
            get
            {
                return _confirme_password;
            }
            set
            {
                _confirme_password = value;
                OnPropertyChanged();
            }
        }
    }
}
