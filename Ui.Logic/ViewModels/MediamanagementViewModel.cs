using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Ui.Logic;
using static Ui.Logic.ViewModel.SearchViewModel;

namespace Ui.Logic.ViewModel {

    public class MediamanagementViewModel : ViewModelBase {

        private string _Titel { get; set; }
        public string Titel { get { return _Titel; } set { _Titel = value; RaisePropertyChanged(); } }
        private int _Menge { get; set; }
        public int Menge { get { return _Menge; } set { _Menge = value; RaisePropertyChanged(); } }
        private int _Preis { get; set; }
        public int Preis { get { return _Preis; } set { _Preis = value; RaisePropertyChanged(); } }
        private int _TypAuswahl { get; set; }
        public int TypAuswahl { get { return _TypAuswahl; } set { _TypAuswahl = value; RaisePropertyChanged(); } }
        private string _Beschreibung { get; set; }
        public string Beschreibung { get { return _Beschreibung; } set { _Beschreibung = value; RaisePropertyChanged(); } }
        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }
        private int _ListSelection { get; set; }
        public int ListSelection { get { return _ListSelection; } set { _ListSelection = value; RaisePropertyChanged(); } }
        private string _HinzufügenDialogTitel { get; set; }
        public string HinzufügenDialogTitel { get { return _HinzufügenDialogTitel; } set { _HinzufügenDialogTitel = value; RaisePropertyChanged(); } }
        private bool _ArtikelBearbeiten { get; set; }
        public bool ArtikelBearbeiten { get { return _ArtikelBearbeiten; } set { _ArtikelBearbeiten = value; RaisePropertyChanged(); } }


