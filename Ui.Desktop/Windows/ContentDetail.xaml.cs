using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ui.Desktop.Frames;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class ContentDetail : Window {
        private int Benutzernummer = -1;
        public ContentDetail(string id, string itemName, string price, bool loggedIn, int userId) {
            InitializeComponent();
            Benutzernummer = userId;


            Messenger.Default.Register <UIMessage>(this, msg => {
                MessengerAction(msg.Action);
            });

            txt_articleNbr.Text = "Art.-Nummer: " + id;
            ((CurrentArticleViewModel)(this.DataContext)).Artikelnummer = id;
            ((CurrentArticleViewModel)(this.DataContext)).FetchData();

            txt_titleBarText.Text = itemName;
            txt_price.Text = price;
            if(!loggedIn) {
                txt_rentalWarning.Visibility = Visibility.Visible;
                btn_orderRental.IsEnabled = false;
                img_lock.Visibility = Visibility.Collapsed;
            } else {
                txt_rentalWarning.Visibility = Visibility.Collapsed;
                btn_orderRental.IsEnabled = true;
            }
            LoadImage(id.ToString());
            contentId = Convert.ToInt32(id);
        }
        protected int contentId;

        private void MessengerAction(string message = null) {
            switch (message) {
                case "paymentInProgress":
                    btn_hideOrderView.IsEnabled = false;
                    break;
                case "paymentProgressDone":
                    btn_hideOrderView.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        public async void SetArticleNumber(string text) {
            await Task.Delay(500);
            txt_articleNbr.Text = text;
        }

        public BitmapImage bitmapImage = new BitmapImage();
        public async void LoadImage(string id) {
            try {
                await Task.Delay(500);
                var backgroundURI = @"http://10.33.11.55/covers/" + id + ".png";
                await Task.Run(() => {
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(backgroundURI, UriKind.Absolute);
                    bitmapImage.EndInit();
                });
                img_cover.Source = bitmapImage;
            } catch { }
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

        private async void btn_hideOrderView_Click(object sender, RoutedEventArgs e) {
            await Task.Delay(500);
            brdr_confirmRental.Visibility = Visibility.Collapsed;
        }

        // ToDo: IDs bei PamentFrame variieren je nach Nutzer - 11 Ist der Testnutzer der Alpha-Version
        private void btn_orderRental_Click(object sender, RoutedEventArgs e) {
            brdr_confirmRental.Visibility = Visibility.Visible;
            PaymentFrame pf = new PaymentFrame(contentId, 11);
            paymentView.Content = pf;
        }

        private void img_lock_MouseEnter(object sender, MouseEventArgs e) { grd_securityAlert.Visibility = Visibility.Visible; }

        private async void img_lock_MouseLeave(object sender, MouseEventArgs e) {
            await Task.Delay(100);
            grd_securityAlert.Visibility = Visibility.Collapsed;
        }
    }
}
