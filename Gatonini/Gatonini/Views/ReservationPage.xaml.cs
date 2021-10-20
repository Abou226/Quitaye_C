using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gatonini.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentePage : ContentPage
    {
        public VentePage()
        {
            InitializeComponent();
            BindingContext = new VenteViewModel(this.Navigation);
        }
    }
}