using Acr.UserDialogs;
using BaseVM;
using Models;
using Quitaye.Services;
using Quitaye.Views;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService<Entreprise>))]
[assembly: Dependency(typeof(DataService<RefreshToken>))]
[assembly: Dependency(typeof(DataService<Secrets>))]

[assembly: Dependency(typeof(DataService<Ville>))]
[assembly: Dependency(typeof(DataService<Pays>))]
[assembly: Dependency(typeof(DataService<Type_Entreprise>))]
[assembly: Dependency(typeof(DataService<Ville>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]

namespace Quitaye.ViewModels
{
    public class NouvelleEntrepriseViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; }
        public ISessionService SessionService { get; }
        public IDataService<Type_Entreprise> TypeService { get;}
        public IDataService<Entreprise> EntrepriseService { get;}
        public IDataService<Ville> Villeservice { get; }
        public IDataService<Pays> PaysService { get; }
        public ObservableCollection<Type_Entreprise> Types { get; }
        public ObservableCollection<Pays> Pays { get; }
        private Pays _pay;

        public Pays Pay
        {
            get { return _pay; }
            set 
            {
                if (_pay == value)
                    return;
                _pay = value;
                OnPropertyChanged();
                GetVillesAsync(_pay.Id);
            }
        }

        public ObservableCollection<Ville> Villes { get; }
        public Quartier Quartier { get; set; }
        public string Nom_Entreprise { get; set; }
        public Type_Entreprise Type { get; set; }
        public Ville Ville { get; set; }
        public IInitialService Initial { get; }
        public string Nb_Employés { get; set; }
        public IBaseViewModel BaseVM { get; }
        public INavigation Navigation { get; }
        public ICommand PositionCommand { get; }
        public ICommand PaysTappedCommand { get; }
        public string Adresse { get; set; }
        public IMessage MessageAlert { get; }
        public bool IsRunning { get; set; }
        public IDataService<RefreshToken> Token { get; }
        public IDataService<Secrets> Secret { get; }

        public NouvelleEntrepriseViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }
        public NouvelleEntrepriseViewModel()
        {
            Types = new ObservableCollection<Type_Entreprise>();
            Token = DependencyService.Get<IDataService<RefreshToken>>();
            Secret = DependencyService.Get<IDataService<Secrets>>();
            Pays = new ObservableCollection<Pays>();
            Villes = new ObservableCollection<Ville>();
            PaysTappedCommand = new Command(OnPaysTappedCommand);
            MessageAlert = DependencyService.Get<IMessage>();
            EntrepriseService = DependencyService.Get<IDataService<Entreprise>>();
            TypeService = DependencyService.Get<IDataService<Type_Entreprise>>();
            Villeservice = DependencyService.Get<IDataService<Ville>>();
            PaysService = DependencyService.Get<IDataService<Pays>>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            PositionCommand = new Command(OnPositionCommand);
            Initial = DependencyService.Get<IInitialService>();
            SessionService = DependencyService.Get<ISessionService>();
            AddCommand = new Command(OnAddCommand);
            GetTypesAsync();
        }

        private async void OnPositionCommand(object obj)
        {
            //await GetAddresse();
        }

        private async Task TokenManagement()
        {
            if (await SessionService.HasTokenExpired())
            {
                if (await SessionService.IsSecureStorageCompatible())
                {
                    await SessionService.GetNewToken(await SecureStorage.GetAsync("Token"));
                }
                else
                {
                    await SessionService.GetNewToken(Preferences.Get("Token", "null"));
                }
            }
        }


        private async void OnPaysTappedCommand(object obj)
        {
            var pays = (Pays)obj;
            await GetVillesAsync(pays.Id);
        }

        //async Task GetAddresse()
        //{
        //    try
        //    {
        //        var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(20));
        //        CancellationTokenSource cts = new CancellationTokenSource();
        //        var location = await Geolocation.GetLocationAsync(request, cts.Token);

        //        if (location != null)
        //            await GetLocation(location);
        //        else
        //            await Application.Current.MainPage.DisplayAlert("Inconnue", "Adresse inconnue", "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //       await Application.Current.MainPage.DisplayAlert("Error occurred", ex.Message.ToString(), "Ok");
        //    }
        //}

        //public async Task GetLocation(Location location)
        //{
        //    try
        //    {
        //        var locationInfo = $"Latitude: {location.Latitude}\n" +
        //        $"Longitude: {location.Longitude}\n";

        //        //lblLocationInfo.Text = locationInfo;

        //        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
        //        var placemark = placemarks?.FirstOrDefault();

        //        if (placemark != null)
        //        {
        //            var geocodeAddress = "\n" +
        //            $"{ placemark.Thoroughfare}\n" + //Address
        //            $"{ placemark.SubLocality}\n" + //Address area name
        //            $"{placemark.Locality} {placemark.SubAdminArea}\n" + //CityName
        //            $"{placemark.PostalCode}\n" + //PostalCode
        //            $"{placemark.AdminArea}\n" + //StateName
        //            $"{placemark.CountryName}\n" + //CountryName
        //            $"CountryCode: {placemark.CountryCode}\n";

        //            Pay = new Pays() { Name = placemark.CountryName };
        //            Ville = new Ville() { Name = placemark.Locality +" "+placemark.SubAdminArea };
        //            Quartier = new Quartier() { Name = placemark.SubLocality };
        //            //lblLocationInfo.Text += geocodeAddress;
        //        }
        //        else
        //            await Application.Current.MainPage.DisplayAlert("Error occurred", "Unable to retreive address information", "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Error occurred", ex.Message.ToString(), "Ok");
        //    }
        //}

        private async void OnAddCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom_Entreprise) && !string.IsNullOrWhiteSpace(Adresse) 
                && Type != null && Pay != null && Ville != null 
                && !string.IsNullOrWhiteSpace(Nb_Employés))
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

                        var list = new List<Entreprise>();
                        list.Add(new Entreprise()
                        {
                            Name = Nom_Entreprise,
                            VilleId = Ville.Id,
                            Type_Id = Type.Id,
                            Quartier = new Quartier() { Name = Adresse },
                            Nb_Employés = Convert.ToInt32(Nb_Employés),
                            DateOfCreation = DateTime.Now,
                        });
                        var item = await EntrepriseService.AddListAsync(list, await SecureStorage.GetAsync("Token"));
                        ClearData();
                        if (item != null)
                        {
                            await SecureStorage.SetAsync("LastEntreprise", item.First().Id.ToString());
                            Application.Current.MainPage = new NavigationPage(new HomePage());
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
                            OnAddCommand(obj);
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

        private void ClearData()
        {
            Nom_Entreprise = null;
            Ville = null;
            Quartier = null;
            Nb_Employés = null;
        }

        async Task GetTypesAsync()
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

                    await TokenManagement();
                    var types = await TypeService.GetItemsAsync(await SessionService.GetToken(), "Type_Entreprises/");
                    Types.Clear();
                    if (types.Count() != 0)
                    {
                        foreach (var item in types)
                        {
                            Types.Add(item);
                        }
                    }
                    await GetPaysAsync();
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
                        await GetTypesAsync();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        await GetTypesAsync();
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
        }

        async Task GetPaysAsync()
        {
            if (BaseVM.IsInternetOn)
            {
                //if (IsNotBusy)
                //    return;

                try
                {
                    IsRunning = true;
                    IsNotBusy = false;
                    var pays = await PaysService.GetItemsAsync(await SessionService.GetToken(), "Pays/");
                    Pays.Clear();
                    if (pays.Count() != 0)
                    {
                        foreach (var item in pays)
                        {
                            Pays.Add(item);
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
                        await GetPaysAsync();
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        await GetPaysAsync();
                    }
                    else MessageAlert.LongAlert("Erreur" + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                    IsNotBusy = true;
                }
            }
        }

        async Task GetVillesAsync(Guid paysId)
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsRunning = true;
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Chargement....");
                    var villes = await Villeservice.GetItemsAsync(await SessionService.GetToken(), "Villes/pays/"+paysId.ToString());
                    Villes.Clear();
                    if (villes.Count() != 0)
                    {
                        foreach (var item in villes)
                        {
                            Villes.Add(item);
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
                        await GetVillesAsync(paysId);
                    }
                    else if (ex.Message.Contains("host"))
                    {
                        await GetVillesAsync(paysId);
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
        }
    }
}
