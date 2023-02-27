using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class ContentDetail : Window {
        public ContentDetail(string id, string itemName, string price, bool loggedIn) {
            InitializeComponent();
            txt_articleNbr.Text = "Art.-Nummer: " + id;
            ((CurrentArticleViewModel)(this.DataContext)).Artikelnummer = id;
            ((CurrentArticleViewModel)(this.DataContext)).FetchData();

            txt_titleBarText.Text = itemName;
            txt_price.Text = price;
            if(!loggedIn) {
                txt_rentalWarning.Visibility = Visibility.Visible;
                btn_orderRental.IsEnabled = false;
            } else {
                txt_rentalWarning.Visibility = Visibility.Collapsed;
                btn_orderRental.IsEnabled = true;
            }

        }

        public async void SetArticleNumber(string text) {
            await Task.Delay(500);
            txt_articleNbr.Text = text;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            try {
                txt_titleBarText.Width = window.Width - 200;
            } catch { }
        }

        private void windowTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private async void ell_closeWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            await Task.Delay(200);
            this.Close();
        }

        private void ell_closeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_closeWindow.Fill = Brushes.DarkRed; }

        private void ell_closeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_closeWindow.Fill = Brushes.Red; }

        private void ScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e) {
            if(scr_textScroll.VerticalOffset >= 8) {
                cb_shrinkTitleBar.IsChecked = true;
                brdr_titleBar.Background = Brushes.Gainsboro;
                gs_shadow.Color = Colors.Gainsboro;
            } else if(scr_textScroll.VerticalOffset <= 8) {
                cb_shrinkTitleBar.IsChecked = false;
                brdr_titleBar.Background = Brushes.Transparent;
                gs_shadow.Color = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
            }
        }
    }
}
