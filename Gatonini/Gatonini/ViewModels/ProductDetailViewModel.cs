using Acr.UserDialogs;
using BaseVM;
using Gatonini.Views;
using Models;
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

using Style = Models.Style;

[assembly: Dependency(typeof(DataService<Vente>))]
[assembly: Dependency(typeof(DataService<Num_Vente>))]
[assembly: Dependency(typeof(DataService<Panier>))]
[assembly: Dependency(typeof(DataService<Gamme>))]
[assembly: Dependency(typeof(DataService<Heure>))]
[assembly: Dependency(typeof(DataService<Model>))]
[assembly: Dependency(typeof(DataService<Taille>))]

namespace Gatonini
{
    public class ProductDetailViewModel : BaseVM.BaseViewModel
    {
        public Model Model { get; set; }
        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning == value)
                    return;
                _isRunning = value;
                OnPropertyChanged();
            }
        }
        public ICommand BackCommand { get; set; }

        public IMessage MessageAlert { get; }

        private decimal _prix_reel;

        public decimal Prix_Reel
        {
            get => _prix_reel;
            set
            {
                if (_prix_reel == value)
                    return;
                _prix_reel = value;
                OnPropertyChanged();

            }
        }

        private bool _usine;

        public bool Usine
        {
            get { return _usine; }
            set 
            {
                if (_usine == value)
                    return;

                _usine = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddItem { get; }
        private string _adresse_livraison;

        public string Adresse_Livraison
        {
            get => _adresse_livraison;
            set
            {
                if (_adresse_livraison == value)
                    return;
                _adresse_livraison = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date_livraison = DateTime.Today;

        public DateTime Date_Livraison
        {
            get => _date_livraison;
            set
            {
                if (_date_livraison == value)
                    return;

                _date_livraison = value;
                OnPropertyChanged();
                GetHeuresAsync();
            }
        }

            
        public IDataService<Num_Vente> Num_VenteService { get; }
        private string _mention;

        public string Mention
        {
            get => _mention;
            set
            {
                if (_mention == value)
                    return;

                _mention = value;
                OnPropertyChanged();
            }
        }

        private Heure heure;

        public Heure Heure
        {
            get => heure;
            set
            {
                if (heure == value)
                    return;
                heure = value;
                OnPropertyChanged();
            }
        }

        private bool isGateau;

        public bool IsGateau
        {
            get { return isGateau; }
            set 
            {
                if (isGateau == value)
                    return;
                isGateau = value;
                OnPropertyChanged();
            }
        }


        private Taille _taille;

        public Taille Taille
        {
            get => _taille;
            set
            {
                if (_taille == value)
                    return;

                _taille = value;
                OnPropertyChanged();
                if (!string.IsNullOrWhiteSpace(Taille.Name))
                {
                    if(Taille.Categorie.Name == "Gateau" || Taille.Categorie.Name == "Gâteau")
                    {

                        Prix_Reel = Gamme.Prix_Unité * Convert.ToDecimal(Taille.Name);
                        if (Prix_Reel == 10500)
                            Prix_Reel = 10000;
                        else if (Prix_Reel == 21000)
                            Prix_Reel = 20000;
                        OnPropertyChanged(nameof(Prix_Reel));
                    }
                    else
                    {
                        if (Prix_Reel == 10500)
                            Prix_Reel = 10000;
                        else if (Prix_Reel == 21000)
                            Prix_Reel = 20000;
                        Prix_Reel = Gamme.Prix_Unité * Convert.ToDecimal(Taille.Name);
                        OnPropertyChanged(nameof(Prix_Reel));
                    }
                }
                else
                {
                    Prix_Reel = 0;
                    OnPropertyChanged(nameof(Prix_Reel));
                }
            }
        }


        

        private string _contact_client;

        public string Contact_Client
        {
            get => _contact_client;
            set
            {
                if (_contact_client == value)
                    return;

                _contact_client = value;
                OnPropertyChanged();
            }
        }

        private string _contact_livraison;
        public string Contact_Livraison
        {
            get => _contact_livraison;
            set
            {
                if (_contact_livraison == value)
                    return;

                _contact_livraison = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; }
        public Gamme Gamme { get; set; }
        
        public Marque Marque { get; set; }
        public Style Style { get; set; }
        public ICommand CurrentItem { get; }
        public ObservableCollection<Gamme> Gammes { get; }
        public ObservableCollection<Taille> Tailles { get; }
        public ObservableCollection<Model> Models { get; }
        public IDataService<Gamme> GammeService { get; }
        public IDataService<Heure> HeureService { get; }
        public IDataService<Taille> TailleService { get; }
        public Command GammeChangedCommand { get; }
        public IDataService<Vente> VenteService { get; }
        public IDataService<Model> ModelService { get; }
        public IDataService<PanierVente> PanierVenteService { get; }
        public IInitialService Initial { get; }
        public ICommand BuyCommand { get; }
        public ICommand PanierCommand { get; }
        public IBaseViewModel BaseVM { get; }
        public ObservableCollection<Heure> Heures { get; }

        public ProductDetailViewModel(Gamme gamme, INavigation navigation) : this()
        {
            Gamme = gamme;
            Navigation = navigation;
            Title = Gamme.Marque.Name;
            Marque = Gamme.Marque;
            Style = Gamme.Style;
            if (Gamme.Categorie.Name == "Gateau" || Gamme.Categorie.Name == "Gâteau" 
                || Gamme.Categorie.Name == "Gateaux" || Gamme.Categorie.Name == "Gâteaux")
                IsGateau = true;
            GetProductsAsync(Gamme);
            IsNotBusy = false;
        }

        private Quartier _quartier;

        public Quartier Quartier
        {
            get { return _quartier; }
            set 
            {
                if (_quartier == value)
                    return;

                _quartier = value;
                OnPropertyChanged();
            }
        }


        private Offre _offre;

        public Offre Offre
        {
            get { return _offre; }
            set 
            {
                if (_offre == value)
                    return;

                _offre = value;
                OnPropertyChanged();
            }
        }

        private Client client;

        public Client Client_Vente
        {
            get { return client; }
            set 
            {
                if (client == value)
                    return;
                client = value;
                OnPropertyChanged();
            }
        }



        public ProductDetailViewModel()
        {
            Title = "Details Offres";
            BaseVM = DependencyService.Get<IBaseViewModel>();
            //Taille = new Taille();
            CurrentItem = new Command(OnCurrentItem);
            TailleService = DependencyService.Get<IDataService<Taille>>();
            HeureService = DependencyService.Get<IDataService<Heure>>();
            VenteService = DependencyService.Get<IDataService<Vente>>();
            Heures = new ObservableCollection<Heure>();
            Heures = new ObservableCollection<Heure>();
            GammeChangedCommand = new Command(OnGammeChangedCommand);
            Initial = DependencyService.Get<IInitialService>();
            PanierVenteService = DependencyService.Get<IDataService<PanierVente>>();
            Num_VenteService = DependencyService.Get<IDataService<Num_Vente>>();
            ModelService = DependencyService.Get<IDataService<Model>>();
            BuyCommand = new Command(OnBuyCommand);
            Gammes = new ObservableCollection<Gamme>();
            MessageAlert = DependencyService.Get<IMessage>();
            Models = new ObservableCollection<Model>();
            Tailles = new ObservableCollection<Taille>();
            GammeService = DependencyService.Get<IDataService<Gamme>>();
            BackCommand = new Command(OnBackCommand);
            AddItem = new Command(OnAddItem);
            PanierCommand = new Command(OnPanierCommand);
        }

        private void OnGammeChangedCommand(object obj)
        {
            if(obj != null)
            {
                Gamme = ((Gamme)obj);
                Taille = null;
                Marque = Gamme.Marque;
                Style = Gamme.Style;
            }
        }

        private void OnCurrentItem(object obj)
        {
            if (obj != null)
            {
                Gamme = ((Gamme)obj);
                Marque = Gamme.Marque;
                Style = Gamme.Style;
            }
        }

        private async void OnPanierCommand(object obj)
        {
            await Navigation.PushAsync(new PanierPage());
        }


        private async Task GetHeuresAsync()
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsRunning = true;
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Chargement.....");

                    var heures = await HeureService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Heures/" + Date_Livraison.ToString("MM-dd-yyyy"));
                    Heures.Clear();
                    if (heures.Count() != 0)
                    {
                        foreach (var item in heures)
                        {
                            Heures.Add(item);
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
                        await GetHeuresAsync();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        await GetHeuresAsync();
                    }
                    else MessageAlert.LongAlert("Erreur" + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
            else MessageAlert.ShortAlert("Veillez verifier votre connection internet");
        }
        private async Task GetProductsAsync(Gamme gamme)
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsRunning = true;
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Chargement.....");
                    
                    var items = await GammeService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Gammes/" + gamme.Categorie.Name + "/" + gamme.Marque.Name);
                    Gammes.Clear();
                    if (items.Count() != 0)
                    {
                        Marque = items.First().Marque;
                        Style = items.First().Style;
                        foreach (var item in items)
                        {
                            if (item.Url == null)
                                item.Url = item.Marque.Url;
                            //item.Designation = item.Marque.Name + "-" + item.Style.Name;
                            Gammes.Add(item);
                        }
                    }
                    var models = await ModelService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Models");
                    Models.Clear();
                    if (models.Count() != 0)
                    {
                        foreach (var item in models)
                        {
                            Models.Add(item);
                        }
                    }

                    var heures = await HeureService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Heures/" + Date_Livraison.ToString("MM-dd-yyyy"));
                    Heures.Clear();
                    if(heures.Count() != 0)
                    {
                        foreach (var item in heures)
                        {
                            Heures.Add(item);
                        }
                    }

                    if (IsGateau)
                    {
                        var tailles = await TailleService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Tailles");
                        Tailles.Clear();
                        if (tailles.Count() != 0)
                        {
                            foreach (var item in tailles)
                            {
                                Tailles.Add(item);
                            }
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
                        await GetProductsAsync(gamme);
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        await GetProductsAsync(gamme);
                    }
                    else MessageAlert.LongAlert("Erreur" + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
            else MessageAlert.ShortAlert("Veillez verifier votre connection internet");
        }

        private async void OnBuyCommand(object obj)
        {
            if (Prix_Reel != 0 && Taille != null && Model != null
                && Client_Vente != null && !string.IsNullOrWhiteSpace(Contact_Client)
                && !string.IsNullOrWhiteSpace(Adresse_Livraison) && Heure != null && !string.IsNullOrWhiteSpace(Contact_Livraison))
            {
                var valide = await Application.Current.MainPage.DisplayAlert("Confirmation", "Voulez-vous passer cette commande ?", "Oui", "Non");
                if (valide)
                {
                    if (BaseVM.IsInternetOn)
                    {
                        if (IsNotBusy)
                            return;

                        try
                        {
                            IsRunning = true;
                            IsNotBusy = false;
                            UserDialogs.Instance.ShowLoading("Validation");
                            //var num_vente = await Num_VenteService.GetItemAsync(await SecureStorage.GetAsync("Token"), "Num_Ventes/create");

                            //if (num_vente != null)
                            {
                                var item = await VenteService.AddAsync(new Vente()
                                {
                                    QuartierId = Quartier.Id,
                                    ClientId = Client_Vente.Id,
                                    Contact_Livraison = Contact_Livraison,
                                    Prix_Unité = Prix_Reel,
                                    Quantité = 1,
                                    Date_Livraison = Date_Livraison,
                                    Heure_Livraison = Heure.Name,
                                    EntrepriseId = Active_Entreprise.Instance.Id,
                                    Date = DateTime.Now,
                                }, await SecureStorage.GetAsync("Token"));
                                ClearData();
                                if (item != null)
                                    await Application.Current.MainPage.DisplayAlert("Confirmation", "Commande effectué avec succès", "Ok");
                            }

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Echec operation: {ex.Message}");
                            if (ex.Message.Contains("Unauthorize"))
                            {
                                var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                                await SecureStorage.SetAsync("Token", result.Token);
                                await SecureStorage.SetAsync("Prenom", result.Prenom);
                                await SecureStorage.SetAsync("Nom", result.Nom);
                                await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                                OnBuyCommand(obj);
                            }
                            else MessageAlert.LongAlert("Erreur" + ex.Message);
                        }
                        finally
                        {
                            IsRunning = false;
                            IsNotBusy = true;
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else MessageAlert.ShortAlert("Veillez verifier votre connection internet");
                }
            }
            else MessageAlert.ShortAlert("Veillez remplir tous les champs de données");
        }

        private void ClearData()
        {
            Adresse_Livraison = null;
            Contact_Livraison = null;
            Client_Vente = null;
            Contact_Client = null;
            Heure = null;
            Mention = null;
        }

        private async void OnAddItem(object obj)
        {
            if (!IsGateau)
            {
                Taille.Name = "d";
            }
            if (Prix_Reel != 0 && Taille != null && Model != null
                && Client_Vente != null && !string.IsNullOrWhiteSpace(Contact_Client)
                && !string.IsNullOrWhiteSpace(Adresse_Livraison) && Heure != null && !string.IsNullOrWhiteSpace(Contact_Livraison))
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsRunning = true;
                        IsNotBusy = false;
                        if (Taille.Name == "d")
                            Taille.Name = null;
                        UserDialogs.Instance.ShowLoading("Validation");
                        var item = await PanierVenteService.AddAsync(new PanierVente()
                        {
                            QuartierId = Quartier.Id,
                            Client = Client_Vente,
                            Contact_Livraison = Contact_Livraison,
                            Prix_Unité = Prix_Reel,
                            Quantité = 1,
                            Date_Livraison = Date_Livraison,
                            Heure_Livraison = Heure.Name,
                            OffreId = Offre.Id,
                        }, await SecureStorage.GetAsync("Token"));

                        ClearData();
                        if (item != null)
                            MessageAlert.ShortAlert("Element ajouté à votre panier avec succès.");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Echec operation: {ex.Message}");
                        if (ex.Message.Contains("Unauthorize"))
                        {
                            var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token") });
                            OnBuyCommand(obj);
                        }
                        else
                            MessageAlert.LongAlert("Erreur" + ex.Message);
                    }
                    finally
                    {
                        IsRunning = false;
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else MessageAlert.ShortAlert("Veillez verifier votre connection internet");
            }
            else MessageAlert.ShortAlert("Veillez remplir tous les champs de données");
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
        }
    }
}
