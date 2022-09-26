using Acr.UserDialogs;
using BaseVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Models;
using Plugin.Connectivity;
using Quitaye.Services;
using Quitaye.ViewModels;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using Style = Models.Style;

[assembly: Dependency(typeof(DataService<Entreprise>))]
[assembly: Dependency(typeof(DataService<Marque>))]
[assembly: Dependency(typeof(DataService<Model>))]
[assembly: Dependency(typeof(DataService<Style>))]
[assembly: Dependency(typeof(DataService<Matière_Premiere>))]
[assembly: Dependency(typeof(DataService<Produit_Fini>))]
[assembly: Dependency(typeof(DataService<Gamme>))]
[assembly: Dependency(typeof(DataService<Categorie>))]
[assembly: Dependency(typeof(DataService<Taille>))]

[assembly: Dependency(typeof(CheckInternetService<Test>))]
[assembly: Dependency(typeof(BaseViewModel))]
[assembly: Dependency(typeof(InitialService))]


namespace Quitaye
{
    public class SettingViewModel : BaseVM.BaseViewModel
    {
        public ISessionService SessionService { get; }
        public IBaseViewModel BaseVM { get; }
        public IDataService<Test> Test { get; }
        public IInitialService Init { get; }

        private bool _is_style = true;

        public bool IsStyle
        {
            get { return _is_style; }
            set 
            {
                if (_is_style == value)
                    return;
                _is_style = value;
                OnPropertyChanged();
            }
        }


        private ObjectViewModel<object> currentVM;

        public ObjectViewModel<object> CurrentVM
        {
            get { return currentVM; }
            set 
            {
                if (currentVM == value)
                    return;

                currentVM = value;
                OnPropertyChanged();
            }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set 
            {
                if (_nom == value)
                    return;
                _nom = value;
                OnPropertyChanged();
            }
        }

        private Section section1;

        public Section CurrentSection
        {
            get { return section1; }
            set 
            {
                if (section1 == value)
                    return;
                section1 = value;
                OnPropertyChanged();
            }
        }

        public IMessage MessageAlert { get; }
        private Entreprise entreprise;

        public INavigation Navigation { get; }

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

        public ICommand SectionTappedCommand { get; }
        public IFormFile Image { get; set; }

        
        public ObservableCollection<Section> Sections { get; }
        private ImageSource _pictureSource;
        public ImageSource PictureSource
        {
            get => _pictureSource;
            set
            {
                if (_pictureSource == value)
                    return;

                _pictureSource = value;
                OnPropertyChanged();
            }
        }
        

        private bool _isUsine;

        public bool IsUsine
        {
            get { return _isUsine; }
            set 
            {
                if (_isUsine == value)
                    return;

                _isUsine = value;
                OnPropertyChanged();
            }
        }


        private bool styleVisible = true;

        public bool StyleVisible
        {
            get { return styleVisible; }
            set 
            {
                if (styleVisible == value)
                    return;

                styleVisible = value;
                OnPropertyChanged();
            }
        }

        private bool categorieVisible = false;

        public bool CategorieVisible
        {
            get { return categorieVisible; }
            set 
            {
                if (categorieVisible == value)
                    return;

                categorieVisible = value;
                OnPropertyChanged();
            }
        }

        private bool marqueVisible = false;

        public bool MarqueVisible
        {
            get { return marqueVisible; }
            set 
            {
                if (marqueVisible == value)
                    return;

                marqueVisible = value;
                OnPropertyChanged();
            }
        }

        private bool matiere_premièreVisible = false;

