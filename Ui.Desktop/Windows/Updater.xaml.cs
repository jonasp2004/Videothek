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

namespace Ui.Desktop.Windows {

    public partial class Updater : Window {
        public Updater() {
            InitializeComponent();
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
    }
}
