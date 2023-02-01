using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class AccountViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public AccountViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Mein Account (Designer-Ansicht)";
            } else {
                WindowTitle = "Mein Account";
            }

            // Bereite Beispieldaten auf

            UserName = "@benutzerName2023";
            Email = "benutzer.name@benutzernetz.de";
            Password = "IchMagKäse123#";
            Vorname = "Benutzer";
            Nachname = "Name";
            Strasse = "Musterstraße";
            Hausnummer = 1.ToString();
            ZIP = 12345.ToString();
            Location = "Musterdorf";

            FullName = Vorname + " " + Nachname;
            Password2 = Password;
        }

        private ICommand _PrepareSendData;
        public ICommand PrepareSendData
        {
            get {
                if (_PrepareSendData == null) {
                    _PrepareSendData = new RelayCommand(() => {
                        blurActivate = true;
                        SendData();
                        MessageBox.Show(Vorname + Strasse);
                        blurActivate = false;
                    });
                }
                return _PrepareSendData;
            }
        }

        private async void SendData() {
            await Send();
        }

        private async System.Threading.Tasks.Task Send() {
            await System.Threading.Tasks.Task.Run(() => {
                Thread.Sleep(2000);
            });
        }


        private bool _blurActivate { get; set; }
        public bool blurActivate { get { return _blurActivate; } set { _blurActivate = value; RaisePropertyChanged(); } }


        private string _Vorname { get; set; }
        public string Vorname { get { return _Vorname; } set { _Vorname = value; RaisePropertyChanged(); } }


        private string _Nachname { get; set; }
        public string Nachname { get { return _Nachname; } set { _Nachname = value; RaisePropertyChanged(); } }


        private string _Street { get; set; }
        public string Street { get { return _Street; } set { _Street = value; RaisePropertyChanged(); } }


        private string _ZIP { get; set; }
        public string ZIP { get { return _ZIP; } set { _ZIP = value; RaisePropertyChanged(); } }


        private string _Location { get; set; }
        public string Location { get { return _Location; } set { _Location = value; RaisePropertyChanged(); } }


        private string _Strasse { get; set; }
        public string Strasse { get { return _Strasse; } set { _Strasse = value; RaisePropertyChanged(); } }


        private string _Hausnummer { get; set; }
        public string Hausnummer { get { return _Hausnummer; } set { _Hausnummer = value; RaisePropertyChanged(); } }


        private string _UserName { get; set; }
        public string UserName { get { return _UserName; } set { _UserName = value; RaisePropertyChanged(); } }


        private string _FullName { get; set; }
        public string FullName  { get { return _FullName; } set { _FullName = value; RaisePropertyChanged(); } }


        private string _Email { get; set; }
        public string Email { get { return _Email; } set { _Email = value; RaisePropertyChanged(); } }


        private string _Password { get; set; }
        public string Password { get { return _Password; } set { _Password = value; RaisePropertyChanged(); } }


        private string _Password2 { get; set; }
        public string Password2 { get { return _Password2; } set { _Password2 = value; RaisePropertyChanged(); } }
    }
}