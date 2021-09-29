using Services.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Quitaye.ViewModels
{
    public class InitialViewModel : BaseVM.BaseViewModel
    {
        private IAuthService _authService;

        public InitialViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
        }
    }
}
