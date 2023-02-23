using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ui.Desktop.Frames;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Windows {

    public partial class Updater : Window {
        public Updater() {
            InitializeComponent();
            Messenger.Default.Register<NavigationMessage>(this, msg => {
                NavigateTo(msg.Page);
            });
            txt_thisVersion.Text = "Diese Version: " + Properties.Settings.Default.thisVersion;
            txt_lastUpdateChecked.Text = "Letzter Update-Check: " + Properties.Settings.Default.lastUpdated;
        }

        private void txt_windowTitle_MouseDown(object sender, MouseButtonEventArgs e) {
            if(Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.lastUpdated = DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy");
            Properties.Settings.Default.Save();
            txt_lastUpdateChecked.Text = "Letzter Update-Check: " + Properties.Settings.Default.lastUpdated;
        }

        private async void ell_closeWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            await Task.Delay(200);
            this.Close();
        }

        private void NavigateTo(string page = null) {
            switch (page) {
                case "updateTheHelperText!":
                    if (txt_newestVersion.Text.Replace("Aktuellste Version: ", "") == Properties.Settings.Default.thisVersion)
                    {
                        txt_updateHelperText.Text = "Du verwendest bereits die neuste Version!";
                    }
                    else if (txt_newestVersion.Text.Replace("Aktuellste Version: ", "") == "") {
                        txt_updateHelperText.Text = "Beim updaten ist ein unbekannter Fehler aufgetreten!";
                    } else { 
                        txt_updateHelperText.Text = "Es steht eine neue Version zum Downlaod bereit.";
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
