using System;
using System.Windows;
using System.Windows.Controls;
using Ui.Desktop.Windows;

namespace Ui.Desktop.Frames {

    public partial class Offerings : Page {

        protected bool loggedIn = false;
        public Offerings(bool loggedin) {
            InitializeComponent();
            loggedIn= loggedin;

        }

        private void grd_showContents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if(grd_showContents.SelectedItem != null) {
                    TextBlock idCell = (TextBlock)grd_showContents.SelectedCells[0].Column.GetCellContent(grd_showContents.SelectedItem);
                    TextBlock nameCell = (TextBlock)grd_showContents.SelectedCells[1].Column.GetCellContent(grd_showContents.SelectedItem);
                    TextBlock priceCell = (TextBlock)grd_showContents.SelectedCells[3].Column.GetCellContent(grd_showContents.SelectedItem);

                    ContentDetail detail = new ContentDetail(idCell.Text, nameCell.Text, priceCell.Text, loggedIn);
                    detail.ShowDialog();
                }
                
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
