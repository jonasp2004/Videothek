using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class UpdateViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public UpdateViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Updates (Designer-Ansicht)";
            } else {
                WindowTitle = "Updates";
            }
            waitingVisible = Visibility.Collapsed;
            
        }


        private ICommand _PrepareSearch;
        public ICommand PrepareSearch
        {
            get
            {
                if (_PrepareSearch == null)
                {
                    _PrepareSearch = new RelayCommand(() => {
                        SearchUpdate();
                    });
                }
                return _PrepareSearch;
            }
        }

        private async void SearchUpdate() {
            blurActivate = true;
            waitingVisible = Visibility.Visible;
            await Timer(2000);
            latestVersion = "Aktuellste Version: 1.0";
            blurActivate = false;
            waitingVisible = Visibility.Collapsed;
        }
        internal async Task Timer(int duration) { await Task.Delay(duration); }


        private string _latestVersion { get; set; }
        public string latestVersion { get { return _latestVersion; } set { _latestVersion = value; RaisePropertyChanged(); } }


        private bool _blurActivate { get; set; }
        public bool blurActivate { get { return _blurActivate; } set { _blurActivate = value; RaisePropertyChanged(); } }
        private Visibility _waitingVisible { get; set; }
        public Visibility waitingVisible { get { return _waitingVisible; } set { _waitingVisible = value; RaisePropertyChanged(); } }
    }
}