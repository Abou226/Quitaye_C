using Acr.UserDialogs;
using BaseVM;
using Microcharts;
using Models;
using Plugin.Connectivity;
using Plugin.FirebasePushNotification;
using Quitaye.Helpers;
using Quitaye.Views;
using Quitaye.Views.Home;
using Services;
using SkiaSharp;
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
[assembly: Dependency(typeof(DataService<ChartData>))]
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

        public bool HomeVisible
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

        public bool VenteVisible
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

        public bool PayementVisible
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

        public static Entreprise Project;

        private bool _livraisonTextVisible;

        public bool LivraisonVisible
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

        public bool ProductionVisible
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
                VenteVisible = true;
                HomeVisible = false;
                LivraisonVisible = false;
                ProductionVisible = false;
                PayementVisible = false;
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production_black.png";
            }else if(icon == "Home")
            {
                HomeImage = "home.png";
                VenteVisible = false;
                HomeVisible = true;
                LivraisonVisible = false;
                ProductionVisible = false;
                PayementVisible = false;
                SalesImage = "sales_black.png";
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production_black.png";
            }else if(icon == "Payement")
            {
                PayementImage = "hand.png";
                VenteVisible = false;
                HomeVisible = false;
                LivraisonVisible = false;
                ProductionVisible = false;
                PayementVisible = true;
                SalesImage = "sales_black.png";
                HomeImage = "home_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production_black.png";
            }else if(icon == "Production")
            {
                SalesImage = "sales_black.png";
                VenteVisible = false;
                HomeVisible = false;
                LivraisonVisible = false;
                ProductionVisible = true;
                PayementVisible = false;
                PayementImage = "hand_black.png";
                LivraisonImage = "shipped_black.png";
                ProductionImage = "production.png";
            }else if(icon == "Livraison")
            {
                HomeImage = "home_black.png";
                VenteVisible = false;
                HomeVisible = false;
                LivraisonVisible = true;
                ProductionVisible = false;
                PayementVisible = false;
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
        public ObservableCollection<ChartData> ChartDatas { get; }
        public IDataService<ChartData> ChartService { get; }

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

        private decimal _vente_progresse = 44;

        public decimal Vente_Progress
        {
            get { return _vente_progresse; }
            set { _vente_progresse = value; }
        }

        private bool firstLunch = true;

        public bool FirstLunch
        {
            get { return firstLunch; }
            set 
            {
                if (firstLunch == value)
                    return;
                firstLunch = value;
                OnPropertyChanged();
            }
        }


        private decimal _montant_Vente;

        public decimal Montant_Vente
        {
            get { return _montant_Vente; }
            set 
            {
                if (_montant_Vente == value)
                    return;

                _montant_Vente = value;
                OnPropertyChanged();
            }
        }

        
        private MultiLinesChart multiLinesChart;

        public MultiLinesChart MultiLinesChart
        {
            get { return multiLinesChart; }
            set 
            {
                if (multiLinesChart == value)
                    return;

                multiLinesChart = value;
                OnPropertyChanged();
            }
        }



        private MultiBarChart multiBarChart;

        public MultiBarChart MultiBarChart
        {
            get { return multiBarChart; }
            set 
            {
                if (multiBarChart == value)
                    return;

                multiBarChart = value;
                OnPropertyChanged();
            }
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
            HomeVisible = true;
            GetProjects();
            
            //InitData();
            //Task.Run(async () => await GetProjects());
            //Task.Run(async () => await InitData());
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

        private SKColor blueColor = SKColor.Parse("#09C");
        private SKColor redColor = SKColor.Parse("#CC0000");

        private async Task InitData()
        {
            if(Entreprise != null)
            {
                if (BaseVM.IsInternetOn)
                    return;

                var entries = new List<List<ChartEntry>>();
                var turnoverEntries = new List<ChartEntry>();
                var donutChartEntries = new List<ChartEntry>();
                var chargesEntries = new List<ChartEntry>();

                try
                {
                    var ventes = await ChartService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Reservations/interval/" + Entreprise.Id.ToString() + "/" + DateTime.Today.AddDays(-6).Date.ToString("MM-dd-yyyy") + "/" + DateTime.Today.Date.ToString("MM-dd-yyyy"));
                    foreach (var item in ventes)
                    {
                        turnoverEntries.Add(new ChartEntry((float)item.Montant)
                        {
                            Color = blueColor,
                            ValueLabel = $"{(float)item.Montant / 1000} k",
                            Label = item.Date.ToString("ddd")
                        });
                    }

                    var payements = await ChartService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Payements/interval/" + Entreprise.Id.ToString() + "/" + DateTime.Today.AddDays(-6).Date.ToString("MM-dd-yyyy") + "/" + DateTime.Today.Date.ToString("MM-dd-yyyy"));
                    foreach (var item in payements)
                    {
                        chargesEntries.Add(new ChartEntry((float)item.Montant)
                        {
                            Color = redColor,
                            ValueLabel = $"{(float)item.Montant / 1000} k",
                            Label = item.Date.ToString("ddd")
                        });
                    }

                    MultiLinesChart = new MultiLinesChart
                    {
                        MultiLineEntires = entries,
                        LabelTextSize = 20f,
                        LabelOrientation = Orientation.Horizontal,
                        LineAreaAlpha = 50,
                        PointAreaAlpha = 50,
                        LegendNames = new List<string> { "Vente(s)", "Payement(s)" },
                        IsAnimated = false
                    };

                    MultiBarChart = new MultiBarChart
                    {
                        MultiBarEntries = entries,
                        LabelTextSize = 20f,
                        LabelOrientation = Orientation.Horizontal,
                        PointAreaAlpha = 0,
                        LegendNames = new List<string> { "Vente(s)", "Payement(s)" },
                        IsAnimated = false
                    };
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async Task GetProjects()
        {
            await CheckConnection();
           
            if (BaseVM.IsInternetOn || FirstLunch)
            {

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
                        Project = entre.First();
                        CrossFirebasePushNotification.Current.Subscribe(Project.Id.ToString());
                        CrossFirebasePushNotification.Current.Subscribe($"{Project.Id.ToString()}_vente");
                        CrossFirebasePushNotification.Current.Subscribe($"{Project.Id.ToString()}_production");
                        CrossFirebasePushNotification.Current.Subscribe($"{Project.Id.ToString()}_livraison");
                        CrossFirebasePushNotification.Current.Subscribe($"{Project.Id.ToString()}_payement");
                        await InitData();
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
                        IsNotBusy = true;
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
                    FirstLunch = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private void OnProductionCommand(object obj)
        {
            ChangeIcon("Production");
            CurrentSection = "Production(s)";
        }

        private async void OnSalesCommand(object obj)
        {
            ChangeIcon("Sales");
            CurrentSection = "Vente(s)";
            await Navigation.PushAsync(new Ventes_Infos());
        }

        private async void OnSettingsCommand(object obj)
        {
            if(Entreprise != null)
            {
                if (Entreprise.Type.Type.Contains("production"))
                {
                    await Navigation.PushAsync(new ProductionSetting(Entreprise));
                }
            }
        }

        private void OnPayementCommand(object obj)
        {
            ChangeIcon("Payement");
            CurrentSection = "Payement(s)";
        }

        private async void OnLivraisonCommand(object obj)
        {
            ChangeIcon("Livraison");
            CurrentSection = "Livraison(s)";
            await Navigation.PushAsync(new Livraison_Infos());
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