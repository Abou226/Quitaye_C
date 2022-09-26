using Acr.UserDialogs;
using BaseVM;
using Models;
using Plugin.Connectivity;
using Quitaye.Services;
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

[assembly: Dependency(typeof(DataService<EntrepriseUser>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]


namespace Quitaye.ViewModels
{
    public class UsersViewModel : BaseVM.BaseViewModel
    {
        public IBaseViewModel BaseVM { get; }
        public ICommand InviteUser { get; }
        public ICommand UserDetailsCommand { get; }
        public IInitialService Init { get; }
        public ISessionService SessionService { get; }
        private string email;

        public string Email
        {
            get { return email; }
            set 
            {
                if (email == value)
                    return;

                email = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EntrepriseUser> Users { get; }
        public IDataService<Test> Test { get; }
        public INavigation Navigation { get; }
        public IDataService<EntrepriseUser> UserService { get; }
        public UsersViewModel(INavigation navigation, Entreprise entreprise): this()
        {
            Navigation = navigation;
            Entreprise = entreprise;
        }

        public UsersViewModel()
        {
            InviteUser = new Command(OnInviteUser);
            Test = DependencyService.Get<IDataService<Test>>();
            Users = new ObservableCollection<EntrepriseUser>();
            Init = DependencyService.Get<IInitialService>();
            SessionService = DependencyService.Get<ISessionService>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            UserDetailsCommand = new Command(OnUserDetailCommand);
            UserService = DependencyService.Get<IDataService<EntrepriseUser>>();
        }

        private void OnUserDetailCommand(object obj)
        {
            
        }

        public Entreprise Entreprise { get; set; }

        private async void OnInviteUser(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Email))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;

                    var list = new List<EntrepriseUser>();
                    list.Add(new EntrepriseUser()
                    {
                        User = new User() { Email = Email },
                        EntrepriseId = Entreprise.Id,
                    });

                    var result = await UserService.AddListAsync(list, await SecureStorage.GetAsync("Token"));
                    
                    if (result != null)
                    {
                        Email = null;
                    }
                    await GetUsersAsync();

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        DependencyService.Get<IMessage>().ShortAlert("Opération effectuée avec succès !");
                    }
                }
                catch (Exception ex)
                {
                    //await Initialize(ex, OnAddCommand(obj));
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async Task GetUsersAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {

                    try
                    {
                        IsNotBusy = false;
                        var items = await UserService.GetItemsAsync(await SessionService.GetToken(), "EntrepriseUsers/" + Entreprise.Id.ToString());
                        Users.Clear();
                        if (items.Count() != 0)
                        {
                            foreach (var item in items)
                            {
                                Users.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetUsersAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                    }
                }
            }
            while (!BaseVM.IsInternetOn);
        }

        private async Task CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                do
                {
                    try
                    {
                        var result = await Test.GetItemsAsync(await SessionService.GetToken(), "Tests");
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

        public async Task Initialize(Exception ex, Task action)
        {
            Debug.WriteLine($"Echec operation: {ex.Message}");
            if (ex.Message.Contains("Unauthorize"))
            {
                await SessionService.GetNewToken(await SessionService.GetToken());
                await action;
            }
            else if (ex.Message.Contains("host"))
            {
                await action;
            }
            //else DependencyService.Get<IMessage>().LongAlert("Erreur" + ex.Message);
        }

    }
}
