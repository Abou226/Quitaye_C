﻿using Acr.UserDialogs;
using BaseVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Models;
using Plugin.Connectivity;
using Quitaye.Services;
using Quitaye.Views.Settings;
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

using Style = Models.Style;

[assembly: Dependency(typeof(DataService<Gamme>))]
[assembly: Dependency(typeof(DataService<Style>))]
[assembly: Dependency(typeof(DataService<Marque>))]
[assembly: Dependency(typeof(DataService<Categorie>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]

namespace Quitaye.ViewModels
{
    public class GammeViewModel : BaseViewModel
    {
        public IDataService<Marque> MarqueService { get; }
        public IDataService<Style> StyleService { get; }
        public IDataService<Test> Test { get; }
        public ISessionService SessionService { get; }
        public IBaseViewModel BaseVM { get; }
        public IDataService<Categorie> CategorieService { get; }
        public IMessage MessageAlert { get; }
        public IInitialService Init { get; set; }
        public ICommand AddImageCommand { get; }
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

        private async Task GetMarquesAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var items = await MarqueService.GetItemsAsync(await SessionService.GetToken(), "Marques/" + Entreprise.Id.ToString());
                        Marques.Clear();
                        if (items.Count() != 0)
                        {
                            foreach (var item in items)
                            {
                                Marques.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetMarquesAsync());
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

        private async Task RefreshList()
        {
            GetCategoriesAsync();
            GetMarquesAsync();
            GetStylesAsync(); 
        }

        private async Task Refresh()
        {
            if (BaseVM.RefreshGamme)
            {
                await RefreshList();
                BaseVM.RefreshGamme = false;
            }
        }


        private async Task GetCategoriesAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var items = await CategorieService.GetItemsAsync(await SessionService.GetToken(), "Categories/" + Entreprise.Id.ToString());
                        Categories.Clear();
                        if (items.Count() != 0)
                        {
                            foreach (var item in items)
                            {
                                Categories.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetCategoriesAsync());
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

        private async Task GetStylesAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var items = await StyleService.GetItemsAsync(await SessionService.GetToken(), "Styles/" + Entreprise.Id.ToString());
                        Styles.Clear();
                        if (items.Count() != 0)
                        {
                            foreach (var item in items)
                            {
                                Styles.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetStylesAsync());
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

        public ICommand AddCommand { get; }

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
        public IDataService<Gamme> DataService { get; }
        public ObservableCollection<Gamme> Items { get; }
        public Entreprise Entreprise { get; set; }
        public INavigation Navigation { get; }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }
        public GammeViewModel(INavigation navigation, Entreprise entreprise) 
        {
            Entreprise = entreprise;
            Navigation = navigation;
            Title = "Gammes";
            MarqueService = DependencyService.Get<IDataService<Marque>>();
            StyleService = DependencyService.Get<IDataService<Style>>();
            CategorieService = DependencyService.Get<IDataService<Categorie>>();
            DataService = DependencyService.Get<IDataService<Gamme>>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            MessageAlert = DependencyService.Get<IMessage>();
            Test = DependencyService.Get<IDataService<Test>>();
            Init = DependencyService.Get<IInitialService>();
            Marques = new ObservableCollection<Marque>();
            Styles = new ObservableCollection<Style>();
            Categories = new ObservableCollection<Categorie>();
            Items = new ObservableCollection<Gamme>();
            BackCommand = new Command(OnBackCommand);
            AddImageCommand = new Command(OnAddImageCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            AddCommand = new Command(OnAddCommand);
            SessionService = DependencyService.Get<ISessionService>();
            RefreshList();
            GetGammesAsync(true);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Refresh();
                return true;
            });
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
        }

        public GammeViewModel()
        {

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

        public ObservableCollection<Marque> Marques { get; }
        public ObservableCollection<Categorie> Categories { get; }
        public ObservableCollection<Style> Styles { get; }

        private decimal _prix_unité;

        public decimal Prix_Unité
        {
            get { return _prix_unité; }
            set 
            {
                if (_prix_unité == value)
                    return;

                _prix_unité = value;
                OnPropertyChanged();
            }
        }


        private Categorie categorie;

        public Categorie Categorie
        {
            get { return categorie; }
            set 
            {
                if (categorie == value)
                    return;

                categorie = value;
                OnPropertyChanged();
            }
        }


        private Marque marque;

        public Marque Marque
        {
            get { return marque; }
            set 
            {
                if (marque == value)
                    return;
                marque = value;
                OnPropertyChanged();
            }
        }

        private Style style;

        public Style Style
        {
            get { return style; }
            set
            {
                if (style == value)
                    return;

                style = value;
                OnPropertyChanged();
                if (style.Style_Special)
                    Style_Special = true;
                else Style_Special = false;
            }
        }

        private bool _style_special;

        public bool Style_Special
        {
            get { return _style_special; }
            set 
            {
                if (_style_special == value)
                    return;

                _style_special = value;
                OnPropertyChanged();
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
            if (Style_Special)
            {
                if (Categorie != null && Style != null && Prix_Unité != 0)
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

                        

                        var result = await DataService.AddFormDataAsync(new Gamme()
                        {
                            CategorieId = Categorie.Id,
                            StyleId = Style.Id,
                            Image = Image,
                            EntrepriseId = Entreprise.Id,
                            Prix_Unité = Prix_Unité,
                            Url = "d",
                        }, await SecureStorage.GetAsync("Token"), "single");

                        ClearData();
                        if (result == null)
                        {

                        }
                        UserDialogs.Instance.HideLoading();

                        await GetGammesAsync(false);

                        if (result != null)
                        {
                            BaseVM.RefreshProduit = true;
                            DependencyService.Get<IMessage>().ShortAlert("Opération effectuée avec succès !");
                        }
                    }
                    catch (Exception ex)
                    {
                        //await Initialize(ex, OnAddGammeCommand(obj));
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            else
            {
                if (Categorie != null && Marque != null && Style != null && Prix_Unité != 0)
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

                        var result = await DataService.AddFormDataAsync(new Gamme()
                        {
                            MarqueId = Marque.Id,
                            CategorieId = Categorie.Id,
                            StyleId = Style.Id,
                            Image = Image,
                            EntrepriseId = Entreprise.Id,
                            Prix_Unité = Prix_Unité,
                            Url = "d",
                        }, await SecureStorage.GetAsync("Token"), "single");

                        ClearData();
                        if (result == null)
                        {

                        }
                        UserDialogs.Instance.HideLoading();

                        await GetGammesAsync(false);

                        if (result != null)
                        {
                            BaseVM.RefreshProduit = true;
                            await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        //await Initialize(ex, OnAddGammeCommand(obj));
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
        }

        private async Task GetGammesAsync(bool showDialog)
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
                        var items = await DataService.GetItemsAsync(await SessionService.GetToken(), "Gammes/"+Entreprise.Id.ToString());
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
                        await Initialize(ex, GetGammesAsync(showDialog));
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
                    var data = (Gamme)obj;
                    var item = await DataService.DeleteAsync(await SessionService.GetToken(), (Gamme)obj, "gammes/"+data.Id.ToString());
                    if (item != null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Element supprimer avec succès.");
                        await GetGammesAsync(true);
                    }
                    else await Application.Current.MainPage.DisplayAlert("Erreur", "Element non supprimer", "Ok");
                }
            }
        }
    }
}
