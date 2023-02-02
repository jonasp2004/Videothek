using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ui.Desktop.Windows {

    public partial class ContentDetail : Window {
        public ContentDetail() {
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

        private void ScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e) {
            blr_imageBlur.Radius = scr_textScroll.VerticalOffset / 5;
        }
    }
}
