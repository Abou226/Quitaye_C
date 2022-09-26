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

[assembly: Dependency(typeof(DataService<Marque>))]
[assembly: Dependency(typeof(DataService<Test>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]


namespace Quitaye.ViewModels
{
    public class CategorieViewModel : BaseViewModel
    {
        public ICommand AddImageCommand { get; }
        public ICommand AddCommand { get; set; }
        public IDataService<Test> Test { get; }
        public Entreprise Entreprise { get; set; }
        public IMessage MessageAlert { get; }
        public IInitialService Init { get; }
        public ISessionService SessionService { get; }
        public IDataService<Categorie> DataService { get; }
        public ObservableCollection<Categorie> Items { get; }
        public FileResult FileResult { get; set; }
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
        public ICommand DeleteCommand { get; }
        public CategorieViewModel(Entreprise entreprise)
        {
            Entreprise = entreprise;
            Title = "Catégories";
            BaseVM = DependencyService.Get<IBaseViewModel>();
            DataService = DependencyService.Get<IDataService<Categorie>>();
            Items = new ObservableCollection<Categorie>();
            Init = DependencyService.Get<IInitialService>();
            MessageAlert = DependencyService.Get<IMessage>();
            DeleteCommand = new Command(OnDeleteCommand);
            AddImageCommand = new Command(OnAddImageCommand);
            AddCommand = new Command(OnAddCommand);
            SessionService = DependencyService.Get<ISessionService>();
            Test = DependencyService.Get<IDataService<Test>>();
            GetCategorieAsync(true);
        }

        private async Task GetCategorieAsync(bool showDialog)
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
                        var items = await DataService.GetItemsAsync(await SessionService.GetToken(), "Categories/"+Entreprise.Id.ToString());
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
                        await Initialize(ex, GetCategorieAsync(showDialog));
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
                var result = await Init.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                await SecureStorage.SetAsync("Token", result.Token);
                await SecureStorage.SetAsync("Prenom", result.Prenom);
                await SecureStorage.SetAsync("Nom", result.Nom);
                await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
                await action;
            }
            else if (ex.Message.Contains("host"))
            {
                await action;
            }
            else MessageAlert.LongAlert("Erreur" + ex.Message);
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
            if (!string.IsNullOrWhiteSpace(Nom)
                && !string.IsNullOrWhiteSpace(Description)
                && !string.IsNullOrWhiteSpace(FileResult.FileName))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;
                    if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                    {
                        Stream = await FileResult.OpenReadAsync();
                        Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                    }
                    else Image = null;

                    

                    var result = await DataService.AddFormDataAsync(new Categorie()
                    {
                        Name = Nom,
                        EntrepriseId = Entreprise.Id,
                        Description = Description,
                        Image = Image,
                        Url = "d",
                    }, await SecureStorage.GetAsync("Token"), "single");
                    BaseVM.RefreshGamme = true;
                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetCategorieAsync(false);

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
                    var data = (Categorie)obj;
                    var item = await DataService.DeleteAsync(await SessionService.GetToken(), (Categorie)obj, "categories/"+data.Id.ToString());
                    if (item != null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Element supprimer avec succès.");
                        await GetCategorieAsync(true);
                    }
                    else await Application.Current.MainPage.DisplayAlert("Erreur", "Element non supprimer", "Ok");
                }
            }
        }
    }
}
