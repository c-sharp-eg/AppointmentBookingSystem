﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace loginPage
{
    /// <summary>
    /// Interaction logic for pastAppointments.xaml
    /// </summary>
    public partial class pastAppointments : Window
    {
        searchPatient searchPat = new searchPatient();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            searchPat.Close();
        }

        public pastAppointments()
        {
            InitializeComponent();
        }

        private void pastCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pastSelectButton_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            searchPat = new searchPatient(this);
            searchPat.Show();
        }
    }
}
