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
    }
}