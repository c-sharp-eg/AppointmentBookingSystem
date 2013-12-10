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
using System.Windows.Controls.Primitives;


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

        DataGridColumnHeader previousSlot;
        DataGridColumnHeader currentSlot;

        DataGridColumnHeader[] d0Slots = new DataGridColumnHeader[23];
        DataGridColumnHeader[][] dateSlots = new DataGridColumnHeader[31][];



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

            saveSlots();
            clearSlots();
        }

        //left arrow button for previous day
        private void previousDay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateTime currentDate = monthCalendar.SelectedDate.GetValueOrDefault();
            monthCalendar.SelectedDate = currentDate.AddDays(-1);
            //this "refreshes" the month calendar
            monthCalendar.DisplayDate = currentDate.AddDays(-1);

            loadSlots();
        }

        private void dateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            monthCalendar.SelectedDate = dateBox.SelectedDate;
            monthCalendar.DisplayDate = monthCalendar.SelectedDate.GetValueOrDefault();
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


        SolidColorBrush customGreen = new SolidColorBrush();
        LinearGradientBrush greenGradient = new LinearGradientBrush();

        private void calendarDatesClicked(object sender, EventArgs e)
        {
            customGreen.Color = Color.FromArgb(255, 175, 255, 0);

            greenGradient.StartPoint = new Point(0.5,0);
            greenGradient.EndPoint = new Point(0.5,1);
            greenGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen,0.0));
            greenGradient.GradientStops.Add(new GradientStop(Colors.Lime,1.0));

            DataGridColumnHeader calButton = sender as DataGridColumnHeader;
            previousSlot = currentSlot;
            currentSlot = calButton;

            if (previousSlot != null)
                previousSlot.BorderBrush = null;

            currentSlot.BorderBrush = Brushes.Aqua;

            if (currentSlot.Background == greenGradient)
            {
                checkInButton.Content = "Cancel Check - In";
            }
            else if (currentSlot.Background == null)
            {
                checkInButton.Content = "Check - In";
            }
 
        }

        private void checkInButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSlot != null)
            {
                if (currentSlot.Background == greenGradient)
                {
                    currentSlot.Background = null;
                    checkInButton.Content = "Check- In";
                }
                else if (currentSlot.Background == null)
                {
                    currentSlot.Background = greenGradient;
                    checkInButton.Content = "Cancel Check - In";
                }
            }
        }


        public void saveSlots()
        {
            d0Slots[0] = d0t0;
            dateSlots[0] = d0Slots;
        }

        public void clearSlots()
        {
            d0t0.Background = null;
        }

        public void loadSlots()
        {
            d0Slots = dateSlots[0];
            d0t0 = d0Slots[0];
        }


    }
}
