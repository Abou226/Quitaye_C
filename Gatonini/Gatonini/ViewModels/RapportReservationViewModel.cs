﻿using Acr.UserDialogs;
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
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]

namespace Gatonini
{
    public class RapportVenteViewModel : BaseVM.BaseViewModel
    {

        public IBaseViewModel BaseVM { get; }
        public ICommand RefreshCommand { get; }
        public INavigation Navigation { get; }
        public bool IsRunning { get; set; }
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

        private DateTime _start;
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
        public ObservableCollection<Reservation> Items { get; }
        public IDataService<Reservation> DataService { get; }

        public ICommand BackCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand AnnuléeCommand { get; }

        public RapportVenteViewModel()
        {
            Items = new ObservableCollection<Reservation>();
            BackCommand = new Command(OnBackCommand);
            BaseVM = DependencyService.Get<IBaseViewModel>();
            DataService = DependencyService.Get<IDataService<Reservation>>();
            AnnuléeCommand = new Command(OnAnnuléeCommand);
            Initial = DependencyService.Get<IInitialService>();
            RefreshCommand = new Command(OnRefreshCommand);
            IsNotBusy = false;
            GetItemsAsync();
        }

        public RapportVenteViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
        }

        private async void OnRefreshCommand(object obj)
        {
            await GetItemsAsync();
        }

        private void OnAnnuléeCommand(object obj)
        {
            if (BaseVM.IsInternetOn)
            {
                if (IsRunning)
                {
                    try
                    {
                        //var item = await PanierService.DeleteAsync()
                    }
                    catch (Exception e)
                    {

                    }
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
                    var items = await DataService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Ventes/" + Start.ToString("MM-dd-yyyy") + "/" + End.ToString("MM-dd-yyyy"));
                    if (items.Count() != 0)
                    {
                        Items.Clear();
                        Total = 0;
                        foreach (var item in items)
                        {
                            Total += item.Prix_Vente_Unité;
                            item.Designation = item.Offre.Gamme.Marque + "-" + item.Offre.Taille.Name + ", " + item.Offre.Model.Name;
                            if (item.Offre.Gamme.Url == null)
                                item.Offre.Gamme.Url = item.Offre.Gamme.Marque.Url;
                            Items.Add(item);
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
