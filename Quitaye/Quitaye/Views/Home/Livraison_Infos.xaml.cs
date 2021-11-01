using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quitaye.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Livraison_Infos : ContentPage
    {
        public Livraison_Infos()
        {
            InitializeComponent();
            Project = HomeViewModel.Project;
            if (Project == null)
                return;

            if (Project.Type.Type.Contains("production"))
                BindingContext = new RapportLivraisonViewModel(this.Navigation, Project);
        }

        public static readonly BindableProperty ProjectProperty =
            BindableProperty.Create(nameof(Project), typeof(Entreprise), typeof(Taille));

        public Entreprise Project
        {
            get { return (Entreprise)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }
    }
}