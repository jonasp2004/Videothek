using System;
using System.Windows.Controls;

namespace Ui.Desktop.Frames {

    public partial class MyContent : Page {
        public MyContent() { 
            InitializeComponent();
        }

        public MainWindow Delegate {  get; set; }
    }
}
