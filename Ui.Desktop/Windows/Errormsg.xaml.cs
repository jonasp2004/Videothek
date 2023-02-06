using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ui.Desktop.Windows {

    public partial class Errormsg : Window {
        public Errormsg() {
            InitializeComponent();
        }

        private void btn_copyToClipboard_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(txt_error.Text);
            btn_copyToClipboard.Background = Brushes.Green;
            btn_copyToClipboard.Foreground = Brushes.White;
        }

        internal async Task Timer(int duration) {
            await Task.Delay(duration);
        }

        private async void ell_closeWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            await Timer(200);
            Close();
        }

        private void windowTitle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if(Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }
    }
}
