using Models;
using Quitaye.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quitaye.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductionSetting : ContentPage
    {
        public ProductionSetting(Entreprise entreprise)
        {
            InitializeComponent();
            BindingContext = new ProductionSettingViewModel(this.Navigation, entreprise);
        }
    }
}