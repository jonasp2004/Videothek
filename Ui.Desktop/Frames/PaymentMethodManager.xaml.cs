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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ui.Desktop.Windows;
using Ui.Logic.ViewModel;

namespace Ui.Desktop.Frames {

    public partial class PaymentMethodManager : Page {
        public PaymentMethodManager() {
            InitializeComponent();
        }

        private void ell_closeFrame_MouseDown(object sender, MouseButtonEventArgs e) {
            ((PaymentViewModel)(ell_closeFrame.DataContext)).IsFrameBlurred = false;
        }
    }
}
