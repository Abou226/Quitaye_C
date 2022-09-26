using Acr.UserDialogs;
using BaseVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Models;
using Plugin.Connectivity;
using Quitaye.Services;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService<Model>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]


namespace Quitaye.ViewModels
{
    public class ModelViewModel : BaseViewModel
    {
        public ICommand AddImageCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ISessionService SessionService { get; }
        public IDataService<Test> Test { get; }
        public Entreprise Entreprise { get; set; }
        public IMessage MessageAlert { get; }
        public IInitialService Init { get; }
        public IDataService<Model> DataService { get; }
        public ObservableCollection<Model> Items { get; }
        private FileResult fileResult;

        public FileResult FileResult
        {
            get { return fileResult; }
            set 
            {
                if (fileResult == value)
                    return;

                fileResult = value;
                OnPropertyChanged();
            }
        }

        private ImageSource imageSource;

        public ImageSource PictureSource
        {
            get { return imageSource; }
            set
            {
                if (imageSource == value)
                    return;
                imageSource = value;
                OnPropertyChanged();
            }
        }

        private Stream stream1;

        public Stream Stream
        {
            get { return stream1; }
            set
            {
                if (stream1 == value)
                    return;

                stream1 = value;
                OnPropertyChanged();
            }
        }

        public IBaseViewModel BaseVM { get; }
        public INavigation Navigation { get; }
        public ICommand BackCommand { get; }
        public ModelViewModel(INavigation navigation, Entreprise entreprise)
        {
            Entreprise = entreprise;
            Navigation = navigation;
            Title = "Model(s)";
            BaseVM = DependencyService.Get<IBaseViewModel>();
            DataService = DependencyService.Get<IDataService<Model>>();
            Items = new ObservableCollection<Model>();
            Init = DependencyService.Get<IInitialService>();
            MessageAlert = DependencyService.Get<IMessage>();
            AddImageCommand = new Command(OnAddImageCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            AddCommand = new Command(OnAddCommand);
            BackCommand = new Command(OnBackCommand);
            Test = DependencyService.Get<IDataService<Test>>();
            SessionService = DependencyService.Get<ISessionService>();
            Entreprise = entreprise;
            GetItemsAsync(true);
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
        }

        private async void OnAddImageCommand(object obj)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Veillez selectionner une Image"
            });

            if (result != null && !string.IsNullOrWhiteSpace(result.FileName))
            {
                FileResult = result;
                var stream = await result.OpenReadAsync();
                PictureSource = ImageSource.FromStream(() => stream);
            }
        }

        public IFormFile Image { get; set; }
        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set
            {
                if (_nom == value)
                    return;

                _nom = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;

                _description = value;
                OnPropertyChanged();
            }
        }

        public async void OnAddCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;

                    var list = new List<Model>();
                    list.Add(new Model()
                    {
                        Name = Nom,
                        EntrepriseId = Entreprise.Id,
                        Description = Description,
                    });
                    
                    var result = await DataService.AddListAsync(list, await SecureStorage.GetAsync("Token"));
                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetItemsAsync(false);

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        DependencyService.Get<IMessage>().ShortAlert("Opération effectuée avec succès !");
                        //await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
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

        private async Task GetItemsAsync(bool showDialog)
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    try
                    {
                        IsNotBusy = false;
                        if(showDialog)
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var items = await DataService.GetItemsAsync(await SessionService.GetToken(), "Models/"+Entreprise.Id.ToString());
                        Items.Clear();
                        if (items.Count() != 0)
                        {
                            foreach (var item in items)
                            {
                                Items.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetItemsAsync(showDialog));
                    }
                    finally
                    {
                        IsNotBusy = true;
                        if(showDialog)
                        UserDialogs.Instance.HideLoading();
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
            else MessageAlert.LongAlert("Erreur" + ex.Message);
        }


        private void ClearData()
        {
            Nom = null;
            Description = null;
            FileResult = null;
            PictureSource = null;
            FileResult = null;
        }

        public async void OnDeleteCommand(object obj)
        {
            if (BaseVM.IsInternetOn)
            {
                var result = await Application.Current.MainPage.DisplayAlert("Suppression", "Voulez-vous réelement supprimer cet élément ?", "Oui", "Non");
                if (result)
                {
                    var data = (Model)obj;
                    var item = await DataService.DeleteAsync(await SessionService.GetToken(), (Model)obj, "Models/"+ data.Id.ToString());
                    if (item != null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Element supprimer avec succès.");
                        await GetItemsAsync(true);
                    }
                    else await Application.Current.MainPage.DisplayAlert("Erreur", "Element non supprimer", "Ok");
                }
            }
        }

        private async Task DeleteAsync()
        {

        }
    }
}
