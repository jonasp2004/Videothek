using GalaSoft.MvvmLight;

namespace Ui.Logic.ViewModel {

    public class RegisterViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public RegisterViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Registrieren (Designer-Ansicht)";
            } else {
                WindowTitle = "Registrieren";
            }
        }

        private string _Username;
        public string Username { get { return _Username; } set { _Username = value; RaisePropertyChanged(); } }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; RaisePropertyChanged(); } }

        private string _Password2;
        public string Password2 { get { return _Password2; } set { _Password2 = value; RaisePropertyChanged(); } }
    }
}