﻿using System.Windows;
using System.Windows.Controls;
using Ui.Desktop.Windows;

namespace Ui.Desktop.Frames {

    public partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
        }

        private void btn_loginAsCustomer_Click(object sender, RoutedEventArgs e) {
            Login login = new Login();
            login.Show();
        }
    }
}