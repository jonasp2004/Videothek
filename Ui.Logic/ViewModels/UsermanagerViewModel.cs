using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Ui.Logic.ViewModel {

    public class UsermanagerViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public UsermanagerViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Benutzerverwaltung (Designer-Modus)";
            } else {
                WindowTitle = "Benutzerverwaltung";
            }

            Users = new ObservableCollection<UserList> {
                new UserList(1, 1, "jopraast2004", "jonas.praast@tsbw.cloud", "Testpasswort", "2004-09-05"),
                new UserList(2, 2, "joprast2004", "jonas.prasst@tsbw.cloud", "Testpasswort123", "")
            };
        }

        private ObservableCollection<UserList> _Users { get; set; }
        public ObservableCollection<UserList> Users { get { return _Users; } set { _Users = value; RaisePropertyChanged(); } }
        public class UserList {
            public UserList(int accountId, int customerId, string userName, string email, string password, string lastLoginDate) {
                AccountID = accountId;
                KundenID = customerId;
                Nutzername = userName;
                Email = email;
                Passwort = password;
            }

            public int AccountID { get; set; }
            public int KundenID { get; set; }
            public string Nutzername { get; set; }
            public string Email { get; set; }
            public string Passwort { get; set; }
            public string LetzterLogin { get; set; }
        }
    }
}