using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class Logout : Window {

        public bool loggedOut {
            get { return false; }
            set { }
        }

        public Logout() {
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


        private async void btn_logOut_Click(object sender, RoutedEventArgs e) {
            // Abmelden
            loggedOut = true;
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
            await Timer(200);
            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            await Timer(200);
            this.Close();
        }

        private void img_lock_MouseEnter(object sender, MouseEventArgs e) { grd_securityAlert.Visibility = Visibility.Visible; }

        private async void img_lock_MouseLeave(object sender, MouseEventArgs e) {
            await Task.Delay(100);
            grd_securityAlert.Visibility = Visibility.Collapsed;
        }
    }
}
