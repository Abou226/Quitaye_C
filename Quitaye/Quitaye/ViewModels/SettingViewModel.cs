using Acr.UserDialogs;
using BaseVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Models;
using Plugin.Connectivity;
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
        public ObservableCollection<Style> Styles { get; }
        public ObservableCollection<Marque> Marques { get; }
        public ObservableCollection<Categorie> Categories { get; }
        public ObservableCollection<Model> Models { get; }
        public IBaseViewModel BaseVM { get; }
        public IDataService<Test> Test { get; }
        public IDataService<Style> StyleService { get; }
        public IDataService<Marque> MarqueService { get; }
        public ObservableCollection<object> Items { get; }
        public IDataService<Gamme> GammeService { get; }
        public IDataService<Produit_Fini> ProduitFiniService { get; }
        public IDataService<Matière_Premiere> Matière_PremièreService { get; }
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

        private string guid;

        public string EntrepriseId
        {
            get { return guid; }
            set 
            {
                if (guid == value)
                    return;
                guid = value;
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

        private string _description;

        public string Description
        {
            get { return _description; }
            set 
            {
                if (_description == value)
                    return;
                _description = value;
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


        private Taille taille;

        public Taille Taille
        {
            get { return taille; }
            set 
            {
                if (taille == value)
                    return;

                taille = value;
                OnPropertyChanged();
            }
        }

        private Style style;

        public Style Style
        {
            get { return style; }
            set 
            {
                if (style == value)
                    return;

                style = value;
                OnPropertyChanged();
                if (style.Style_Special)
                    Style_Special = true;
                else Style_Special = false;
            }
        }

        private Marque marque;

        public Marque Marque
        {
            get { return marque; }
            set 
            {
                if (marque == value)
                    return;
                marque = value;
                OnPropertyChanged();
            }
        }

        private Categorie categorie;

        public Categorie Categorie
        {
            get { return categorie; }
            set 
            {
                if (categorie == value)
                    return;

                categorie = value;
                OnPropertyChanged();
            }
        }

        private Gamme gamme;

        public Gamme Gamme
        {
            get { return gamme; }
            set 
            {
                if (gamme == value)
                    return;
                gamme = value;
                OnPropertyChanged();
            }
        }

        private Model model;

        public Model Model
        {
            get { return model; }
            set 
            {
                if (model == value)
                    return;
                model = value;
                OnPropertyChanged();
            }
        }

        public IMessage MessageAlert { get; }
        public ObservableCollection<Gamme> Gammes { get; }
        public ICommand AddImageComand { get; }
        public IDataService<Model> ModelService { get; }
        public IDataService<Categorie> CategorieService { get; }
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

        public ICommand AddStyleCommand { get; }
        public ICommand AddTailleCommand { get; }
        public ICommand AddMarqueCommand { get; }
        private bool _isGamme;

        public bool IsGamme
        {
            get { return _isGamme; }
            set 
            {
                if (_isGamme == value)
                    return;
                _isGamme = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddModelCommand { get; }
        public ICommand AddGammeCommand { get; }
        public ICommand AddCategorieCommand { get; }

        public ICommand AddCommand { get; }

        public ICommand SectionTappedCommand { get; }
        public IFormFile Image { get; set; }

        private decimal prix_Unité;

        public decimal Prix_Unité
        {
            get { return prix_Unité; }
            set 
            {
                if (prix_Unité == value)
                    return;

                prix_Unité = value;
                OnPropertyChanged();
            }
        }

        private string _url;

        public string Url
        {
            get { return _url; }
            set 
            {
                if (_url == value)
                    return;

                _url = value;
                OnPropertyChanged();
            }
        }

        private bool style_Special;

        public bool Style_Special
        {
            get { return style_Special; }
            set 
            {
                if (style_Special == value)
                    return;

                style_Special = value;
                OnPropertyChanged();
            }
        }


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
        public Stream Stream { get; set; }
        private async void OnAddImageCommand(object obj)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Veillez selectionner une image"
            });

            
            if(result != null)
            {
                FileResult = result;
                Stream = await result.OpenReadAsync();
                PictureSource = ImageSource.FromStream(() => Stream);
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

        public FileResult FileResult { get; set; }

        public IDataService<Taille> TailleService { get; }

        public SettingViewModel()
        {
            Styles = new ObservableCollection<Style>();
            Marques = new ObservableCollection<Marque>();
            Categories = new ObservableCollection<Categorie>();
            Sections = new ObservableCollection<Section>();
            Models = new ObservableCollection<Model>();
            ModelService = DependencyService.Get<IDataService<Model>>();
            GammeService = DependencyService.Get<IDataService<Gamme>>();
            CategorieService = DependencyService.Get<IDataService<Categorie>>();
            Items = new ObservableCollection<object>();
            BaseVM = DependencyService.Get<IBaseViewModel>();
            SectionTappedCommand = new Command(OnSectionTappedCommand);
            ProduitFiniService = DependencyService.Get<IDataService<Produit_Fini>>();
            Matière_PremièreService = DependencyService.Get<IDataService<Matière_Premiere>>();
            AddImageComand = new Command(OnAddImageCommand);
            AddCommand = new Command(OnAddCommand);
            TailleService = DependencyService.Get<IDataService<Taille>>();
            Test = DependencyService.Get<IDataService<Test>>();
            Gammes = new ObservableCollection<Gamme>();
            MessageAlert = DependencyService.Get<IMessage>();
            MarqueService = DependencyService.Get<IDataService<Marque>>();
            StyleService = DependencyService.Get<IDataService<Style>>();
            Init = DependencyService.Get<IInitialService>();
            GetSections();
        }

        private async void OnAddCommand(object obj)
        {
            if (CurrentSection.Nom == "Styles")
                await OnAddStyleCommand(obj);
            else if (CurrentSection.Nom == "Models")
                await OnAddModelCommand(obj);
            else if (CurrentSection.Nom == "Marques")
                await OnAddMarqueCommand(obj);
            else if (CurrentSection.Nom == "Tailles")
                await OnAddTailleCommand(obj);
            else if (CurrentSection.Nom == "Gammes")
                await OnAddGammeCommand(obj);
            else if (CurrentSection.Nom == "Categories")
                await OnAddCategorieCommand(obj);
            
        }

        private async Task OnAddTailleCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Description)
                && !string.IsNullOrWhiteSpace(FileResult.FileName))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;
                    if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                    {
                        Stream = await FileResult.OpenReadAsync();
                        Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                    }
                    else Image = null;

                    var result = await TailleService.AddFormDataAsync(new Taille()
                    {
                        Name = Nom,
                        Description = Description,
                        EntrepriseId = Entreprise.Id,
                    }, await SecureStorage.GetAsync("Token"));

                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetMarquesAsync();

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                    }
                }
                catch (Exception ex)
                {
                    //await Initializes(ex, OnAddGammeCommand(obj));
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async void OnSectionTappedCommand(object obj)
        {
            var value = (Section)obj;
            CurrentSection = value;
            ChangeIcon(value);
            IsStyle = false;
            if(value.Nom == "Gammes")
            {
                IsGamme = true;
                GetCategoriesAsync();
                GetMarquesAsync();
                GetStylesAsync();
                await GetGammesAsync();
            }
            else
            {
                IsGamme = false;
                if (value.Nom == "Categories")
                    await GetCategoriesAsync();
                else if (value.Nom == "Styles")
                {
                    IsStyle = true;
                    await GetStylesAsync();
                }
                else if (value.Nom == "Tailles")
                {
                    await GetTaillesAsync();
                }
                else if (value.Nom == "Marques")
                    await GetMarquesAsync();
                else if (value.Nom == "Models")
                    await GetModelsAsync();
                else if(value.Nom == "Matière Première")
                {
                    await GetMatièrePremièresAsync();
                }
                else if(value.Nom == "Produit Fini")
                {
                    await GetProduitFinisAsync();
                }
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
            }else if(section.Nom == "Produit Fini")
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
                Icon = "inventory.png",
                Black_Icon = "inventory_black.png",
                CurrentIcon = "inventory_black.png"
            });

            Sections.Add(new Section()
            {
                Nom = "Produit Fini",
                Icon = "inventory.png",
                Black_Icon = "inventory_black.png",
                CurrentIcon = "inventory_black.png"
            });
        }

        private async Task OnAddCategorieCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Description) 
                && !string.IsNullOrWhiteSpace(FileResult.FileName))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;
                    if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                    {
                        Stream = await FileResult.OpenReadAsync();
                        Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                    }
                    else Image = null;

                    
                    var result = await CategorieService.AddFormDataAsync(new Categorie()
                    {
                        Name = Nom,
                        EntrepriseId = Entreprise.Id,
                        Image = Image,
                        Description = Description,
                        Url = "d",
                    }, await SecureStorage.GetAsync("Token")) ;
                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetCategoriesAsync();

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Initialize(ex, OnAddCategorieCommand(obj));
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async Task OnAddGammeCommand(object obj)
        {
            if (Style_Special)
            {
                if (Categorie != null && Style != null && Prix_Unité != 0)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Validation....");
                        //var stream = File.Op;
                        //long sd = 123450404;
                        if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                        {
                            Stream = await FileResult.OpenReadAsync();
                            Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                        }
                        else Image = null;

                        var result = await GammeService.AddFormDataAsync(new Gamme()
                        {
                            CategorieId = Categorie.Id,
                            StyleId = Style.Id,
                            Image = Image,
                            EntrepriseId = Entreprise.Id,
                            Prix_Unité = Prix_Unité,
                            Url = "d",
                        }, await SecureStorage.GetAsync("Token"));

                        ClearData();
                        if (result == null)
                        {

                        }
                        UserDialogs.Instance.HideLoading();

                        await GetGammesAsync();

                        if (result != null)
                        {
                            BaseVM.RefreshProduit = true;
                            await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, OnAddGammeCommand(obj));
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            else
            {
                if (Categorie != null && Marque != null && Style != null && Prix_Unité != 0)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Validation....");
                        //var stream = File.Op;
                        //long sd = 123450404;
                        if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                        {
                            Stream = await FileResult.OpenReadAsync();
                            Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                        }
                        else Image = null;

                        var result = await GammeService.AddFormDataAsync(new Gamme()
                        {
                            MarqueId = Marque.Id,
                            CategorieId = Categorie.Id,
                            StyleId = Style.Id,
                            Image = Image,
                            EntrepriseId = Entreprise.Id,
                            Prix_Unité = Prix_Unité,
                            Url = "d",
                        }, await SecureStorage.GetAsync("Token"));

                        ClearData();
                        if (result == null)
                        {

                        }
                        UserDialogs.Instance.HideLoading();

                        await GetGammesAsync();

                        if (result != null)
                        {
                            BaseVM.RefreshProduit = true;
                            await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, OnAddGammeCommand(obj));
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            
        }

        
        private async Task OnAddMarqueCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Description)
                && !string.IsNullOrWhiteSpace(FileResult.FileName))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;
                    if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                    {
                        Stream = await FileResult.OpenReadAsync();
                        Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                    }
                    else Image = null;

                    var result = await MarqueService.AddFormDataAsync(new Marque()
                    {
                        Name = Nom,
                        EntrepriseId = Entreprise.Id,
                        Description = Description,
                        Image = Image,
                        Url = "d",
                    }, await SecureStorage.GetAsync("Token"));

                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetMarquesAsync();

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Initialize(ex, OnAddMarqueCommand(obj));
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private async Task OnAddModelCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Description)
                && !string.IsNullOrWhiteSpace(FileResult.FileName))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;
                    if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                    {
                        Stream = await FileResult.OpenReadAsync();
                        Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                    }
                    else Image = null;

                    var result = await ModelService.AddFormDataAsync(new Model()
                    {
                        Name = Nom,
                        Description = Description,
                        EntrepriseId = Entreprise.Id,
                    }, await SecureStorage.GetAsync("Token"));

                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetModelsAsync();

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Initialize(ex, OnAddModelCommand(obj));
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        private void ClearData()
        {
            Nom = null;
            Description = null;
            FileResult = null;
            PictureSource = null;
            FileResult = null;
        }

        private async Task OnAddStyleCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Description)
                && !string.IsNullOrWhiteSpace(FileResult.FileName))
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Validation....");
                    //var stream = File.Op;
                    //long sd = 123450404;
                    if (!string.IsNullOrWhiteSpace(FileResult.FileName))
                    {
                        Stream = await FileResult.OpenReadAsync();
                        Image = new FormFile(Stream, Stream.Position, Stream.Length, FileResult.FileName, FileResult.FileName);
                    }
                    else Image = null;

                    var result = await StyleService.AddFormDataAsync(new Style()
                    {
                        Name = Nom,
                        EntrepriseId = Entreprise.Id,
                        Description = Description,
                        Image = Image,
                        Style_Special = Style_Special,
                        Url = "d",
                    }, await SecureStorage.GetAsync("Token"));
                    ClearData();
                    if (result == null)
                    {

                    }
                    UserDialogs.Instance.HideLoading();

                    await GetStylesAsync();

                    if (result != null)
                    {
                        BaseVM.RefreshProduit = true;
                        await Application.Current.MainPage.DisplayAlert("Confirmation", "Opération effectuée avec succès !", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Initialize(ex, OnAddStyleCommand(obj));
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        public SettingViewModel(INavigation navigation, Entreprise entreprise) : this()
        {
            Navigation = navigation;
            Entreprise = entreprise;
            if (Entreprise.Type.Type.Contains("production"))
                IsUsine = true;
            GetStylesAsync();
        }

        private async Task CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                do
                {
                    try
                    {
                        var result = await Test.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Tests");
                        //if(result == null)
                        {
                            BaseVM.IsInternetOn = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("host"))
                        {
                            BaseVM.IsInternetOn = false;
                        }
                        else BaseVM.IsInternetOn = true;
                    }
                } while (!BaseVM.IsInternetOn);
            }
            else
            {
                BaseVM.IsInternetOn = false;
            }
        }

        async Task GetStylesAsync()
        {
            await CheckConnection();
            //do
            //{
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Chargement....");
                    var styles = await StyleService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Styles/"+ Entreprise.Id.ToString());
                    Items.Clear();
                    Styles.Clear();
                    if (styles.Count() != 0)
                    {
                        foreach (var item in styles)
                        {
                            Items.Add(item);
                            Styles.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Initialize(ex, GetStylesAsync());
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
            //} while (!BaseVM.IsInternetOn);
        }

        async Task GetTaillesAsync()
        {
            await CheckConnection();
            //do
            //{
            if (BaseVM.IsInternetOn)
            {
                if (IsNotBusy)
                    return;

                try
                {
                    IsNotBusy = false;
                    UserDialogs.Instance.ShowLoading("Chargement....");
                    var styles = await TailleService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Tailles/" + Entreprise.Id.ToString());
                    Items.Clear();
                    
                    if (styles.Count() != 0)
                    {
                        foreach (var item in styles)
                        {
                            Items.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Initialize(ex, GetTaillesAsync());
                }
                finally
                {
                    IsNotBusy = true;
                    UserDialogs.Instance.HideLoading();
                }
            }
            //} while (!BaseVM.IsInternetOn);
        }

        async Task GetMarquesAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var marques = await MarqueService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Marques/"+Entreprise.Id.ToString());
                        Items.Clear();
                        Marques.Clear();
                        if (marques.Count() != 0)
                        {
                            foreach (var item in marques)
                            {
                                Items.Add(item);
                                Marques.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetMarquesAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }

        async Task GetModelsAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var models = await ModelService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Models/"+Entreprise.Id.ToString());
                        Items.Clear();
                        Models.Clear();
                        if (models.Count() != 0)
                        {
                            foreach (var item in models)
                            {
                                Items.Add(item);
                                Models.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetModelsAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }

        async Task GetProduitFinisAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var produit_Finis = await ProduitFiniService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Produit_Finis/" + Entreprise.Id.ToString());
                        Items.Clear();
                        if (produit_Finis.Count() != 0)
                        {
                            foreach (var item in produit_Finis)
                            {
                                Items.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetProduitFinisAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }

        async Task GetMatièrePremièresAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var matières = await Matière_PremièreService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Matiere_Premieres/" + Entreprise.Id.ToString());
                        Items.Clear();
                        if (matières.Count() != 0)
                        {
                            foreach (var item in matières)
                            {
                                Items.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetMatièrePremièresAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }

        async Task GetGammesAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var gammes = await GammeService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Gammes/"+Entreprise.Id.ToString());
                        Items.Clear();
                        if (gammes.Count() != 0)
                        {
                            foreach (var item in gammes)
                            {
                                Items.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetGammesAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }

        async Task GetCategoriesAsync()
        {
            await CheckConnection();
            do
            {
                if (BaseVM.IsInternetOn)
                {
                    if (IsNotBusy)
                        return;

                    try
                    {
                        IsNotBusy = false;
                        UserDialogs.Instance.ShowLoading("Chargement....");
                        var categories = await CategorieService.GetItemsAsync(await SecureStorage.GetAsync("Token"), "Categories/" + Entreprise.Id.ToString());
                        Items.Clear();
                        Categories.Clear();
                        if (categories.Count() != 0)
                        {
                            foreach (var item in categories)
                            {
                                Items.Add(item);
                                Categories.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Initialize(ex, GetCategoriesAsync());
                    }
                    finally
                    {
                        IsNotBusy = true;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            } while (!BaseVM.IsInternetOn);
        }

        private async Task Initialize(Exception ex, Task action)
        {
            Debug.WriteLine($"Echec operation: {ex.Message}");
            if (ex.Message.Contains("Unauthorize"))
            {
                var result = await Init.Get(new LogInModel() { Token = await SecureStorage.GetAsync("Token"), Password = "d", Username = "d" });
                await SecureStorage.SetAsync("Token", result.Token);
                await SecureStorage.SetAsync("Prenom", result.Prenom);
                await SecureStorage.SetAsync("Nom", result.Nom);
                await SecureStorage.SetAsync("ProfilePic", result.ProfilePic);
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
