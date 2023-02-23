using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ui.Desktop.Windows {

    public partial class Logout : Window {
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
            await Timer(200);
            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            await Timer(200);
            this.Close();
        }
    }
}
