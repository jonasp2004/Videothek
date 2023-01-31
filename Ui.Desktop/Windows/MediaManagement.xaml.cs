using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ui.Desktop.Windows {

    public partial class MediaManagement : Window {
        public MediaManagement() {
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

        private void ell_closeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_closeWindow.Fill = Brushes.DarkRed; }

        private void ell_closeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_closeWindow.Fill = Brushes.Red; }

        // Gelber Button-Verhalten
        private void ell_minimizeWindow_MouseDown(object sender, MouseButtonEventArgs e) { this.WindowState = WindowState.Minimized; }

        private void ell_minimizeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_minimizeWindow.Fill = Brushes.Orange; }

        private void ell_minimizeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_minimizeWindow.Fill = Brushes.Yellow; }

        // Grüner Button-Verhalten
        private void ell_maximizeWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            try {
                if (this.WindowState == WindowState.Normal) {
                    this.WindowState = WindowState.Maximized;
                    windowBorder.Margin = new Thickness(0);
                } else {
                    this.WindowState = WindowState.Normal;
                    windowBorder.Margin = new Thickness(10);
                }
            }
            catch { }
        }

        private void ell_maximizeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_maximizeWindow.Fill = Brushes.DarkGreen; }

        private void ell_maximizeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_maximizeWindow.Fill = Brushes.Green; }

    }
}
