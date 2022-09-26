using Acr.UserDialogs;
using BaseVM;
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

[assembly: Dependency(typeof(DataService<Reservation>))]
[assembly: Dependency(typeof(DataService<Livraison>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]
[assembly: Dependency(typeof(DataService<Heure>))]

namespace Quitaye
{
    public class RapportReservationViewModel : BaseVM.BaseViewModel
    {
        public IBaseViewModel BaseVM { get; }
        public INavigation Navigation { get; }
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

        private int _quantité;

        public int Quantité
        {
            get { return _quantité; }
            set
            {
                if (_quantité == value)
                    return;

                _quantité = value;
                OnPropertyChanged();
            }
        }
        public IInitialService Initial { get; }

        private DateTime _end = DateTime.Today;
        public DateTime End
        {
            get => _end;
            set
            {
                if (_end == value)
                    return;

                _end = value;
                OnPropertyChanged();
            }
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText == value)
                    return;

                searchText = value;
                OnPropertyChanged();
                OnSearchCommand(searchText);
            }
        }

        public IDataService<EditObject> EditService { get; }

        private DateTime _start = DateTime.Today;
        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start == value)
                    return;

                _start = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Reservation> Items { get; }
        public ObservableCollection<ReservationGroupedDays> ItemsDays { get; private set; } = new ObservableCollection<ReservationGroupedDays>();
        public ObservableCollection<ReservationGroupedDays> ItemsDaysInitial { get; private set; } = new ObservableCollection<ReservationGroupedDays>();
        private decimal _total;

        public decimal Total
        {
            get => _total;
            set
            {
                if (_total == value)
                    return;
                _total = value;
                OnPropertyChanged();
            }
        }

        public IDataService<Reservation> ClientService { get; }

        public ICommand BuyCommand { get; }
        public ICommand AnnuléeCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand RefreshCommand { get; }

        public ICommand SearchCommand { get; }

        public ObservableCollection<Reservation> InitialItems { get; }

        public RapportReservationViewModel()
        {
            Title = "Commandes";
            Items = new ObservableCollection<Reservation>();
            InitialItems = new ObservableCollection<Reservation>();
            RefreshCommand = new Command(OnRefreshCommand);
            BaseVM = DependencyService.Get<IBaseViewModel>();
            BackCommand = new Command(OnBackCommand);
            EditService = DependencyService.Get<IDataService<EditObject>>();
            ClientService = DependencyService.Get<IDataService<Reservation>>();
            AnnuléeCommand = new Command(OnAnnuléeCommand);
            SearchCommand = new Command(OnSearchCommand);
            Initial = DependencyService.Get<IInitialService>();
            IsNotBusy = false;
        }

        private void OnSearchCommand(object obj)
        {
            var search = ((string)obj);
            if (!string.IsNullOrWhiteSpace(search))
            {
                if (Items.Count() != 0)
                {
                    var items = (from d in Items
                                 where d.Quartier.Name.Contains(search)
                                 || d.Autres_Info.Contains(search)
                                 || d.Client.Nom.Contains(search)
                                 || d.Client.Prenom.Contains(search)
                                 || d.Contact_Livraison.Contains(search)
                                 || d.Prix_Vente_Unité.ToString().Equals(search)
                                 || d.Designation.Contains(search)
                                 select d).ToList();
                    //if (items.Count() != 0)
                    {
                        Items.Clear();
                        Total = 0;
                        Quantité = items.Count();
                        foreach (var item in items)
                        {
                            Total += item.Prix_Vente_Unité;
                            Items.Add(item);
                        }

                        ItemsDays.Clear();
                        var grous = from d in Items group d by d.Date_Livraison.Date into gr select gr;
                        foreach (var item in grous)
                        {
                            var fsd = from d in Items where d.Date_Livraison.Date == item.Key.Date orderby d.DateOfCreation descending select d;
                            var list = new ObservableCollection<Reservation>();
                            foreach (var ites in fsd)
                            {
                                list.Add(ites);
                            }
                            ItemsDays.Add(new ReservationGroupedDays(item.Key.Date, Convert.ToDecimal(item.Sum(x => x.Prix_Vente_Unité)), list));
                        }
                    }
                }
            }
            else
            {
                Items.Clear();
                Total = 0;
                Quantité = InitialItems.Count();
                foreach (var item in InitialItems)
                {
                    Total += item.Prix_Vente_Unité;
                    Items.Add(item);
                }
                ItemsDays = ItemsDaysInitial;
            }
        }

        private async void OnRefreshCommand(object obj)
        {
            await GetItemsAsync();
        }

        public Entreprise Entreprise { get; set; }
        public RapportReservationViewModel(INavigation navigation, Entreprise entreprise) : this()
        {
            Navigation = navigation;
            Entreprise = entreprise;
            GetItemsAsync();
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
        }

        private async void OnAnnuléeCommand(object obj)
        {
            var result = await Application.Current.MainPage.DisplayAlert("Confirmation", "Voulez-vous vraiment annuler cette commande ?", "Oui", "Non");
            if (result)
            {
                await Annulée(((Reservation)obj));
            }
        }
        private async Task Annulée(Reservation reservation)
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    List<EditObject> list = new List<EditObject>();
                    list.Add(new EditObject() { Op = "Replace", Value = "True", Path = "Annulée" });
                    EditService.ProjectId = await SecureStorage.GetAsync("Source");
                    var item = await EditService.UpdateListAsync(list, await SecureStorage.GetAsync("Token"), "Reservations/" + reservation.Id.ToString() + "/" + Entreprise.Id.ToString());

                    if (item.Count() != 0)
                    {
                        IsNotBusy = true;
                        DependencyService.Get<IMessage>().LongAlert("Element annulé avec succès");
                        await GetItemsAsync();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Echec operation: {ex.Message}");
                    if (ex.Message.Contains("Unauthorize"))
                    {
                        var result = await Initial.Get(new LogInModel()
                        { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                        await GetItemsAsync();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        BaseVM.IsInternetOn = false;
                    }
                    else DependencyService.Get<IMessage>().ShortAlert("Erreur : " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async Task GetItemsAsync()
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Chargement....");
                    ClientService.ProjectId = await SecureStorage.GetAsync("Source");
                    var items = await ClientService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "reservations/" + Start.ToString("MM-dd-yyyy") + "/" + End.ToString("MM-dd-yyyy") + "/" + Entreprise.Id.ToString());
                    Items.Clear();
                    InitialItems.Clear();
                    Total = 0;
                    Quantité = items.Count();
                    if (items.Count() != 0)
                    {
                        foreach (var item in items)
                        {
                            Total += item.Prix_Vente_Unité;
                            if (item.Gamme.Url == null)
                                item.Gamme.Url = item.Gamme.Marque.Url;

                            string marque = "";
                            if (item.Gamme.Marque != null)
                                marque = item.Gamme.Marque.Name;
                            else if (item.Marque != null)
                                marque = item.Marque.Name;
                            item.Designation = marque + "-" + item.Gamme.Style.Name + ", " + item.Model.Name;
                            item.Autres_Info = item.Taille.Name + " parts, " + item.Model.Name;
                            Items.Add(item);
                            InitialItems.Add(item);
                        }

                        ItemsDays.Clear();
                        ItemsDaysInitial.Clear();
                        var grous = from d in Items group d by d.DateOfCreation.Date into gr select gr;
                        foreach (var item in grous)
                        {
                            var fsd = from d in Items where d.DateOfCreation.Date == item.Key.Date orderby d.DateOfCreation descending select d;
                            var list = new ObservableCollection<Reservation>();
                            foreach (var ites in fsd)
                            {
                                list.Add(ites);
                            }
                            ItemsDays.Add(new ReservationGroupedDays(item.Key.Date, Convert.ToDecimal(item.Sum(x => x.Prix_Vente_Unité)), list));
                            ItemsDaysInitial.Add(new ReservationGroupedDays(item.Key.Date, Convert.ToDecimal(item.Sum(x => x.Prix_Vente_Unité)), list));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Echec operation: {ex.Message}");
                    if (ex.Message.Contains("Unauthorize"))
                    {
                        var result = await Initial.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                        await GetItemsAsync();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        await GetItemsAsync();
                    }
                    else await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
                }
                finally
                {
                    IsRunning = false;
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("No internet", "Problème de connection internet, veillez verifier votre connection !", "OK");
            }
        }
    }
}