        public bool Matière_PremiereVisible
        {
            get { return matiere_premièreVisible; }
            set 
            {
                if (matiere_premièreVisible == value)
                    return;

                matiere_premièreVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _produitFiniVisible = false;

        public bool ProduitFiniVisible
        {
            get { return _produitFiniVisible; }
            set 
            {
                if (_produitFiniVisible == value)
                    return;
                _produitFiniVisible = value;
                OnPropertyChanged();
            }
        }


        private bool tailleVisible = false;

        public bool TailleVisible
        {
            get { return tailleVisible; }
            set 
            {
                if (tailleVisible == value)
                    return;

                tailleVisible = value;
                OnPropertyChanged();
            }
        }

        private bool gammeVisible = false;

        public bool GammeVisible
        {
            get { return gammeVisible; }
            set 
            {
                if (gammeVisible == value)
                    return;

                gammeVisible = value;
                OnPropertyChanged();
            }
        }

        private bool heureVisible;

        public bool HeureVisible
        {
            get { return heureVisible; }
            set 
            {
                if (heureVisible == value)
                    return;
                heureVisible = value;
                OnPropertyChanged();
            }
        }


        private bool modelVisible = false;

        public bool ModelVisible
        {
            get { return modelVisible; }
            set 
            {
                if (modelVisible == value)
                    return;

                modelVisible = value;
                OnPropertyChanged();
            }
        }


        public FileResult FileResult { get; set; }

        public IDataService<Taille> TailleService { get; }

        public SettingViewModel()
        {
            Sections = new ObservableCollection<Section>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            SectionTappedCommand = new Command(OnSectionTappedCommand);
            TailleService = DependencyService.Get<IDataService<Taille>>();
            Test = DependencyService.Get<IDataService<Test>>();
            MessageAlert = DependencyService.Get<IMessage>();
            SessionService = DependencyService.Get<ISessionService>();
            Init = DependencyService.Get<IInitialService>();
            GetSections();
        }

        private void OnSectionTappedCommand(object obj)
        {
            var value = (Section)obj;
            CurrentSection = value;
            ChangeIcon(value);

            MakeVisible(value);
        }

        private void MakeVisible(Section value)
        {
            if (value.Nom == "Categories")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = true;
                TailleVisible = false;
                GammeVisible = false;
                HeureVisible = false;
                ModelVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Gammes")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = false;
                TailleVisible = false;
                ModelVisible = false;
                HeureVisible = false;
                GammeVisible = true;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Styles")
            {
                MarqueVisible = false;
                StyleVisible = true;
                HeureVisible = false;
                CategorieVisible = false;
                TailleVisible = false;
                ModelVisible = false;
                GammeVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Tailles")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = false;
                TailleVisible = true;
                ModelVisible = false;
                GammeVisible = false;
                HeureVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Marques")
            {
                MarqueVisible = true;
                StyleVisible = false;
                CategorieVisible = false;
                HeureVisible = false;
                TailleVisible = false;
                ModelVisible = false;
                GammeVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Models")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = false;
                TailleVisible = false;
                ModelVisible = true;
                HeureVisible = false;
                GammeVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Matière Première")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = false;
                TailleVisible = false;
                HeureVisible = false;
                GammeVisible = false;
                ModelVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = true;
            }
            else if (value.Nom == "Produit Fini")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = false;
                TailleVisible = false;
                GammeVisible = false;
                HeureVisible = false;
                ModelVisible = false;
                ProduitFiniVisible = true;
                Matière_PremiereVisible = false;
            }
            else if (value.Nom == "Heure")
            {
                MarqueVisible = false;
                StyleVisible = false;
                CategorieVisible = false;
                TailleVisible = false;
                GammeVisible = false;
                HeureVisible = true;
                ModelVisible = false;
                ProduitFiniVisible = false;
                Matière_PremiereVisible = false;
            }
        }

        async void ChangeIcon(Section section)
        {
            if(section.Nom == "Styles")
            {
                section.CurrentIcon = section.Icon;
                section.Color = "Secondary";
                foreach (var item in Sections.Where(x => x.Nom != "Styles"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }
            else if(section.Nom == "Marques")
            {
                section.CurrentIcon = section.Icon;
                section.Color = "Secondary";
                foreach (var item in Sections.Where(x => x.Nom != "Marques"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }
            else if(section.Nom == "Tailles")
            {
                section.CurrentIcon = section.Icon;
                section.Color = "Secondary";
                foreach (var item in Sections.Where(x => x.Nom != "Tailles"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }
            else if(section.Nom == "Models")
            {
                section.CurrentIcon = section.Icon;
                section.Color = "Secondary";
                foreach (var item in Sections.Where(x => x.Nom != "Models"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }
            else if(section.Nom == "Gammes")
            {
                section.CurrentIcon = section.Icon;
                section.Color = "Secondary";
                foreach (var item in Sections.Where(x => x.Nom != "Gammes"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }else if(section.Nom == "Categories")
            {
                section.CurrentIcon = section.Icon;
                section.Color = "Secondary";
                foreach (var item in Sections.Where(x => x.Nom != "Categories"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }
            else if(section.Nom == "Produit Fini")
            {
                section.CurrentIcon = section.Icon;
                foreach (var item in Sections.Where(x => x.Nom != "Produit Fini"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }else if(section.Nom == "Matière Première")
            {
                section.CurrentIcon = section.Icon;
                foreach (var item in Sections.Where(x => x.Nom != "Matière Première"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }else if(section.Nom == "Heure")
            {
                section.CurrentIcon = section.Icon;
                foreach (var item in Sections.Where(x => x.Nom != "Heure"))
                {
                    item.CurrentIcon = item.Black_Icon;
                    item.Color = "FourthColor";
                }
            }
        }
        void GetSections()
        {
            Sections.Add(new Section()
            {
                Nom = "Styles",
                Icon = "sketch.png",
                Black_Icon = "sketch_black.png",
                CurrentIcon = "sketch.png"
            });
            CurrentSection = new Section() {
                Nom = "Styles",
                Icon = "sketch.png",
                Black_Icon = "sketch_black.png",
                CurrentIcon = "sketch.png" };
            Sections.Add(new Section()
            {
                Nom = "Marques",
                Icon = "brand.png",
                Black_Icon = "brand_black.png",
                CurrentIcon = "brand_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Models",
                Icon = "model.png",
                Black_Icon = "model_black.png",
                CurrentIcon = "model_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Tailles",
                Icon = "size.png",
                Black_Icon = "size_black.png",
                CurrentIcon = "size_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Categories",
                Icon = "category.png",
                Black_Icon = "category_black.png",
                CurrentIcon = "category_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Gammes",
                Icon = "box.png",
                Black_Icon = "box_black.png",
                CurrentIcon = "box_black.png"
            });
            
            Sections.Add(new Section()
            {
                Nom = "Matière Première",
                Icon = "natural_resources.png",
                Black_Icon = "natural_resources_black.png",
                CurrentIcon = "natural_resources_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Produit Fini",
                Icon = "inventory.png",
                Black_Icon = "inventory_black.png",
                CurrentIcon = "inventory_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Heure",
                Icon = "time.png",
                Black_Icon = "time_black.png",
                CurrentIcon = "time_black.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Niveau",
                Icon = "level.png",
                Black_Icon = "level.png",
                CurrentIcon = "level.png"
            });
            Sections.Add(new Section()
            {
                Nom = "Occasion",
                Icon = "occasion.png",
                Black_Icon = "occasion.png",
                CurrentIcon = "occasion.png"
            });
            
        }

        public SettingViewModel(INavigation navigation, Entreprise entreprise) : this()
        {
            Navigation = navigation;
            Entreprise = entreprise;
            if (Entreprise.Type.Type.Contains("production"))
            {
                IsUsine = true;
                StyleVisible = true;
            }
        }

        
        private async Task Initialize(Exception ex, Task action)
        {
            Debug.WriteLine($"Echec operation: {ex.Message}");
            if (ex.Message.Contains("Unauthorize"))
            {
                await SessionService.GetNewToken(await SessionService.GetToken());
                await action;
            }
            else if (ex.Message.Contains("host"))
            {
                await action;
            }
            else MessageAlert.LongAlert("Erreur" + ex.Message);
        }
    }
}
