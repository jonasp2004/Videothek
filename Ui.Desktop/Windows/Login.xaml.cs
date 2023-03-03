using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class Login : Window {
        public int userId {
            get { return (this.DataContext as LoginViewModel).NutzerId; }
        }

        public int isAdmin {
            get { return (this.DataContext as LoginViewModel).UserIsAdmin; }
        }

        public string fullName {
            get { return (this.DataContext as LoginViewModel).VollerName; }
        }
        public string userName {
            get { return (this.DataContext as LoginViewModel).Benutzername; }
        }


        public Login() {
            InitializeComponent();
        }

        private void windowTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private async void ell_closeWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            await Task.Delay(200);
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
            this.Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            Register register = new Register();
            register.ShowDialog();
        }

        private void txt_password_PasswordChanged(object sender, RoutedEventArgs e) {
            (this.DataContext as LoginViewModel).Password = ((PasswordBox)sender).Password;
        }

        private async void cb_closeWindow_Checked(object sender, RoutedEventArgs e) {
            // Login-Prozedur befindet sich im ViewModel, das hier ist für das Schließen des Fensters
            if(((LoginViewModel)(this.DataContext)).isLoggedIn == true) {
                await Task.Delay(200);
                Window.GetWindow(this).DialogResult = true;
                Window.GetWindow(this).Close();
            }
        }

        private void cb_enableBlur_Unchecked(object sender, RoutedEventArgs e) {
            if (((LoginViewModel)(this.DataContext)).isLoggedIn == true) {
                cb_closeWindow.IsChecked = true;
            }
        }

        private void img_lock_MouseEnter(object sender, MouseEventArgs e)  { grd_securityAlert.Visibility = Visibility.Visible; }

        private async void img_lock_MouseLeave(object sender, MouseEventArgs e) {
            await Task.Delay(100);
            grd_securityAlert.Visibility = Visibility.Collapsed;
        }
    }
}
