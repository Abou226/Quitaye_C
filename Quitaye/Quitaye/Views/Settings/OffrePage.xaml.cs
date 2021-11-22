﻿using Models;
using Quitaye.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quitaye.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OffrePage : ContentPage
    {
        public OffrePage()
        {
            InitializeComponent();
            Project = HomeViewModel.Project;
            if (Project == null)
                return;
            BindingContext = new OffreViewModel(this.Navigation, Project);
        }

        public static readonly BindableProperty ProjectProperty =
            BindableProperty.Create(nameof(Project), typeof(Entreprise), typeof(Model));

        public Entreprise Project
        {
            get { return (Entreprise)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }
    }
}