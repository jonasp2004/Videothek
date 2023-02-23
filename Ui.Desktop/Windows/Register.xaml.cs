using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class Register : Window {
        public Register() {
            InitializeComponent();
        }

        private void txt_agb_MouseDown(object sender, MouseButtonEventArgs e) {
            MessageBox.Show("Rechtlich konformer Text...\nBli, Bla, Blubb usw. Ich mag Käse.", "Allgemeine Geschäftsbedingungen", MessageBoxButton.OK);
        }

        private void txt_dsgvo_MouseDown(object sender, MouseButtonEventArgs e) {
            MessageBox.Show("Wir werden alles über Sie herausfinden, ihre Kreditkartendaten, Ihren Führerschein, Ihre Adresse, Ihre Familie und Freunde, Ihre Tätigkeiten und Interessen. Wir sind schlimmer als die chinesische Regierung und deshalb verkaufen wir sogar alle Ihre Daten in die USA..", "Datenschutzerklärung", MessageBoxButton.OK);
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

        private void pw_1_PasswordChanged(object sender, RoutedEventArgs e) {
            PasswordCheck();
            (this.DataContext as RegisterViewModel).Password2 = ((PasswordBox)sender).Password;
        }

        private void pw_2_PasswordChanged(object sender, RoutedEventArgs e) {
            PasswordCheck();
            (this.DataContext as RegisterViewModel).Password = ((PasswordBox)sender).Password;
        }

        protected void PasswordCheck() {
            if (pw_1.Password != pw_2.Password) {
                txt_nonMatchingPasswords.Visibility = Visibility.Visible;
                txt_nonMatchingPasswords.Text = "Die Passwörter stimmen nicht überein. Das Passwort im unteren Eingabefeld wird somit übernommen.";
            } else {
                if(pw_2.Password == "" && pw_1.Password == "") {
                    txt_nonMatchingPasswords.Visibility = Visibility.Visible;
                    txt_nonMatchingPasswords.Text = "Die Passworteingabe darf nicht leer sein!";
                } else {
                    txt_nonMatchingPasswords.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
