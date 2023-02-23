using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class OfferingsViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public OfferingsViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Alle Inhalte (Designer-Ansicht)";
            } else {
                WindowTitle = "Alle Inhalte";
            }

            // Bereite Beispieldaten auf

            media = new ObservableCollection<MediaList>();
            media.Add(new MediaList(1, "Test 1", 11, 5, "DVD"));
            media.Add(new MediaList(2, "Test 2", 11, 9, "DVD"));
            media.Add(new MediaList(3, "Test 3", 11, 12, "Blu-Ray"));
            media.Add(new MediaList(4, "Test: Die komplette remastered Trilogie", 11, 19, "Blu-Ray"));
            media.Add(new MediaList(5, "TSBW-Simulator 2022: Der Ausbildungsstart", 13, 666, "Videospiel"));
            media.Add(new MediaList(5, "TSBW-Simulator 2021: Die BVB", 10, 5, "Videospiel"));
            media.Add(new MediaList(6, "Cooking Linda: Die Schnitzeljagd", 2, 4, "Videospiel"));
            media.Add(new MediaList(7, "Hello World-Programme für Dummies", 2, 10, "Buch"));
            media.Add(new MediaList(8, "Das ultimative Curry-Kochbuch", 1, 9999, "Buch"));
            media.Add(new MediaList(9, "AJs Workouts am Grill", 6, 23, "Buch"));
            media.Add(new MediaList(10, "Tolle Musik-Collection", 12, 6, "Musik-CD"));

            ComboBoxSelection = 0;
        }

        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }

        private ObservableCollection<MediaList> _media;
        public ObservableCollection<MediaList> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaList {
            public MediaList(int articleId, string name, int amount, decimal leasePrice, string category) {
                Artikelnummer = articleId.ToString();
                Name = name;
                Typ = category;
                Preis = leasePrice.ToString() + " €";
            }

            public string Artikelnummer { get; set; }
            public string Name { get; set; }
            public string Typ { get; set; }
            public string Preis { get; set; }
        }

        private int _ComboBoxSelection { get; set; }
        public int ComboBoxSelection { get { return _ComboBoxSelection; } set { _ComboBoxSelection = value; RaisePropertyChanged(); } }
    }
}