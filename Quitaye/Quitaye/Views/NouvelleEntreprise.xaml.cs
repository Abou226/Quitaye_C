﻿using Quitaye.ViewModels;
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
    public partial class NouvelleEntreprise : ContentPage
    {
        public NouvelleEntreprise()
        {
            InitializeComponent();
            BindingContext = new NouvelleEntrepriseViewModel(this.Navigation);
        }
    }
}