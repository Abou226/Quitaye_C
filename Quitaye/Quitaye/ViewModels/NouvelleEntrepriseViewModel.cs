using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Quitaye.ViewModels
{
    public class NouvelleEntrepriseViewModel
    {
        public ICommand AddCommand { get; }
        public INavigation Navigation { get; }
        public NouvelleEntrepriseViewModel()
        {

        }
    }
}
