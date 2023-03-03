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
            txt_currVersion.Text = Properties.Settings.Default.thisVersion;
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
            Search search = new Search(loggedIn,Benutzernummer);
            search.ShowDialog();
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

        private int Benutzernummer = -1;
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
                    try {
                        UserManagement usrmgmnt = new UserManagement();
                        usrmgmnt.ShowDialog();
                    } catch (Exception ex) {
                        Errormsg err = new Errormsg(ex.Message);
                    }
                    break;
                case "mediamanager":
                    MediaManagement mediamgmnt = new MediaManagement();
                    mediamgmnt.ShowDialog();
                    break;
                case "login":
                    Login login = new Login();
                    if ((bool)login.ShowDialog() == true) {
                        ((MainViewModel)(this.DataContext)).UserName = "@" + login.userName;
                        ((MainViewModel)(this.DataContext)).FullName = login.fullName;
                        if (login.isAdmin == 1) {
                            mi_management.Visibility = Visibility.Visible;
                            mi_management.IsEnabled = true;
                        }
                        Benutzernummer = login.userId;
                        loggedIn = true;
                        MainPageLoggedIn mainPageLoggedIn = new MainPageLoggedIn();
                        fr_Main.Content = mainPageLoggedIn;
                        grd_loggedIn.Visibility = Visibility.Visible;
                        grd_loggedOut.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "logout":
                    Logout logout = new Logout();
                    if((bool)logout.ShowDialog() == true) {
                        MainPage mainPage = new MainPage();
                        Benutzernummer = -1;
                        mi_management.Visibility = Visibility.Collapsed;
                        mi_management.IsEnabled = false;
                        fr_Main.Content = mainPage;
                        grd_loggedIn.Visibility = Visibility.Collapsed;
                        grd_loggedOut.Visibility = Visibility.Visible;
                        loggedIn = false;
                    }
                    
                    break;
                case "register":
                    Register register = new Register();
                    register.ShowDialog();
                    break;
                case "alleArtikel":
                    Offerings offers = new Offerings(loggedIn, Benutzernummer);
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

        private void btn_endProgram_Click(object sender, RoutedEventArgs e) { Environment.Exit(0); }

        private void btn_hideNoInternet_Click(object sender, RoutedEventArgs e) { grd_noInternet.Visibility = Visibility.Collapsed; }
    }
}
