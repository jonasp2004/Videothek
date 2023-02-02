using System;
using System.Windows.Controls;
using Ui.Desktop.Windows;

namespace Ui.Desktop.Frames {

    public partial class Offerings : Page {
        public Offerings() {
            InitializeComponent();
        }

        private void grd_showContents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ContentDetail detail = new ContentDetail();
            detail.Show();
        }
    }
}
