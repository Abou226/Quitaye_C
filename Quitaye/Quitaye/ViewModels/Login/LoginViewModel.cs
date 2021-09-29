﻿using System;
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

[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(DataService<Entreprise>))]

namespace Quitaye.ViewModels
{
    public class LoginViewModel : BaseVM.BaseViewModel
    {
        private ICommand _signUpCommand;
        private ICommand _loginCommand;
        private ICommand _loginGoogleCommand;
        private ICommand _eyeCommand;
        private ICommand _forgotPasswordCommand;

        public INavigation Navigation { get; }

        private string _username;
        private string _password;

        private IUserDialogs _userDialogService;

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

        public LoginViewModel(IUserDialogs userDialogsService)
        {
            _userDialogService = userDialogsService;

        }

        public LoginViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
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
            _authService.SignInWithGoogle();
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
            if (await _authService.SignIn(Username, Password))
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                _userDialogService.Toast("Nom d'utilisateur ou mot de passe incorrect !");
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



        private async Task LoginGoogle(string token)
        {
            if (await _authService.SignInWithGoogle(token))
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
        }
    }
}
