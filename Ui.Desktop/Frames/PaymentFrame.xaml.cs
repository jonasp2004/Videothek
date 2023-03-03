using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Frames {

    public partial class PaymentFrame : Page {
        public PaymentFrame(int contentId, int userId) {
            InitializeComponent();

            ((PaymentViewModel)(this.DataContext)).ContentId = contentId;
            ((PaymentViewModel)(this.DataContext)).UserId = userId;
        }

        private void txt_dhlAGB_MouseDown(object sender, MouseButtonEventArgs e) {
            var uri = "https://www.dhl.de/de/privatkunden/information/agb.html";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }

        private void txt_gdpr_MouseDown(object sender, MouseButtonEventArgs e) {
            var uri = "https://www.dhl.de/de/toolbar/footer/datenschutz.html";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }

        private void cb_showChecking_Checked(object sender, RoutedEventArgs e) { brdr_waiting.Visibility = Visibility.Visible; }

        private async void cb_showChecking_Unchecked(object sender, RoutedEventArgs e) {
            await Task.Delay(100);
            brdr_waiting.Visibility = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            ((PaymentViewModel)(this.DataContext)).FetchData();
        }

        private void btn_choosePaymentMethod_Click(object sender, RoutedEventArgs e)  {
            cb_showChecking.IsChecked = true;
            PaymentMethodManager paymentMethodManager = new PaymentMethodManager();
            uc_additionalViews.Content = paymentMethodManager;
        }
    }
}
