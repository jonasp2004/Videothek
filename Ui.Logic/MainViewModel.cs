using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class MainViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public MainViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Videothek (Designer-Ansicht)";
            } else {
                WindowTitle = "Videothek";
            }

            // Bereite Beispieldaten auf

            media = new ObservableCollection<MediaList>();
            media.Add(new MediaList(1, "Test 1", 11, 5, "DVD"));
            media.Add(new MediaList(2, "Test 2", 11, 9, "Blu-Ray"));
            media.Add(new MediaList(3, "Test 3", 11, 12, "4K-Blu-Ray"));
            media.Add(new MediaList(4, "Test: Die komplette remastered Trilogie", 11, 19, "4K-Blu-Ray"));
            media.Add(new MediaList(5, "TSBW-Simulator 2022: Der Ausbildungsstart", 13, 666, "Videospiel"));
            media.Add(new MediaList(5, "TSBW-Simulator 2021: Die BVB", 10, 5, "Videospiel"));
            media.Add(new MediaList(6, "Cooking Linda: Die Schnitzeljagd", 2, 4, "Videospiel"));
            media.Add(new MediaList(7, "Hello World-Programme für Dummies", 2, 10, "Buch"));
            media.Add(new MediaList(8, "Das ultimative Curry-Kochbuch", 1, 9999, "Buch"));
            media.Add(new MediaList(9, "AJs Workouts am Grill", 6, 23, "Buch"));
            media.Add(new MediaList(10, "Tolle Musik-Collection", 12, 6, "Musik-CD"));

            UserName = "@benutzerName2023";
            FullName = "Benutzer Hans";
        }

        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }

        private ICommand _ShowLogin;
        public ICommand ShowLogin
        {
            get
            {
                if (_ShowLogin == null)
                {
                    _ShowLogin = new RelayCommand(() => {
                        //Logic here
                        MessageBox.Show("TODO: Kunden-Login-Fenster soll erscheinen");
                    });
                }
                return _ShowLogin;
            }
        }

        private ICommand _ShowLoginEmp;
        public ICommand ShowLoginEmp {
            get {
                if (_ShowLoginEmp == null) {
                    _ShowLoginEmp = new RelayCommand(() => {
                        //Logic here
                        MessageBox.Show("TODO: Mitarbeiter-Login-Fenster soll erscheinen");
                    });
                }
                return _ShowLoginEmp;
            }
        }

        private ObservableCollection<MediaList> _media;
        public ObservableCollection<MediaList> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaList {
            public MediaList(int articleId, string name, int amount, decimal leasePrice, string category) {
                Name = name;
            }

            public string Name { get; set; }
        }

        private string _UserName { get; set; }
        public string UserName { get { return _UserName; } set { _UserName = value; RaisePropertyChanged(); } }
        private string _FullName { get; set; }
        public string FullName  { get { return _FullName; } set { _FullName = value; RaisePropertyChanged(); } } 
    }
}