using Models;
using Services;
using Services.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService<Entreprise>))]

namespace Quitaye.ViewModels
{
    public class HomeViewModel : BaseVM.BaseViewModel
    {
        private IAuthService _authService;
        public string ProfilePic { get; set; }
        public INavigation Navigation { get; }

        public HomeViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            Initial();
        }

        public HomeViewModel(INavigation navigation) : this()
        {
            Navigation = navigation;
        }

        async void Initial()
        {
            ProfilePic = await SecureStorage.GetAsync("ProfilePic");
        }
    }
}