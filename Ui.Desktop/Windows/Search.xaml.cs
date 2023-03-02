using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Ui.Desktop.Windows {

    public partial class Search : Window {

        private bool loggedIn = false;
        private int Benutzernummer = -1;
        public Search(bool isLoggedIn, int userId) {
            InitializeComponent();
            loggedIn = isLoggedIn;
            Benutzernummer = userId;
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

        private void dg_searchResults_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) {
            try {
                if (dg_searchResults.SelectedItem != null) {
                    TextBlock idCell = (TextBlock)dg_searchResults.SelectedCells[0].Column.GetCellContent(dg_searchResults.SelectedItem);
                    TextBlock nameCell = (TextBlock)dg_searchResults.SelectedCells[1].Column.GetCellContent(dg_searchResults.SelectedItem);
                    TextBlock priceCell = (TextBlock)dg_searchResults.SelectedCells[3].Column.GetCellContent(dg_searchResults.SelectedItem);
                    ContentDetail detail = new ContentDetail(idCell.Text, nameCell.Text, priceCell.Text, loggedIn, Benutzernummer);
                    detail.ShowDialog();
                }
            } catch (Exception ex) {
                Errormsg err = new Errormsg(ex.Message);
                err.Show();
            }
        }
    }
}
