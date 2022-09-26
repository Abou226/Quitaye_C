using Models;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace BaseVM
{
    public class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {
        public const string baseurl = "https://quitaye.mahalfial.com/";

        public HttpClient httpClient;
        public HttpClient Client => httpClient ?? (httpClient = new HttpClient());

        private string emptyTitle;

        public string EmptyTitle
        {
            get { return emptyTitle; }
            set 
            {
                if (emptyTitle == value)
                    return;

                emptyTitle = value;
                OnPropertyChanged();
            }
        }

        private string startName;

        public string StartTitle
        {
            get { return startName; }
            set 
            {
                if (startName == value)
                    return;

                startName = value;
                OnPropertyChanged();
            }
        }

        private string endTitle;

        public string EndTitle
        {
            get { return endTitle; }
            set 
            {
                if (endTitle == value)
                    return;

                endTitle = value;
                OnPropertyChanged();
            }
        }

        private string emptyDescription;

        public string EmptyDescription
        {
            get { return emptyDescription; }
            set 
            {
                if (emptyDescription == value)
                    return;

                emptyDescription = value;
                OnPropertyChanged();
            }
        }

        private string entrepriseName;

        public string EntrepriseName
        {
            get { return entrepriseName; }
            set 
            {
                if (entrepriseName == value)
                    return;

                entrepriseName = value;
                OnPropertyChanged();
            }
        }

        private bool loginSucceeded;

        public bool LoginSucceeded
        {
            get { return loginSucceeded; }
            set 
            {
                if (loginSucceeded == value)
                    return;

                loginSucceeded = value;
                OnPropertyChanged();
            }
        }



        private Entreprise entreprise;

        public Entreprise Entreprise
        {
            get { return entreprise; }
            set 
            {
                if (entreprise == value)
                    return;

                entreprise = value;
                OnPropertyChanged();
            }
        }

        public BaseViewModel()
        {
            Client.BaseAddress = new Uri(baseurl);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        public string Prenom { get; set; }
        public string Nom_Famille { get; set; }

        string title = "Quitaye";
        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged();
            }
        }

        private bool isInternetOn;

        public bool IsInternetOn
        {
            get => isInternetOn;
            set
            {
                if (isInternetOn == value)
                    return;

                isInternetOn = value;
                OnPropertyChanged();
            }
        }

        private int _internetCheckTime = 1;

        public int InternetCheckTime
        {
            get => _internetCheckTime;
            set 
            {
                if (_internetCheckTime == value)
                    return;

                _internetCheckTime = value;
                OnPropertyChanged();
            }
        }

        private bool firstLunch;

        public bool FirstLunch
        {
            get { return firstLunch; }
            set 
            {
                if (firstLunch == value)
                    return;

                firstLunch = value;
                OnPropertyChanged();
            }
        }


        private static bool _staticNotBusy;

        public static bool StaticNotBusy
        {
            get { return _staticNotBusy; }
            set
            {
                _staticNotBusy = value;
            }
        }

        bool isNotBusy;
        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (StaticNotBusy == value || isNotBusy == value)
                    return;

                isNotBusy = StaticNotBusy;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private bool _messageVisibility;

        public bool MessageVisibility
        {
            get => _messageVisibility;
            set
            {
                if (_messageVisibility == value)
                    return;

                _messageVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                if (_message == value)
                    return;
                _message = value;
                OnPropertyChanged();
            }
        }


        //public bool IsAllBusy => !IsNotAllBusy;

        //bool isNotAllBusy;
        //public bool IsNotAllBusy
        //{
        //    get => isNotAllBusy;
        //    set
        //    {
        //        if (isNotAllBusy == value)
        //            return;
        //        isNotAllBusy = value;
        //        OnPropertyChanged();
        //        OnPropertyChanged(nameof(IsAllBusy));
        //    }
        //}

        public bool IsBusy => !IsNotBusy;
        private bool _refreshCatégorie;
        public bool RefreshCatégorie 
        { 
            get => _refreshCatégorie; 
            set { 
                if (_refreshCatégorie == value) 
                    return; 
                _refreshCatégorie = value; 
                OnPropertyChanged(); 
            } 
        }

        private bool _refreshTaille;
        public bool RefreshTaille 
        { 
            get => _refreshTaille;
            set 
            {
                if (_refreshTaille == value)
                    return;
                _refreshTaille = value;
                OnPropertyChanged();
            }
        }

        private bool _refreshModel;
        public bool RefreshModel 
        { 
            get => _refreshModel;
            set 
            {
                if (_refreshModel == value)
                    return;
                _refreshModel = value;

                OnPropertyChanged();
            }
        }

        private bool _refreshProduit;
        public bool RefreshProduit 
        { 
            get => _refreshProduit; 
            set
            {
                if (_refreshProduit == value)
                    return;
                _refreshProduit = value;
                OnPropertyChanged();
            } 
        }

        private bool _refreshMarque;
        public bool RefreshMarque 
        { 
            get => _refreshMarque;
            set 
            {
                if (_refreshMarque == value)
                    return;
                _refreshMarque = value;
                OnPropertyChanged();
            }
        }

        private bool _refreshGamme;

        public bool RefreshGamme
        {
            get { return _refreshGamme; }
            set 
            {
                if (_refreshGamme == value)
                    return;

                _refreshGamme = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
