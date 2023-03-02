using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ui.Logic.ViewModel {

    public class MyContentsViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public MyContentsViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Meine Inhalte (Designer-Ansicht)";
            } else {
                WindowTitle = "Meine Inhalte";
            }

            // Bereite Beispieldaten auf
            media = new ObservableCollection<MediaList>();

            media.Add(new MediaList(573, "01.08.2025", "01.08.2022", "TSBW-Simulator 2022: Der Ausbildungsstart"));
            media.Add(new MediaList(867, "31.06.2022", "08.08.2021", "TSBW-Simulator 2021: Die BVB"));
            media.Add(new MediaList(2, "07.04.2032", "01.01.1765", "Cooking Linda: Die Schnitzeljagd"));
        }

        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }

        private ObservableCollection<MediaList> _media;
        public ObservableCollection<MediaList> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaList {
            public MediaList(int rentalId, string submissionDate, string rented, string articleName) {
                Leihnummer = rentalId;
                Ausgabedatum = rented;
                Rückgabedatum = submissionDate;
                Artikelbezeichnung = articleName;
            }

            public int Leihnummer { get ; set; }
            public string Ausgabedatum { get; set; }
            public string Rückgabedatum { get; set; }
            public string Artikelbezeichnung { get; set; }
        }
    }
}