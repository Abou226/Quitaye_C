﻿using Acr.UserDialogs;
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

[assembly: Dependency(typeof(DataService<Matière_Premiere>))]
[assembly: Dependency(typeof(DataService<Test>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]


namespace Quitaye.ViewModels
{
    public class MatierePremièreViewModel : BaseViewModel
    {
        public ICommand AddImageCommand { get; }
        public ICommand AddCommand { get; set; }
        public IDataService<Test> Test { get; }
        public Entreprise Entreprise { get; set; }
        public IMessage MessageAlert { get; }
        public IInitialService Init { get; }
        public IDataService<Matière_Premiere> DataService { get; }
        public ObservableCollection<Matière_Premiere> Items { get; }
        public FileResult FileResult { get; set; }

        private Unité unité;

        public Unité Unité
        {
            get { return unité; }
            set 
            {
                if (unité == value)
                    return;

                unité = value;
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
        public ISessionService SessionService { get; }
        public ICommand BackCommand { get; }
        public MatierePremièreViewModel(INavigation navigation, Entreprise entreprise)
        {
            Entreprise = entreprise;
            Navigation = navigation;
            BaseVM = DependencyService.Get<IBaseViewModel>();
            SessionService = DependencyService.Get<ISessionService>();
            DataService = DependencyService.Get<IDataService<Matière_Premiere>>();
            Items = new ObservableCollection<Matière_Premiere>();
            Init = DependencyService.Get<IInitialService>();
            MessageAlert = DependencyService.Get<IMessage>();
            AddImageCommand = new Command(OnAddImageCommand);
            BackCommand = new Command(OnBackCommand);
            AddCommand = new Command(OnAddCommand);
            Test = DependencyService.Get<IDataService<Test>>();
            GetItemsAsync(true);
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
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

                        var items = await DataService.GetItemsAsync(await SessionService.GetToken(), "Matiere_Premieres/" + Entreprise.Id.ToString());
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

                    var list = new List<Matière_Premiere>();
                    list.Add(new Matière_Premiere()
                    {
                        Name = Nom,
                        EntrepriseId = Entreprise.Id,
                        Unité = Unité,
                        Image = Image,
                        Url = "d",
                    });

                    var result = await DataService.AddFormDataAsync(list, await SecureStorage.GetAsync("Token"));

                    BaseVM.RefreshGamme = true;
                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetItemsAsync(false);

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
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

        private void ClearData()
        {
            Nom = null;
            Description = null;
            FileResult = null;
            PictureSource = null;
            FileResult = null;
        }

        public void OnDeleteCommand(object obj)
        {

        }
    }
}
