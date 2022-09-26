using Acr.UserDialogs;
using BaseVM;
using Models;
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



namespace Quitaye.ViewModels
{
    public abstract class ObjectViewModel<T> : BaseVM.BaseViewModel where T : class, new()
    {
        public ICommand AddCommand { get; }
        public bool IsRunning { get; set; }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }
        public ISessionService SessionService { get; }
        public IDataService<T> DataService { get; }
        public IFileUploadService FileUpload { get; }
        public ICommand RefreshCommand { get; }
        public IInitialService Init { get; }
        
        private ImageSource _pictureSource;
        public ImageSource PictureSource
        {
            get => _pictureSource;
            set
            {
                if (_pictureSource == value)
                    return;

                _pictureSource = value;
                OnPropertyChanged();
            }
        }
        public Stream Stream { get; set; }
        public FileResult FileResult { get; set; }
        public ObservableCollection<T> Items { get; }
        public IInitialService Initial { get; }
        public IBaseViewModel BaseVM { get; }
        public IMessage MessageAlert { get; }
        public Entreprise Entreprise { get; set; }
        public T Item { get; }
        public ObjectViewModel(Entreprise entreprise) : this()
        {
            Entreprise = entreprise;
            OnRefreshCommand(null);
        }

        public ObjectViewModel()
        {
            BaseVM = DependencyService.Get<IBaseViewModel>();
            FileUpload = DependencyService.Get<IFileUploadService>();
            RefreshCommand = new Command(OnRefreshCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            SessionService = DependencyService.Get<ISessionService>();
            Item = new T();
            MessageAlert = DependencyService.Get<IMessage>();
            Init = DependencyService.Get<IInitialService>();
            DataService = DependencyService.Get<IDataService<T>>();
            Initial = DependencyService.Get<IInitialService>();
            Items = new ObservableCollection<T>();
            AddCommand = new Command(OnAddCommand);
            
        }
        public abstract void OnDeleteCommand(object obj);

        
        public async void OnRefreshCommand(object obj)
        {
            if (BaseVM.IsInternetOn)
            {
                if (obj is string)
                {
                    var re = (string)obj;
                    if (re == "Refresh")
                    {
                        IsRunning = true;
                        UserDialogs.Instance.ShowLoading("Chargement");
                        await GetItems(Item);
                        UserDialogs.Instance.HideLoading();
                    }
                    IsRunning = false;
                }
                if (IsRunning)
                    return;

                try
                {
                    IsRunning = true;
                    UserDialogs.Instance.ShowLoading("Chargement");
                    await GetItems(Item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Echec operation: {ex.Message}");
                    if (ex.Message.Contains("Unauthorize"))
                    {
                        await Initialize(ex, GetItems(Item));
                    }
                    else OnRefreshCommand(obj);
                    //await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
                }
                finally
                {
                    IsRunning = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        public async Task GetItems(T value)
        {
            var t = value;
            string p = "";
            var ty = t.GetType();
            var ps = "";
            //if (ty.Contains('.'))
            {
                var d = t.GetType().ToString().Split('.');
                foreach (var item in d)
                {
                    ps = item;
                }
            }

            try
            {
                var list = await DataService.GetItemsAsync(await SessionService.GetToken(), ps + "s/" + Entreprise.Id.ToString());
                Items.Clear();
                if (list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await Initialize(ex, GetItems(Item));
            }
            finally
            {
                IsNotBusy = true;
                UserDialogs.Instance.HideLoading();
            }
        }

        public abstract void OnAddCommand(object obj);

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
    }
}
