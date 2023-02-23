using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class Login : Window
    {
        public Login() {
            InitializeComponent();
        }

        private void windowTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        protected async Task Timer(int duration) {
            await Task.Run(() => {
                Thread.Sleep(duration);
            });
        }

        private async void ell_closeWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            await Timer(200);
            this.Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            Register register = new Register();
            register.ShowDialog();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            // Login kommt hier (oder im ViewModel)
            await Timer(200);
            this.Close();
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            await Timer(200);
            this.Close();
        }

        private void txt_password_PasswordChanged(object sender, RoutedEventArgs e) {
            (this.DataContext as LoginViewModel).Password = ((PasswordBox)sender).Password;
        }
    }
}
