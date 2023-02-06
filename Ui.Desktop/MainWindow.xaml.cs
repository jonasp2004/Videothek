using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Ui.Desktop.Frames;
using Ui.Desktop.Windows;

namespace Ui.Desktop {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        public MainWindow Delegate { get; set; }

        private bool loggedIn = false;


        // Wenn linke Maustaste gedrückt ist, dann verschiebe Fenster gemäß Mauszeiger
        private void windowTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if(Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        protected async Task Timer(int duration) {
            await Task.Delay(duration);
        }

        // Roter Button-Verhalten
        private async void ell_closeWindow_MouseUp(object sender, MouseButtonEventArgs e) { await Timer(150); Environment.Exit(0); }

        // Gelber Button-Verhalten
        private void ell_minimizeWindow_MouseUp(object sender, MouseButtonEventArgs e) { this.WindowState = WindowState.Minimized; }

        // Grüner Button-Verhalten
        private void ell_maximizeWindow_MouseUp(object sender, MouseButtonEventArgs e) {
            try {
                if(this.WindowState == WindowState.Normal) {
                    this.WindowState = WindowState.Maximized;
                    windowBorder.Margin = new Thickness(7);
                } else {
                    this.WindowState = WindowState.Normal;
                    windowBorder.Margin = new Thickness(10);
                }
            } catch { }
        }

        // Fenster öffnen
        private void mi_about_Click(object sender, RoutedEventArgs e) {
            About about =new About();
            about.Show();
        }

        private void mi_manageUsers_Click(object sender, RoutedEventArgs e) {
            UserManagement user = new UserManagement();
            user.Show();
        }

        private void mi_search_Click(object sender, RoutedEventArgs e) {
            Search search = new Search();
            search.Show();
        }

        private void mi_manageMedia_Click(object sender, RoutedEventArgs e) {
            MediaManagement media = new MediaManagement();
            media.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Login login = new Login();
            login.ShowDialog();
            MainPageLoggedIn mainPageLoggedIn= new MainPageLoggedIn();
            fr_Main.Content = mainPageLoggedIn;
            grd_loggedIn.Visibility = Visibility.Visible;
            grd_loggedOut.Visibility = Visibility.Collapsed;
            loggedIn = true;
        }

        private void mi_logout_Click(object sender, RoutedEventArgs e) {
            Logout logout = new Logout();
            logout.ShowDialog();
            MainPage mainPage = new MainPage();
            fr_Main.Content = mainPage;
            grd_loggedIn.Visibility = Visibility.Collapsed;
            grd_loggedOut.Visibility = Visibility.Visible;
            loggedIn = false;
        }

        private void btn_register_Click(object sender, RoutedEventArgs e) {
            Register register = new Register();
            register.ShowDialog();
        }

        private void btn_myAccount_Click(object sender, RoutedEventArgs e) {
            AccountWindow account = new AccountWindow();
            account.ShowDialog();
        }

        private void mi_alleMedien_Click(object sender, RoutedEventArgs e) {
            Offerings offers = new Offerings();
            fr_Main.Content = offers;
        }

        private void mi_start_Click(object sender, RoutedEventArgs e) {
            if(loggedIn) {
                MainPageLoggedIn main = new MainPageLoggedIn();
                fr_Main.Content = main;
            } else {
                MainPage main = new MainPage();
                fr_Main.Content = main;
            }
        }

        private void mi_myContents_Click(object sender, RoutedEventArgs e) {
            if(loggedIn) {
                MyContent myContent = new MyContent();
                myContent.Delegate = this;
                fr_Main.Content = myContent;
            } else {
                Login login = new Login();
                login.ShowDialog();
                grd_loggedIn.Visibility = Visibility.Visible;
                grd_loggedOut.Visibility = Visibility.Collapsed;
                loggedIn = true;
            }
        }

        private void mi_update_Click(object sender, RoutedEventArgs e) {
            Updater update = new Updater();
            update.Show();
        }
    }
}
