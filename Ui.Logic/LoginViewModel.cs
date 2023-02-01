using GalaSoft.MvvmLight;

namespace Ui.Logic.ViewModel {

    public class LoginViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public LoginViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Login (Designer-Ansicht)";
            } else {
                WindowTitle = "Login";
            }
        }
    }
}