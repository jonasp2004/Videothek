using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Ui.Logic;

namespace Ui.Logic.ViewModel {

    public class MediamanagementViewModel : ViewModelBase {

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

            media = new ObservableCollection<MediaList>();
            media.Add(new MediaList(1, "Test 1", 11, 5, "DVD"));
            media.Add(new MediaList(2, "Test 2", 11, 9, "DVD"));
            media.Add(new MediaList(3, "Test 3", 11, 12, "Blue-Ray"));
            media.Add(new MediaList(4, "Test: Die komplette remastered Trilogie", 11, 19, "Blue-Ray"));
            media.Add(new MediaList(5, "TSBW-Simulator 2022: Der Ausbildungsstart", 13, 666, "Videospiel"));
            media.Add(new MediaList(5, "TSBW-Simulator 2021: Die BVB", 10, 5, "Videospiel"));
            media.Add(new MediaList(6, "Cooking Linda: Die Schnitzeljagd", 2, 4, "Videospiel"));
            media.Add(new MediaList(7, "Hello World-Programme für Dummies", 2, 10, "Buch"));
            media.Add(new MediaList(8, "Das ultimative Curry-Kochbuch", 1, 9999, "Buch"));
            media.Add(new MediaList(9, "AJs Workouts am Grill", 6, 23, "Buch"));
            media.Add(new MediaList(10, "Tolle Musik-Collection", 12, 6, "Musik-CD"));
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
                                media.Add(new MediaList(media.Count + 1, Titel, Menge, Preis, sel));
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
                        Titel = media[ListSelection].Name.ToString();
                        Menge = media[ListSelection].Menge;
                        Preis = System.Convert.ToInt32(media[Preis].Leihpreis);
                        string typ = media[ListSelection].Kategorie;
                        if (typ == "DVD") { TypAuswahl = 0; }
                        else if (typ == "Blue-Ray") { TypAuswahl = 1; }
                        else if (typ == "Musik-CD") { TypAuswahl = 2; }
                        else if (typ == "Videospiel") { TypAuswahl = 3; }
                        else if (typ == "Buch") { TypAuswahl = 4; }
                        TypAuswahl = 0;
                        HinzufügenDialogTitel = "Artikel bearbeiten";
                        ArtikelBearbeiten = true;
                    });
                }
                return _ShowEditItem;
            }
        }


        private int _ListSelection { get; set; }
        public int ListSelection { get { return _ListSelection; } set { _ListSelection = value; RaisePropertyChanged(); } }
        private ICommand _DeleteItem;
        public ICommand DeleteItem {
            get {
                if (_DeleteItem == null) {
                    _DeleteItem = new RelayCommand(() => {
                        media.RemoveAt(ListSelection);
                    });
                }
                return _DeleteItem;
            }
        }


        private string _Titel { get; set; }
        public string Titel { get { return _Titel; } set { _Titel = value; RaisePropertyChanged(); } }
        private int _Menge { get; set; }
        public int Menge { get { return _Menge; } set { _Menge = value; RaisePropertyChanged(); } }
        private int _Preis { get; set; }
        public int Preis { get { return _Preis; } set { _Preis = value; RaisePropertyChanged(); } }
        private int _TypAuswahl { get; set; }
        public int TypAuswahl { get { return _TypAuswahl; } set { _TypAuswahl = value; RaisePropertyChanged(); } }

        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }

        private ObservableCollection<MediaList> _media;
        public ObservableCollection<MediaList> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaList {
            public MediaList(int articleId, string name, int amount, decimal leasePrice, string category) {
                Artikelnummer = articleId;
                Name = name;
                Menge = amount;
                Leihpreis = leasePrice;
                Kategorie = category;
            }

            public int Artikelnummer { get; set; }
            public string Name { get; set; }
            public int Menge { get; set; }
            public decimal Leihpreis { get; set; }
            public string Kategorie { get; set; }
        }


        private string _HinzufügenDialogTitel { get; set; }
        public string HinzufügenDialogTitel { get { return _HinzufügenDialogTitel; } set { _HinzufügenDialogTitel = value; RaisePropertyChanged(); } }
        private bool _ArtikelBearbeiten { get; set; }
        public bool ArtikelBearbeiten { get { return _ArtikelBearbeiten; } set { _ArtikelBearbeiten = value; RaisePropertyChanged(); } }
    }
}