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
[assembly: Dependency(typeof(DataService<Test>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]

namespace Quitaye
{
    public class HomeViewModel : BaseVM.BaseViewModel
    {
        private IAuthService _authService;
        public string ProfilePic { get; set; }
        public INavigation Navigation { get; }
        public IMessage MessageAlert { get; }
        private int _activeGrid;

        public int ActiveGrid
        {
            get { return _activeGrid; }
            set 
            {
                if (_activeGrid == value)
                    return;

                _activeGrid = value;
                OnPropertyChanged();
            }
        }

        private bool _homeTextVisible;

        public bool HomeTextVisible
        {
            get { return _homeTextVisible; }
            set 
            {
                if (_homeTextVisible == value)
                    return;
                _homeTextVisible = value;
                OnPropertyChanged();
            }
        }


        private bool _salesTextVisible;

        public bool SalesTextVisible
        {
            get { return _salesTextVisible; }
            set 
            {
                if (_salesTextVisible == value)
                    return;
                _salesTextVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _payementTextVisible;

        public bool PayementTextVisible
        {
            get { return _payementTextVisible; }
            set 
            {
                if (_payementTextVisible == value)
                    return;

                _payementTextVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _livraisonTextVisible;

        public bool LivraisonTextVisible
        {
            get { return _livraisonTextVisible; }
            set 
            {
                if (_livraisonTextVisible == value)
                    return;

                _livraisonTextVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _productionTextVisible;

        public bool ProductionTextVisible
        {
            get { return _productionTextVisible; }
            set 
            {
                if (_productionTextVisible == value)
                    return;
                _productionTextVisible = value;
                OnPropertyChanged();
            }
        }

        private void ChangeIcon(string icon)
        {
            if(icon == "Sales")
            {
                SalesImage = "sales.png";
                HomeImage = "home_black.png";
                SalesTextVisible = true;
                HomeTextVisible = false;
                LivraisonTextVisible = false;
                ProductionTextVisible = false;
                PayementTextVisible = false;
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production_black.png";
            }else if(icon == "Home")
            {
                HomeImage = "home.png";
                SalesTextVisible = false;
                HomeTextVisible = true;
                LivraisonTextVisible = false;
                ProductionTextVisible = false;
                PayementTextVisible = false;
                SalesImage = "sales_black.png";
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production_black.png";
            }else if(icon == "Payement")
            {
                PayementImage = "hand.png";
                SalesTextVisible = false;
                HomeTextVisible = false;
                LivraisonTextVisible = false;
                ProductionTextVisible = false;
                PayementTextVisible = true;
                SalesImage = "sales_black.png";
                HomeImage = "home_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production_black.png";
            }else if(icon == "Production")
            {
                SalesImage = "sales_black.png";
                SalesTextVisible = false;
                HomeTextVisible = false;
                LivraisonTextVisible = false;
                ProductionTextVisible = true;
                PayementTextVisible = false;
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production.png";
            }else if(icon == "Livraison")
            {
                HomeImage = "home_black.png";
                SalesTextVisible = false;
                HomeTextVisible = false;
                LivraisonTextVisible = true;
                ProductionTextVisible = false;
                PayementTextVisible = false;
                SalesImage = "sales_black.png";
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped.png";
                ProductionImage = "production_black.png";
            }
        }
        public IBaseViewModel BaseVM { get; }
        public IInitialService Init { get;}

        public IDataService<Test> Test { get; }
        public IDataService<Entreprise> EntrepriseService { get; }

        #region Images Icons

        private string _currentSection = "Acceuil";

        public string CurrentSection
        {
            get { return _currentSection; }
            set
            {
                if (_currentSection == value)
                    return;
                _currentSection = value;
                OnPropertyChanged();
            }
        }


        private string _payementImage = "hand_black.png";

        public string PayementImage
        {
            get { return _payementImage; }
            set
            {
                if (_payementImage == value)
                    return;

                _payementImage = value;
                OnPropertyChanged();
            }
        }

        private string _salesImage = "sales_black.png";

        public string SalesImage
        {
            get { return _salesImage; }
            set
            {
                if (_salesImage == value)
                    return;

                _salesImage = value;
                OnPropertyChanged();
            }
        }

        private string _homeImage = "home.png";

        public string HomeImage
        {
            get { return _homeImage; }
            set 
            {
                if (_homeImage == value)
                    return;

                _homeImage = value;
                OnPropertyChanged();
            }
        }

        private string _livraisonImage = "shipped_black.png";

        public string LivraisonImage
        {
            get { return _livraisonImage; }
            set 
            {
                if (_livraisonImage == value)
                    return;

                _livraisonImage = value;
                OnPropertyChanged();
            }
        }

        private string _productionImage = "production_black";

        public string ProductionImage
        {
            get { return _productionImage; }
            set 
            {
                if (_productionImage == value)
                    return;
                _productionImage = value;
                OnPropertyChanged();
            }
        }

        private string _inventaireImage = "inventaire_black";

        public string InventaireImage
        {
            get { return _inventaireImage; }
            set 
            {
                if (_inventaireImage == value)
                    return;

                _inventaireImage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands Properties
        public ICommand SalesCommand { get; }
        public ICommand ProductionCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand PayementCommand { get; }
        public ICommand LivraisonCommand { get; }
        public ICommand HomeCommand { get; }

        public ICommand RefreshCommand { get; }
        #endregion

        private decimal _vente_progresse = 74;

        public decimal Vente_Progress
        {
            get { return _vente_progresse; }
            set { _vente_progresse = value; }
        }


        private Entreprise entreprise;

        public Entreprise Entreprise
        {
            get { return entreprise; }
            set 
            {
                if (entreprise == value)
                    return;

                entreprise = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Entreprise> Entreprises { get; }
        public HomeViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            HomeCommand = new Command(OnHomeCommand);
            LivraisonCommand = new Command(OnLivraisonCommand);
            PayementCommand = new Command(OnPayementCommand);
            RefreshCommand = new Command(OnRefreshCommand);
            Init = DependencyService.Get<IInitialService>();
            MessageAlert = DependencyService.Get<IMessage>();
            Test = DependencyService.Get<IDataService<Test>>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            EntrepriseService = DependencyService.Get<IDataService<Entreprise>>();
            Entreprises = new ObservableCollection<Entreprise>();
            SettingsCommand = new Command(OnSettingsCommand);
            ProductionCommand = new Command(OnProductionCommand);
            SalesCommand = new Command(OnSalesCommand);
            Initial();
            GetProjects();
        }

        private async void OnRefreshCommand(object obj)
        {
            await GetProjects();
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

        private async Task GetProjects()
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
                        var pays = await EntrepriseService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Entreprises/");
                        Entreprises.Clear();
                        if (pays.Count() != 0)
                        {
                            foreach (var item in pays)
                            {
                                Entreprises.Add(item);
                            }
                            var entrepriseId = await SecureStorage.GetAsync("LastEntreprise");
                            var entre = from d in pays where d.Id.ToString().Equals(entrepriseId) select d;
                            Entreprise = entre.First();
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
                            await GetProjects();
                        }
                        else if (ex.Message.Contains("host"))
                        {
                            await GetProjects();
                        }
                        else MessageAlert.LongAlert("Erreur" + ex.Message);
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } 
            while (!BaseVM.IsInternetOn);
            
        }

        private void OnProductionCommand(object obj)
        {
            ChangeIcon("Production");
            CurrentSection = "Production(s)";
        }

        private void OnSalesCommand(object obj)
        {
            ChangeIcon("Sales");
            CurrentSection = "Vente(s)";
        }

        private async void OnSettingsCommand(object obj)
        {
            if(Entreprise != null)
            await Navigation.PushAsync(new SettingPage(Entreprise));
        }

        private void OnPayementCommand(object obj)
        {
            ChangeIcon("Payement");
            CurrentSection = "Payement(s)";
        }

        private void OnLivraisonCommand(object obj)
        {
            ChangeIcon("Livraison");
            CurrentSection = "Livraison(s)";
        }

        private void OnHomeCommand(object obj)
        {
            ChangeIcon("Home");
            CurrentSection = "Acceuil";
        }

        public HomeViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }

        async void Initial()
        {
            ProfilePic = await SecureStorage.GetAsync("ProfilePic");
        }
    }
}