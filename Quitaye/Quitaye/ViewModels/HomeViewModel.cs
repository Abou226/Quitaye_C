using Models;
using Services;
using Services.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataService<Entreprise>))]

namespace Quitaye.ViewModels
{
    public class HomeViewModel : BaseVM.BaseViewModel
    {
        private ICommand _logoutCommand;
        private IAuthService _authService;

        public HomeViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
        }

    }
}