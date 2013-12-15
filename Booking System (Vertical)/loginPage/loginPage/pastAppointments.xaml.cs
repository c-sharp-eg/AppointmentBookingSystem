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
    /// Interaction logic for pastAppointments.xaml
    /// </summary>
    public partial class pastAppointments : Window
    {
        public Patient selectedPatient;
        public Boolean patientSelected;
        searchPatient searchPat = new searchPatient();
        public mainCalendarDisplayWindow caller;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            searchPat.Close();
        }

        public pastAppointments()
        {
            InitializeComponent();
            patientSelected = false;
        }

        public pastAppointments(mainCalendarDisplayWindow caller)
        {
            this.caller = caller;
            InitializeComponent();
            patientSelected = false;
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

        public void ShowPatient(Patient p)
        {
            foreach (Appointment a in p.appointments)
            {
                pastApptLB.Items.Add(a);
            }
        }
    }
}
