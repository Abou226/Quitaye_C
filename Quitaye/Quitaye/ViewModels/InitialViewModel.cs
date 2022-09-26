using Acr.UserDialogs;
using BaseVM;
using Models;
using Plugin.Connectivity;
using Quitaye.Views;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService<Entreprise>))]
[assembly: Dependency(typeof(DataService<RefreshToken>))]
[assembly: Dependency(typeof(DataService<Secrets>))]
[assembly: Dependency(typeof(CheckInternetService<Test>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]

namespace Quitaye.ViewModels
{
    public class InitialViewModel : BaseVM.BaseViewModel
    {
        private IAuthService _authService;

        public INavigation Navigation { get; }
        public IMessage MessageAlert { get; }
        public ICommand AddCommand { get; set; }
        public IBaseViewModel BaseVM { get; }
        public IDataService<Test> Test { get; }

        public IInitialService Init { get; }

        private string _headerText = "Créer votre premier project";

        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                if (_headerText == value)
                    return;
                _headerText = value;
                OnPropertyChanged();
            }
        }


        private bool _makeHeaderVisible;

        public bool MakeHeaderVisible
        {
            get { return _makeHeaderVisible; }
            set
            {
                if (_makeHeaderVisible == value)
                    return;
                _makeHeaderVisible = value;
                OnPropertyChanged();
            }
        }

        private bool noEntreprise = true;

        public bool NoEntreprise
        {
            get { return noEntreprise; }
            set 
            {
                if (noEntreprise == value)
                    return;

                noEntreprise = value;
                OnPropertyChanged();
            }
        }


        private int _fontSize = 40;

        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize == value)
                    return;
                _fontSize = value;
                OnPropertyChanged();
            }
        }

        public ICommand EntrepriseTapped { get; }
        public IDataService<Entreprise> EntrepriseService { get; }
        public ObservableCollection<Entreprise> Entreprises { get; }
        public IDataService<RefreshToken> Token { get; }
        public IDataService<Secrets> Secret { get; }
        public InitialViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            Entreprises = new ObservableCollection<Entreprise>();
            EntrepriseTapped = new Command(OnEntrepriseTapped);
            EntrepriseService = DependencyService.Get<IDataService<Entreprise>>();
            Token = DependencyService.Get<IDataService<RefreshToken>>();
            Secret = DependencyService.Get<IDataService<Secrets>>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            Init = DependencyService.Get<IInitialService>();
            MessageAlert = DependencyService.Get<IMessage>();
            Test = DependencyService.Get<IDataService<Test>>();
            AddCommand = new Command(OnAddCommand);
            Device.StartTimer(TimeSpan.FromSeconds(BaseVM.InternetCheckTime), () =>
            {
                // Do something
                CheckConnection();
                return true; // True = Repeat again, False = Stop the timer
            });
            GetEntrepriseAsync();
            Initial();
        }

        private async void OnEntrepriseTapped(object obj)
        {
            try
            {
                var entreprise = (Entreprise)obj;
                await SecureStorage.SetAsync("LastEntreprise", entreprise.Id.ToString());
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            catch (Exception ex)
            {

            }
        }

        private async Task TokenManagement()
        {
            try
            {
                var tok = await SecureStorage.GetAsync("Token");
                if (!string.IsNullOrWhiteSpace(tok))
                {
                    var token = await Token.PostAsync(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Username = "d", Password = "d" },
                    await SecureStorage.GetAsync("Token"), "auth/TokenCheck");
                    if (token == null)
                    {
                        var resul = await Init.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                        if (resul != null)
                        {
                            await SecureStorage.SetAsync("Token", resul.Token);
                        }
                    }
                }
                else
                {
                    var token = await Secret.GetItemAsync(null, "auth/useremail/" + await SecureStorage.GetAsync("Email"));
                    if (token != null)
                    {
                        await SecureStorage.SetAsync("Token", token.Token);
                        await SecureStorage.SetAsync("Prenom", token.Prenom);
                        await SecureStorage.SetAsync("Nom", token.Nom);
                        await SecureStorage.SetAsync("ProfilePic", token.ProfilePic);
                    }
                }
            }
            catch (Exception ex)
            {

            }
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

        async Task GetEntrepriseAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");

                        await TokenManagement();
                        var pays = await EntrepriseService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Entreprises/");
                        Entreprises.Clear();
                        if (pays != null)
                        {
                            foreach (var item in pays)
                            {
                                Entreprises.Add(item);
                            }
                        }

                        if (Entreprises.Count() != 0)
                        {
                            MakeHeaderVisible = true;
                            NoEntreprise = true;
                            HeaderText = "Project(s) en cours...";
                            FontSize = 16;
                        }else
                        {
                            MakeHeaderVisible = true;
                            NoEntreprise = false;
                            HeaderText = "Créer votre premier project..";
                            FontSize = 40;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Echec operation: {ex.Message}");
                        if (ex.Message.Contains("Unauthorize"))
                        {
                            var result = await Init.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                            await SecureStorage.SetAsync("Token", result.Token);
                            await SecureStorage.SetAsync("Prenom", result.Prenom);
                            await SecureStorage.SetAsync("Nom", result.Nom);
                            await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                            await GetEntrepriseAsync();
                        }
                        else if (ex.Message.Contains("host"))
                        {
                            await GetEntrepriseAsync();
                        }
                        //else MessageAlert.LongAlert("Erreur" + ex.Message);
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }


        private async void OnAddCommand(object obj)
        {
            await Navigation.PushAsync(new NouvelleEntreprise());
        }

        async Task Initial()
        {
            Prenom = await SecureStorage.GetAsync("Prenom");
            Nom_Famille = await SecureStorage.GetAsync("Nom");
        }
        public InitialViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }
    }
}
