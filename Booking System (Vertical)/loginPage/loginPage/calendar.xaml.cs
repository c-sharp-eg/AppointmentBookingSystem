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
    /// Interaction logic for calendar.xaml
    /// </summary>
    public partial class calendar : Window
    {
        public calendar()
        {
            InitializeComponent();
        }

        //logging out in file menu
        private void fileLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginScreen = new MainWindow();
            loginScreen.Show();
            this.Close();
        }

        //closing program from file menu
        private void fileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void billOpen_Click(object sender, RoutedEventArgs e)
        {
            billing billingScreen = new billing();
            billingScreen.Show();
        }
    }
}
