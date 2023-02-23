using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ui.Desktop.Frames;
using Ui.Desktop.Windows;
using Ui.Logic.ViewModel;

namespace Ui.Desktop {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            NavigateTo();
            Messenger.Default.Register<NavigationMessage>(this, msg => {
                NavigateTo(msg.Page);
            });
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
        private async void ell_closeWindow_MouseUp(object sender, MouseButtonEventArgs e) { await Timer(200); Environment.Exit(0); }

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

        private void mi_search_Click(object sender, RoutedEventArgs e) {
            Search search = new Search();
            search.Show();
        }

        private void btn_myAccount_Click(object sender, RoutedEventArgs e) {
            AccountWindow account = new AccountWindow();
            account.ShowDialog();
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

        private async void cb_updateAvailable_Loaded(object sender, RoutedEventArgs e) {
            await Timer(5000);
            cb_updateAvailable.IsChecked = false;
        }

        private void NavigateTo(string page = null) {
            switch (page) {
                case "about":
                    About about = new About();
                    about.ShowDialog();
                    break;
                case "updater":
                    Updater update = new Updater();
                    update.ShowDialog();
                    break;
                case "usermanager":
                    UserManagement usrmgmnt = new UserManagement();
                    usrmgmnt.ShowDialog();
                    break;
                case "mediamanager":
                    MediaManagement mediamgmnt = new MediaManagement();
                    mediamgmnt.ShowDialog();
                    break;
                case "login":
                    Login login = new Login();
                    login.ShowDialog();
                    MainPageLoggedIn mainPageLoggedIn = new MainPageLoggedIn();
                    fr_Main.Content = mainPageLoggedIn;
                    grd_loggedIn.Visibility = Visibility.Visible;
                    grd_loggedOut.Visibility = Visibility.Collapsed;
                    loggedIn = true;
                    break;
                case "logout":
                    Logout logout = new Logout();
                    logout.ShowDialog();
                    MainPage mainPage = new MainPage();
                    fr_Main.Content = mainPage;
                    grd_loggedIn.Visibility = Visibility.Collapsed;
                    grd_loggedOut.Visibility = Visibility.Visible;
                    loggedIn = false;
                    break;
                case "register":
                    Register register = new Register();
                    register.ShowDialog();
                    break;
                case "alleArtikel":
                    Offerings offers = new Offerings(loggedIn);
                    fr_Main.Content = offers;
                    break;
                default:
                    if (loggedIn) {
                        fr_Main.Content = new MainPageLoggedIn();
                    } else {
                        fr_Main.Content = new MainPage();
                    }
                    break;
            }
        }
    }
}
