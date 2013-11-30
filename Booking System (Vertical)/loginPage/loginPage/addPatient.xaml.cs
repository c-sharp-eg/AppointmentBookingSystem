using System;
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
    /// Interaction logic for addPatient.xaml
    /// </summary>
    public partial class addPatient : Window
    {
        public addPatient()
        {
            InitializeComponent();
        }

        private void addSaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
