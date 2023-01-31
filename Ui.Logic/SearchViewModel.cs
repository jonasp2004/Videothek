using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Ui.Logic.ViewModel {

    public class SearchViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public SearchViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Suche (Designer-Ansicht)";
            } else {
                WindowTitle = "Suche";
            }
        }

        private ObservableCollection<SearchList> _search;
        public ObservableCollection<SearchList> search { get { return _search; } set { _search = value; RaisePropertyChanged(); } }
        public class SearchList {
            public SearchList(int articleId, string name, int amount, decimal leasePrice, string category) {
                Name = name;
            }

            public string Name { get; set; }
        }
    }
}