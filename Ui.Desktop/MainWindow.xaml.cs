using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Ui.Desktop.Windows;

namespace Ui.Desktop {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }


        // Wenn linke Maustaste gedrückt ist, dann verschiebe Fenster gemäß Mauszeiger
        private void windowTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if(Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }


        // Roter Button-Verhalten
        private void ell_closeWindow_MouseDown(object sender, MouseButtonEventArgs e) { Environment.Exit(0); }

        private void ell_closeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_closeWindow.Fill = Brushes.DarkRed; }

        private void ell_closeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_closeWindow.Fill = Brushes.Red; }

        // Gelber Button-Verhalten
        private void ell_minimizeWindow_MouseDown(object sender, MouseButtonEventArgs e) { this.WindowState = WindowState.Minimized; }

        private void ell_minimizeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_minimizeWindow.Fill = Brushes.Orange; }

        private void ell_minimizeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_minimizeWindow.Fill = Brushes.Yellow; }

        // Grüner Button-Verhalten
        private void ell_maximizeWindow_MouseDown(object sender, MouseButtonEventArgs e) {
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

        private void ell_maximizeWindow_MouseEnter(object sender, MouseEventArgs e) { ell_maximizeWindow.Fill = Brushes.DarkGreen; }

        private void ell_maximizeWindow_MouseLeave(object sender, MouseEventArgs e) { ell_maximizeWindow.Fill = Brushes.Green; }

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
    }
}
