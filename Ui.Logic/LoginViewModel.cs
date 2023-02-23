using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading;

namespace Ui.Logic.ViewModel {

    public class LoginViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public LoginViewModel() {
            WaitGridVisibility = Visibility.Collapsed;
            if (IsInDesignMode) {
                WindowTitle = "Login (Designer-Ansicht)";
            } else {
                WindowTitle = "Login";
            }
        }

        private ICommand _PrepareSendData;
        public ICommand PrepareSendData {
            get {
                if (_PrepareSendData == null) {
                    _PrepareSendData = new RelayCommand(() => {
                        SendData();
                    });
                }
                return _PrepareSendData;
            }
        }

        private async void SendData() {
            WaitGridVisibility = Visibility.Visible;
            blurActivate = true;
            await Send();
            blurActivate = false;
            WaitGridVisibility = Visibility.Collapsed;
            Cleanup();
        }


        private async System.Threading.Tasks.Task Send() {
            await System.Threading.Tasks.Task.Run(() => {
                Thread.Sleep(4000);
            });
        }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; RaisePropertyChanged(); } }

        private bool _blurActivate { get; set; }
        public bool blurActivate { get { return _blurActivate; } set { _blurActivate = value; RaisePropertyChanged(); } }
        private Visibility _WaitGridVisibility { get; set; }
        public Visibility WaitGridVisibility { get { return _WaitGridVisibility; } set { _WaitGridVisibility = value; RaisePropertyChanged(); } }
    }
}