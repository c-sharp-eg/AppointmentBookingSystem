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
    /// Interaction logic for editPatient.xaml
    /// </summary>
    public partial class editPatient : Window
    {
        searchPatient searchPat = new searchPatient();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            searchPat.Close();
        }

        public editPatient()
        {
            InitializeComponent();
        }

        private void editCancelButton_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            this.Close();
        }

        private void editSelectButton_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            searchPat = new searchPatient();
            searchPat.Show();
        }



    }
}
