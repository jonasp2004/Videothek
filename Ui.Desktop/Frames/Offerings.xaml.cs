using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Ui.Desktop.Windows;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Frames {

    public partial class Offerings : Page {

        protected bool loggedIn = false;
        public Offerings(bool loggedin) {
            InitializeComponent();
            loggedIn= loggedin;

            Messenger.Default.Register<OpenContentDetail>(this, msg =>
            {
                OpenContentDetail(msg.Id, msg.Price, msg.ItemName);
            });
        }

        private int BenutzerId = -1;
        public Offerings(bool loggedin, int userId) {
            InitializeComponent();
            loggedIn = loggedin;
            BenutzerId = userId;
        }

        private void grd_showContents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if(grd_showContents.SelectedItem != null) {
                    TextBlock idCell = (TextBlock)grd_showContents.SelectedCells[0].Column.GetCellContent(grd_showContents.SelectedItem);
                    TextBlock nameCell = (TextBlock)grd_showContents.SelectedCells[1].Column.GetCellContent(grd_showContents.SelectedItem);
                    TextBlock priceCell = (TextBlock)grd_showContents.SelectedCells[3].Column.GetCellContent(grd_showContents.SelectedItem);

                    ContentDetail detail = new ContentDetail(idCell.Text, nameCell.Text, priceCell.Text, loggedIn, BenutzerId);
                    detail.ShowDialog();
                }
                
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenContentDetail(int id, string price, string name) {
            ContentDetail detail = new ContentDetail(id.ToString(), name, price, loggedIn, BenutzerId);
            detail.ShowDialog();
        }
    }
}