        private ObservableCollection<MediaItem> _media;
        public ObservableCollection<MediaItem> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }

        public string WindowTitle { get; set; }
        public MediamanagementViewModel() {
            HinzufügenDialogTitel = "Artikel hinzufügen";
            if (IsInDesignMode) {
                WindowTitle = "Medienverwaltung (Designer-Ansicht)";
            } else {
                WindowTitle = "Medienverwaltung";
            }
            ArtikelBearbeiten = false;

            TypAuswahl = 0;

            ObservableCollection<MediaItem> mediaCollection = new ObservableCollection<MediaItem>();
            media = mediaCollection;
            ReloadList();
        }

        public void ReloadList() {
            try {
                if (media == null) {
                    ObservableCollection<MediaItem> media = new System.Collections.ObjectModel.ObservableCollection<MediaItem>();
                }
                media.Clear();
                ObservableCollection<MediaItem> mediaCollection = new ObservableCollection<MediaItem>();
                foreach (var item in GetAll(mediaCollection)) {
                    media.Add(item);
                }
                mediaCollection.Clear();
            } catch (Exception ex) {
                MessageBox.Show("Ein Fehler bei der Verarbeitung der Suche ist aufgetreten.\n\n" + ex.Message, "Upsi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ObservableCollection<MediaItem> GetAll(ObservableCollection<MediaItem> mediaCol) {
            mediaCol.Clear();
            using (videothek_development db = new videothek_development()) {
                var query = db.Article
                .Select(n => new { n.ArticleId, n.Name, n.Amount, n.LeasePrice, n.CategoryId, n.Description });

                foreach (var item in query) {
                    int artnbr = Convert.ToInt32(item.ArticleId);
                    string category = db.Category
                            .Where(c => c.CategoryId.Equals(item.CategoryId))
                            .Select(c => c.Name)
                            .SingleOrDefault();
                    mediaCol.Add(new MediaItem(
                        Convert.ToInt32(item.ArticleId),
                        item.Name,
                        Convert.ToInt32(item.Amount),
                        Convert.ToInt32(item.LeasePrice),
                        db.Category
                                .Where(c => c.CategoryId.Equals(item.CategoryId))
                                .Select(c => c.Name)
                                .SingleOrDefault(),
                        item.Description
                    ));
                }
            }
            return mediaCol;
        }

        private ICommand _AddItem;
        public ICommand AddItem {
            get {
                if (_AddItem == null) {
                    _AddItem = new RelayCommand(() => {
                        HinzufügenDialogTitel = "Artikel hinzufügen";
                        ArtikelBearbeiten = false;
                        string sel = "";
                        switch (TypAuswahl) {
                            case 0:
                                sel = "DVDs";
                                break;
                            case 1:
                                sel = "Blue-Ray";
                                break;
                            case 2:
                                sel = "Musik-CD";
                                break;
                            case 3:
                                sel = "Videospiel";
                                break;
                            case 4:
                                sel = "Buch";
                                break;
                        }
                        if(Titel != "" && Menge >= 0 && Preis >= 0) {
                            try {
                                media.Add(new MediaItem(media.Count + 1, Titel, Menge, Preis, sel, Beschreibung));
                            } catch {
                                MessageBox.Show("Fehler");
                            }
                        }
                    });
                }
                return _AddItem;
            }
        }

        private ICommand _ShowAddItem;
        public ICommand ShowAddItem {
            get {
                if (_ShowAddItem == null) {
                    _ShowAddItem = new RelayCommand(() => {
                        Titel = "";
                        Menge = 0;
                        Preis = 0;
                        TypAuswahl = 0;
                        HinzufügenDialogTitel = "Artikel hinzufügen";
                        ArtikelBearbeiten = false;
                        Beschreibung = "";
                    });
                }
                return _ShowAddItem;
            }
        }


        private ICommand _ShowEditItem;
        public ICommand ShowEditItem {
            get {
                if (_ShowEditItem == null) {
                    _ShowEditItem = new RelayCommand(() => {
                        try {
                            Titel = media[ListSelection].Name.ToString();
                            Menge = media[ListSelection].Menge;
                            Preis = System.Convert.ToInt32(media[ListSelection].Leihpreis);
                            Beschreibung = media[ListSelection].Beschreibung;
                            string typ = media[ListSelection].Kategorie;

                            if (typ == "Videospiel") { TypAuswahl = 0; }
                            else if (typ == "DVD") { TypAuswahl = 1; }
                            else if (typ == "Buch") { TypAuswahl = 2; }
                            else if (typ == "Musik-CD") { TypAuswahl = 3; }
                            else if (typ == "Blue-Ray") { TypAuswahl = 4; }
                            else if (typ == "Spiel") { TypAuswahl = 5; }
                            else if (typ == "Zeitschrift") { TypAuswahl = 6; }

                            HinzufügenDialogTitel = "Artikel bearbeiten";
                            ArtikelBearbeiten = true;
                        } catch {
                            MessageBox.Show("Fehler: Einige Elemente können nicht angezeigt werden. Index: " + ListSelection, "Upsi!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        
                    });
                }
                return _ShowEditItem;
            }
        }

        private ICommand _DeleteItem;
        public ICommand DeleteItem {
            get {
                if (_DeleteItem == null) {
                    _DeleteItem = new RelayCommand(() => {
                        try {
                            using(videothek_development db = new videothek_development()) {
                                int articleId = System.Convert.ToInt32(media[ListSelection].Artikelnummer);
                                var rowToDelete = db.Article.FirstOrDefault(x => x.ArticleId.Equals(articleId));

                                if (rowToDelete != null) {
                                    db.Article.Remove(rowToDelete);
                                    db.SaveChanges();
                                }

                                if(media == null) {
                                    ObservableCollection<MediaItem> media = new ObservableCollection<MediaItem>();
                                }
                                ObservableCollection<MediaItem> mediaCollection = new ObservableCollection<MediaItem>();
                                media = mediaCollection;
                                ReloadList();
                            }
                        } catch (Exception ex) {
                            MessageBox.Show("Ein Fehler ist aufgetreten:\n" + ex.Message, "Upsi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    });
                }
                return _DeleteItem;
            }
        }

        private ICommand _AddOrEditItem;
        public ICommand AddOrEditItem {
            get {
                if (_AddOrEditItem == null) {
                    _AddOrEditItem = new RelayCommand(() => {
                        try {
                            using(videothek_development db = new videothek_development()) {
                                int categoryId = TypAuswahl + 1;
                                int currentArticle = System.Convert.ToInt32(media[ListSelection].Artikelnummer);
                                if (ArtikelBearbeiten) {
                                    var artikel = db.Article.FirstOrDefault(s => s.ArticleId.Equals(currentArticle));
                                    if(artikel != null) {
                                        artikel.Amount = Menge;
                                        artikel.LeasePrice = Preis;
                                        artikel.Name = Titel;
                                        artikel.Description = Beschreibung;
                                        artikel.CategoryId = categoryId;
                                        db.SaveChanges();
                                    }
                                } else {
                                    var newArticle = new Article {
                                        Name = Titel,
                                        Amount = Menge,
                                        LeasePrice = Preis,
                                        Description = Beschreibung,
                                        CategoryId = categoryId,
                                    };
                                    db.Article.Add(newArticle);
                                    db.SaveChanges();
                                }
                                ReloadList();
                            }
                        } catch (Exception ex) {
                            MessageBox.Show("Fehler:\n" + ex.Message, "Upsi!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    });
                }
                return _AddOrEditItem;
            }
        }

        public class MediaItem {
            public MediaItem(int articleId, string name, int amount, int leasePrice, string category, string description) {
                Artikelnummer = articleId;
                Name = name;
                Menge = amount;
                Leihpreis = leasePrice;
                Kategorie = category;
                Beschreibung = description;
            }

            public int Artikelnummer { get; set; }
            public string Name { get; set; }
            public int Menge { get; set; }
            public int Leihpreis { get; set; }
            public string Kategorie { get; set; }
            public string Beschreibung { get; set; }
        }
    }
}