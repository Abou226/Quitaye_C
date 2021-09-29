using BaseVM;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HomeService : BaseViewModel
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public Uri ProfilePicture { get; set; }
        
        public ObservableCollection<Categorie> Catégories { get; }
        public ObservableCollection<Offre> Offres { get; }

        public HomeService()
        {
            Catégories = new ObservableCollection<Categorie>();
            Offres = new ObservableCollection<Offre>();
        }

        private async void OnCatégorieSelected(Categorie obj)
        {
            IsNotBusy = true;
        }

    }
}
