using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading;
using System;
using System.Linq;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;

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
            WrongTextVisibility = Visibility.Collapsed;
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

        public bool isLoggedIn = false;
        private async void SendData() {
            WaitGridVisibility = Visibility.Visible;
            blurActivate = true;
            await Send();
            blurActivate = false;
            WaitGridVisibility = Visibility.Collapsed;
            
        }

        public string FullName;
        public int UserId = -1;
        private async System.Threading.Tasks.Task Send() {
            try {
                await System.Threading.Tasks.Task.Run(async () => {
                    using (videothek_development db = new videothek_development()) {
                        string hashedPw = Hash.ComputeSHA256(Password);
                        await System.Threading.Tasks.Task.Delay(100);
                        // Holt Kontodetails
                        var login = db.UserAccount
                            .Select(n => new { n.CustomerId, n.UserName, n.Password, n.IsAdmin })
                            .Where(q => q.UserName.Equals(Benutzername) && q.Password.Equals(hashedPw.ToString())).SingleOrDefault();

                        // Falls Nutzer existiert
                        if(login != null) {
                            // Hole genauere Details für Kunden
                            UserId = Convert.ToInt32(login.CustomerId);
                            if (login.IsAdmin == 0 || login.IsAdmin == null) {
                                UserIsAdmin = -1;
                            } else if (login.IsAdmin == 1) {
                                UserIsAdmin = 1;
                            }
                            var user = db.Customer
                                .Select(c => new { c.Id, c.FirstName, c.LastName })
                                .Where(q => q.Id.Equals(UserId)).SingleOrDefault();
                            FullName = user.FirstName + " " + user.LastName;
                                
                        } else {
                            isLoggedIn = false;
                            WrongTextVisibility = Visibility.Visible;
                            UserId = -2;
                        }
                    }
                });
                if(UserId >= 1) {
                    WrongTextVisibility = Visibility.Collapsed;
                    MessageBox.Show("Hallo " + FullName + "! Du bist jetzt angemeldet!", "Hallo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    VollerName = FullName;
                    NutzerId = UserId;
                    isLoggedIn = true;
                }
            } catch (Exception ex) {
                MessageBox.Show("Ein nicht behandelter Fehler ist aufgetreten. Bitte versuche es später noch einmal!\n" + ex.Message);
                isLoggedIn = false;
            }
        }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; RaisePropertyChanged(); } }
        private string _Benutzername { get; set; }
        public string Benutzername { get { return _Benutzername; } set { _Benutzername = value; RaisePropertyChanged(); } }
        private string _VollerName { get; set; }
        public string VollerName { get { return _VollerName; } set { _VollerName = value; RaisePropertyChanged(); } }
        private int _NutzerId { get; set; }
        public int NutzerId { get { return _NutzerId; } set { _NutzerId = value; RaisePropertyChanged(); } }

        public int UserIsAdmin = -1;

        private bool _blurActivate { get; set; }
        public bool blurActivate { get { return _blurActivate; } set { _blurActivate = value; RaisePropertyChanged(); } }
        private Visibility _WaitGridVisibility { get; set; }
        public Visibility WaitGridVisibility { get { return _WaitGridVisibility; } set { _WaitGridVisibility = value; RaisePropertyChanged(); } }
        private Visibility _WrongTextVisibility { get; set; }
        public Visibility WrongTextVisibility { get { return _WrongTextVisibility; } set { _WrongTextVisibility = value; RaisePropertyChanged(); } }
    }
}