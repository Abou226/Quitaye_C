using Models;
using Quitaye.Views;
using Quitaye.Views.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

using Mode = Quitaye.Views.Settings.Models;

namespace Quitaye.ViewModels
{
    public class ProductionSettingViewModel : BaseVM.BaseViewModel
    {
        public INavigation Navigation { get; }
        public ICommand SectionTapped { get; }
        public ObservableCollection<Section> Sections { get; }
        public ProductionSettingViewModel(INavigation navigation, Entreprise entreprise) : this()
        {
            Navigation = navigation;
            Entreprise = entreprise;
            Title = "Paramettre " + Entreprise.Name;
        }

        private Entreprise entreprise;
        public Entreprise Entreprise
        {
            get { return entreprise; }
            set
            {
                if (entreprise == value)
                    return;

                entreprise = value;
                OnPropertyChanged();
            }
        }

        public ICommand BackCommand { get; }
        public ProductionSettingViewModel()
        {
            SectionTapped = new Command(OnSectionTapped);
            BackCommand = new Command(OnBackCommand);
            Sections = new ObservableCollection<Section>();
            
            GetSections();
        }

        private async void OnBackCommand(object obj)
        {
            await Navigation.PopAsync();
        }

        private async void OnSectionTapped(object obj)
        {
            var section = (Section)obj;
            if(section.Nom == "Styles")
            {
                await Navigation.PushAsync(new Styles());
            }else if(section.Nom == "Marques")
            {
                await Navigation.PushAsync(new Marques());
            }else if(section.Nom == "Models")
            {
                await Navigation.PushAsync(new Mode());
            }else if(section.Nom == "Tailles")
            {
                await Navigation.PushAsync(new Tailles());
            }else if(section.Nom == "Categories")
            {
                await Navigation.PushAsync(new Categories());
            }else if(section.Nom == "Gammes")
            {
                await Navigation.PushAsync(new Gammes());
            }else if(section.Nom == "Matière Première")
            {
                await Navigation.PushAsync(new MatièrePremière());
            }else if(section.Nom == "Produit Fini")
            {
                await Navigation.PushAsync(new ProduitFini());
            }else if(section.Nom == "Heure")
            {
                await Navigation.PushAsync(new Heures());
            }
        }

        void GetSections()
        {
            Sections.Clear();
            Sections.Add(new Section()
            {
                Nom = "Styles",
                Icon = "sketch.png",
                Black_Icon = "sketch_black.png",
                CurrentIcon = "sketch_black.png",
                Description = "Les styles sont des spéficités qui peuvent accompagnés les produits"
            });
            Sections.Add(new Section()
            {
                Nom = "Marques",
                Icon = "brand.png",
                Black_Icon = "brand_black.png",
                CurrentIcon = "brand_black.png",
                Description = "Les marques sont les nom de marques, les appelations les plus simplifiées"
            });
            Sections.Add(new Section()
            {
                Nom = "Models",
                Icon = "model.png",
                Black_Icon = "model_black.png",
                CurrentIcon = "model_black.png",
                Description = "Les models sont les differents formes"
            });
            Sections.Add(new Section()
            {
                Nom = "Tailles",
                Icon = "size.png",
                Black_Icon = "size_black.png",
                CurrentIcon = "size_black.png",
                Description = "Les tailles sont différentes mesures de vos offres de produits."
            });
            Sections.Add(new Section()
            {
                Nom = "Categories",
                Icon = "category.png",
                Black_Icon = "category_black.png",
                CurrentIcon = "category_black.png",
                Description = "Donnez les différents catégories. Ex : Gateaux, Pizza etc.."
            });
            Sections.Add(new Section()
            {
                Nom = "Gammes",
                Icon = "box.png",
                Black_Icon = "box_black.png",
                CurrentIcon = "box_black.png",
                Description = "Les gammes sont une combinason des categories, styles et ou marques afin de constituer une offre",
            });

            Sections.Add(new Section()
            {
                Nom = "Matière Première",
                Icon = "natural_resources.png",
                Black_Icon = "natural_resources_black.png",
                CurrentIcon = "natural_resources_black.png",
                Description = "Donnez les renseignement sur vos matières premières"
            });
            Sections.Add(new Section()
            {
                Nom = "Produit Fini",
                Icon = "inventory.png",
                Black_Icon = "inventory_black.png",
                CurrentIcon = "inventory_black.png",
                Description = "Les produits finis sont une combinasison de Gammes, Tailles et de Model afin de constituer et gérer les inventaires"
            });
            Sections.Add(new Section()
            {
                Nom = "Heure",
                Icon = "time.png",
                Black_Icon = "time_black.png",
                CurrentIcon = "time_black.png",
                Description = "Les horaires de livraison si possible."
            });
        }
    }
}
