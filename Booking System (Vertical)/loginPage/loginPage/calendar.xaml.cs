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
        MainWindow loginScreen = new MainWindow();
        billing billingScreen = new billing();
        addPatient newPat = new addPatient();
        editPatient editPat = new editPatient();
        searchPatient searchPat = new searchPatient();
        pastAppointments pastAppt = new pastAppointments();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        public calendar()
        {
            InitializeComponent();

            monthCalendar.SelectedDate = DateTime.Now;
            dateBox.SelectedDate = DateTime.Now;
            bookDate.SelectedDate = DateTime.Now;
        }

        //logging out in file menu
        private void fileLogOut_Click(object sender, RoutedEventArgs e)
        {
            loginScreen = new MainWindow();
            loginScreen.Show();

            this.Hide();
            billingScreen.Close();
            newPat.Close();
            editPat.Close();
            searchPat.Close();
            pastAppt.Close();
        }

        //closing program from file menu
        private void fileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //opens the billing window (message)
        private void billOpen_Click(object sender, RoutedEventArgs e)
        {
            billingScreen.Close();
            billingScreen = new billing();
            billingScreen.Show();
        }

        private void monthCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            dateBox.SelectedDate = monthCalendar.SelectedDate;
        }

        //right arrow button for next day
        private void nextDay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateTime currentDate = monthCalendar.SelectedDate.GetValueOrDefault();
            monthCalendar.SelectedDate = currentDate.AddDays(1);
            //this "refreshes" the month calendar
            monthCalendar.DisplayDate = currentDate.AddDays(1);
        }

        //left arrow button for previous day
        private void previousDay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateTime currentDate = monthCalendar.SelectedDate.GetValueOrDefault();
            monthCalendar.SelectedDate = currentDate.AddDays(-1);
            //this "refreshes" the month calendar
            monthCalendar.DisplayDate = currentDate.AddDays(-1);
        }

        private void dateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            monthCalendar.SelectedDate = dateBox.SelectedDate;
        }

        private void patientNew_Click(object sender, RoutedEventArgs e)
        {
            newPat.Close();
            newPat = new addPatient();
            newPat.Show();
        }

        private void patientEdit_Click(object sender, RoutedEventArgs e)
        {
            editPat.Close();
            editPat = new editPatient();
            editPat.Show();
        }

        private void patientPast_Click(object sender, RoutedEventArgs e)
        {
            pastAppt.Close();
            pastAppt = new pastAppointments();
            pastAppt.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            searchPat = new searchPatient();
            searchPat.Show();
        }

        private void bookClearButton_Click(object sender, RoutedEventArgs e)
        {
            bookNewName.Text = "<patient name>";
            bookDoctor.Text = "";
            bookDate.SelectedDate = DateTime.Now; 
            bookTime.Text = "";
            bookDouble.IsChecked = false;
        }


    }
}
