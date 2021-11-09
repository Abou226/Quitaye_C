﻿using Models;
using Quitaye.ViewModels;
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
    public partial class Production_Infos : ContentView
    {
        public Production_Infos()
        {
            InitializeComponent();
            Project = HomeViewModel.Project;
            if (Project == null)
                return;

            if(Project.Type.Type.Contains("production"))
            BindingContext = new ProductionViewModel(this.Navigation, Project);
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