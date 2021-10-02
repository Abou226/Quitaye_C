using Services.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Quitaye.ViewModels
{
    public class InitialViewModel : BaseVM.BaseViewModel
    {
        private IAuthService _authService;

        public INavigation Navigation { get; }

        public InitialViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            Initial();
        }

        async Task Initial()
        {
            Prenom = await SecureStorage.GetAsync("Prenom");
            Nom_Famille = await SecureStorage.GetAsync("Nom");
        }
        public InitialViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }
    }
}
