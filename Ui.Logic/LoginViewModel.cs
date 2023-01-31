using GalaSoft.MvvmLight;

namespace Ui.Logic.ViewModel {

    public class LoginViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public LoginViewModel(bool isEmployeeLogin) {
            if (IsInDesignMode) {
                if(isEmployeeLogin) {
                    WindowTitle = "Mitarbeiter-Login (Designer-Ansicht)";
                } else {
                    WindowTitle = "Kunden-Login (Designer-Ansicht)";
                }
            } else {
                if (isEmployeeLogin) {
                    WindowTitle = "Mitarbeiter-Login";
                } else {
                    WindowTitle = "Kunden-Login";
                }
            }
        }
    }
}