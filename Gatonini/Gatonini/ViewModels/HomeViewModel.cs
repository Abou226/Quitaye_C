using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BaseVM;
using Gatonini.Views;
using Models;
using Services;
using Xamarin.Essentials;
using Xamarin.Forms;

using Style = Models.Style;

[assembly: Dependency(typeof(DataService<object, Secrets>))]
[assembly: Dependency(typeof(DataService<RefreshToken>))]
[assembly: Dependency(typeof(DataService<Categorie>))]
[assembly: Dependency(typeof(DataService<Gamme>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]

namespace Gatonini.ViewModels
{
    public class HomeViewModel : BaseVM.BaseViewModel
    {
        private ICommand _logoutCommand;
        private IAuthService _authService;
        public IDataService<object, Secrets> Secret { get; }
        public ObservableCollection<Style> Styles { get; }
        public IDataService<Style> StyleServices { get; }
        public INavigation Navigation { get; }
        public IDataService<RefreshToken> Token { get; }
        private string _profilePic;

        public string ProfilePic
        {
            get { return _profilePic; }
            set 
            {
                if (_profilePic == value)
                    return;

                _profilePic = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            CatégorieTapped = new Command(OnCatégorieTapped);
            GammeTapped = new Command(OnGammeTapped);
            PanierCommand = new Command(OnPanierCommand);
            CategorieService = DependencyService.Get<IDataService<Categorie>>();
            GammeService = DependencyService.Get<IDataService<Gamme>>();
            StyleServices = DependencyService.Get<IDataService<Style>>();
            Styles = new ObservableCollection<Style>();
            Initial = DependencyService.Get<IInitialService>();
            VentesCommand = new Command(OnVenteCommand);
            LivraisonCommand = new Command(OnLivraisonCommand);
            BaseVM = DependencyService.Get<IBaseViewModel>();
            RefreshCommand = new Command(OnRefreshCommand);
            Secret = DependencyService.Get<IDataService<object, Secrets>>();
            PayementCommand = new Command(OnPayementCommand);
            ProfileCommand = new Command(OnProfileCommand);
            Catégories = new ObservableCollection<Categorie>();
            Gammes = new ObservableCollection<Gamme>();
            Token = DependencyService.Get<IDataService<RefreshToken>>();
            Init();
            GetCatégories();
        }


        async Task Init()
        {
            ProfilePic = await SecureStorage.GetAsync("ProfilePic");
        }
        //public ICommand LogoutCommand
        //{
        //    get { return _logoutCommand = _logoutCommand ?? new Command(LogoutCommandExecute); }
        //}

        //private async Task LogoutCommandExecute()
        //{
        //    if (await _authService.Logout())
        //    {
        //        await NavigationService.NavigateToAsync<LoginViewModel>();
        //    }
        //}

        public ICommand RefreshCommand { get; }
        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning == value)
                    return;
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        public bool FirstLunch { get; set; } = true;

        public ICommand CatégorieTapped { get; }
        public ICommand ProfileCommand { get; }
        public ICommand VentesCommand { get; }
        public ICommand GammeTapped { get; }
        public IBaseViewModel BaseVM { get; }
        public ICommand LivraisonCommand { get; }
        public IInitialService Initial { get; }
        public ICommand PanierCommand { get; }
        public ICommand PayementCommand { get; }
        public IDataService<Categorie> CategorieService { get; }
        public IDataService<Gamme> GammeService { get; }
        public ObservableCollection<Categorie> Catégories { get; }
        public ObservableCollection<Gamme> Gammes { get; }
        public HomeViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }

        private async void OnPayementCommand(object obj)
        {
            await Navigation.PushAsync(new RapportPayement());
        }

        private async void OnPanierCommand(object obj)
        {
            await Navigation.PushAsync(new PanierPage());
        }

        private async void OnLivraisonCommand(object obj)
        {
            await Navigation.PushAsync(new RapportLivraison());
        }

        private async void OnVenteCommand(object obj)
        {
            await Navigation.PushAsync(new VentePage());
        }

        private async void OnGammeTapped(object obj)
        {
            var de = ((Gamme)obj);
            await Navigation.PushAsync(new ProductDetail(de));
        }

        private async void OnProfileCommand(object obj)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void OnCatégorieTapped(object obj)
        {
            await GetProductsAsync((Categorie)obj);
        }

        private async Task GetProductsAsync(Categorie value)
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsRunning)
                    return;

                try
                {
                    IsRunning = true;
                    UserDialogs.Instance.ShowLoading("Chargement....");
                    var result = await GammeService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Gammes/" + value.Id);
                    Gammes.Clear();
                    if (result.Count() != 0)
                    {
                        Gammes.Clear();
                        foreach (var item in result)
                        {
                            if (item.Url == null)
                                item.Url = item.Marque.Url;
                            if (item.Prix_Unité == 1750)
                                item.Prix_Unité = 10000;
                            if (item.Categorie.Name == "Gâteau" || item.Categorie.Name == "Gateau")
                            {
                                item.Prix_Min = item.Prix_Unité * 6;
                                if (item.Prix_Min == 10500)
                                    item.Prix_Min = 10000;
                                else if (item.Prix_Min == 21000)
                                    item.Prix_Min = 20000;
                            }
                            Gammes.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Echec operation: {ex.Message}");
                    if (ex.Message.Contains("Unauthorize"))
                    {
                        var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                        IsRunning = false;
                        await SecureStorage.SetAsync("Token", result.Token);
                        await SecureStorage.SetAsync("Prenom", result.Prenom);
                        await SecureStorage.SetAsync("Nom", result.Nom);
                        await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                        await GetCatégories();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        BaseVM.IsInternetOn = false;
                    }
                    else await Application.Current.MainPage.DisplayAlert("Erreur", ex.Message, "Ok");
                }
                finally
                {
                    IsRunning = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async void OnRefreshCommand(object obj)
        {
            await GetCatégories();
        }

        private async Task GetStylesAsync()
        {
            if (BaseVM.IsInternetOn || FirstLunch)
            {
                if (IsRunning)
                    return;

                try
                {
                    IsRunning = true;

                    UserDialogs.Instance.ShowLoading("Chargement.....");
                    var token = await Token.PostAsync(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Username = "d", Password = "d" },
                        await SecureStorage.GetAsync("Token"), "auth/TokenCheck");
                    if (token != null)
                    {
                        var resul = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                        if (resul != null)
                        {
                            await SecureStorage.SetAsync("Token", resul.Token);
                        }
                    }
                    var result = await StyleServices.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Styles");
                    Styles.Clear();
                    if (result.Count() != 0)
                    {
                        foreach (var item in result)
                        {
                            Styles.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Echec operation: {ex.Message}");
                    if (ex.Message.Contains("Unauthorize"))
                    {
                        var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                        await SecureStorage.SetAsync("Token", result.Token);
                        await SecureStorage.SetAsync("Prenom", result.Prenom);
                        await SecureStorage.SetAsync("Nom", result.Nom);
                        await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                        IsRunning = false;
                        await GetCatégories();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        BaseVM.IsInternetOn = false;
                    }
                    else await Application.Current.MainPage.DisplayAlert("Erreur", ex.Message, "Ok");
                }
                finally
                {
                    IsRunning = false;
                    FirstLunch = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async Task GetCatégories()
        {
            if (BaseVM.IsInternetOn || FirstLunch)
            {
                if (IsRunning)
                    return;

                try
                {
                    IsRunning = true;

                    UserDialogs.Instance.ShowLoading("Chargement.....");
                    var token = await Token.PostAsync(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Username = "d", Password = "d" }, 
                        await SecureStorage.GetAsync("Token"), "auth/TokenCheck");
                    if (token != null)
                    {
                        var resul = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                        if (resul != null)
                        {
                            await SecureStorage.SetAsync("Token", resul.Token);
                        }
                    }
                    var result = await CategorieService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Categories");
                    Catégories.Clear();
                    if (result.Count() != 0)
                    {
                        foreach (var item in result)
                        {
                            Catégories.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Echec operation: {ex.Message}");
                    if (ex.Message.Contains("Unauthorize"))
                    {
                        var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                        await SecureStorage.SetAsync("Token", result.Token);
                        await SecureStorage.SetAsync("Prenom", result.Prenom);
                        await SecureStorage.SetAsync("Nom", result.Nom);
                        await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                        IsRunning = false;
                        await GetCatégories();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        BaseVM.IsInternetOn = false;
                    }
                    else await Application.Current.MainPage.DisplayAlert("Erreur", ex.Message, "Ok");
                }
                finally
                {
                    IsRunning = false;
                    FirstLunch = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }
    }
}
