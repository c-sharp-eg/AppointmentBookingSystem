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
    public partial class mainCalendarDisplayWindow : Window
    {
    
        //constants for our system
        public const int NUM_DOCTORS = 5;
        public const int NUM_TIMESLOTS = 23;

        public DateTime displayDate;

        //global variables
        public Dictionary<string, DayOfAppointments> allAppointmentsDictionary = new Dictionary<string,DayOfAppointments>();
        public List<Patient> patients = new List<Patient>();
        public Patient selectedPatient;

        MainWindow loginScreen = new MainWindow();
        billing billingScreen = new billing();
        addPatient newPat;
        editPatient editPat = new editPatient();
        searchPatient searchPat = new searchPatient();
        pastAppointments pastAppt = new pastAppointments();
        cancelConfirmation confCancel = new cancelConfirmation();

        DataGridColumnHeader previousSlot;
        DataGridColumnHeader currentSlot;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        public mainCalendarDisplayWindow()
        {
            InitializeComponent();

            displayDate = DateTime.Now;
            monthCalendar.SelectedDate = displayDate;
            dateBox.SelectedDate = displayDate;
            bookDate.SelectedDate = displayDate;
            checkInButton.IsEnabled = false;
            bookAddButton.IsEnabled = false;

            //just for display testing print random appointments
            addRandomPatients();
        }

        //logging out in file menu
        private void fileLogOut_Click(object sender, RoutedEventArgs e)
        {
            
            loginScreen.Show();

            this.Hide();
            billingScreen.Close();
            if (newPat != null)
                newPat.closeActivate();
            editPat.Close();
            searchPat.Close();
            pastAppt.Close();
            confCancel.Close();
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
            bookDate.SelectedDate = monthCalendar.SelectedDate;
            displayDate = ((DateTime)monthCalendar.SelectedDate).Date;
        }

        //right arrow button for next day
        private void nextDay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush whiteArrowFill = new SolidColorBrush();
            whiteArrowFill.Color = Color.FromArgb(255, 244, 244, 245);
            nextDay.Fill = whiteArrowFill;

            DateTime currentDate = monthCalendar.SelectedDate.GetValueOrDefault();
            monthCalendar.SelectedDate = currentDate.AddDays(1);
            //this "refreshes" the month calendar
            monthCalendar.DisplayDate = currentDate.AddDays(1);

        }

        private void nextDay_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush blueArrowFill = new SolidColorBrush();
            blueArrowFill.Color = Color.FromArgb(255, 0, 0, 139);
            nextDay.Fill = blueArrowFill;
        }


        private void previousDay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush blueArrowFill = new SolidColorBrush();
            blueArrowFill.Color = Color.FromArgb(255, 0, 0, 139);
            previousDay.Fill = blueArrowFill;
        }

        //left arrow button for previous day
        private void previousDay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush whiteArrowFill = new SolidColorBrush();
            whiteArrowFill.Color = Color.FromArgb(255, 244, 244, 245);
            previousDay.Fill = whiteArrowFill;

            DateTime currentDate = monthCalendar.SelectedDate.GetValueOrDefault();
            monthCalendar.SelectedDate = currentDate.AddDays(-1);
            //this "refreshes" the month calendar
            monthCalendar.DisplayDate = currentDate.AddDays(-1);

        }



        private void nextDay_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush blueArrowFill = new SolidColorBrush();
            blueArrowFill.Color = Color.FromArgb(255, 0, 0, 139);
            nextDay.Stroke = blueArrowFill;
        }

        private void nextDay_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush whiteArrowFill = new SolidColorBrush();
            whiteArrowFill.Color = Color.FromArgb(255, 244, 244, 245);
            nextDay.Stroke = whiteArrowFill;
        }

        private void previousDay_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush whiteArrowFill = new SolidColorBrush();
            whiteArrowFill.Color = Color.FromArgb(255, 244, 244, 245);
            previousDay.Stroke = whiteArrowFill;

        }

        private void previousDay_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush blueArrowFill = new SolidColorBrush();
            blueArrowFill.Color = Color.FromArgb(255, 0, 0, 139);
            previousDay.Stroke = blueArrowFill;
        }


        private void dateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            monthCalendar.SelectedDate = dateBox.SelectedDate;
            monthCalendar.DisplayDate = monthCalendar.SelectedDate.GetValueOrDefault();

            bookDate.SelectedDate = dateBox.SelectedDate;
            
            //update the saved display date
            displayDate = ((DateTime)dateBox.SelectedDate).Date;

            refreshDisplayDate();
       
        }

        private void patientNew_Click(object sender, RoutedEventArgs e)
        {
            if (newPat!=null)
                newPat.Close();
            newPat = new addPatient(this);
            newPat.Show();
        }

        private void patientEdit_Click(object sender, RoutedEventArgs e)
        {
            editPat.Close();
            editPat = new editPatient(this);
            editPat.Show();
        }

        private void patientPast_Click(object sender, RoutedEventArgs e)
        {
            pastAppt.Close();
            pastAppt = new pastAppointments();
            pastAppt.Show();
        }

        private void selectPatientButton_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            searchPat = new searchPatient(this);
            searchPat.Show();
        }

        private void bookClearButton_Click(object sender, RoutedEventArgs e)
        {
            clearBookNewApptStuff();
        }

        private void clearBookNewApptStuff()
        {
            bookNewName.Text = "<patient name>";
            bookDoctor.Text = "";
            bookDate.SelectedDate = displayDate;
            bookTime.Text = "";
            bookNotes.Text = "";
            bookDouble.IsChecked = false;
            bookAddButton.IsEnabled = false;
        }


        SolidColorBrush customGreen = new SolidColorBrush();
        LinearGradientBrush greenGradient = new LinearGradientBrush();
        SolidColorBrush slotGrey = new SolidColorBrush();
        

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

            if (currentSlot.Content.ToString() != "")
            {
                checkInButton.IsEnabled = true;
                bookAddButton.IsEnabled = false;
            }
            else
            {
                checkInButton.IsEnabled = false;
                if (bookNewName.Text != "<patient name>")
                    bookAddButton.IsEnabled = true;
            }

            SolidColorBrush borderGrey = new SolidColorBrush();
            borderGrey.Color = Color.FromArgb(255, 208, 208, 208);
            if (previousSlot != null)
            {
                slotGrey.Color = Color.FromArgb(255, 239, 239, 239);
                previousSlot.BorderBrush = borderGrey;
                if (previousSlot.Background != greenGradient)
                {
                    previousSlot.Background = slotGrey;
                }
            }
            currentSlot.BorderBrush = Brushes.Aqua;

            if (currentSlot.Background == greenGradient)
            {
                checkInButton.Content = "Cancel Check - In";
            }
            else
            {
                SolidColorBrush whiteArrowFill = new SolidColorBrush();
                whiteArrowFill.Color = Color.FromArgb(255, 244, 244, 245);
                currentSlot.Background = whiteArrowFill;
                checkInButton.Content = "Check - In";
            }

            /*displays message with info for box clicked
            var mb = MessageBox.Show("\""+calButton.Name+"\" returns timeslot Index = "
                +timeslotNametoIndex(calButton.Name)
                +"\nFor Doctor #: "+timeslotNametoDoctor(calButton.Name));
            */
            updateBookBox();
            updateApptInfoBox();
            if (currentSlot.Content.ToString() != null)
            {
                appInfoPatientTB.Text = currentSlot.Content.ToString();
            }
            else
            {
                appInfoPatientTB.Text = "";
            }

       }

        public void checkInFunc()
        {
            slotGrey.Color = Color.FromArgb(255, 239, 239, 239);
            if (currentSlot != null)
            {
                if (currentSlot.Background == greenGradient)
                {
                    currentSlot.Background = slotGrey;
                    checkInButton.Content = "Check- In";
                }
                else
                {
                    currentSlot.Background = greenGradient;
                    checkInButton.Content = "Cancel Check - In";
                }
            }
        }

        private void checkInButton_Click(object sender, RoutedEventArgs e)
        {
            checkInFunc();
        }

        private void bookDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            monthCalendar.SelectedDate = bookDate.SelectedDate;
            monthCalendar.DisplayDate = monthCalendar.SelectedDate.GetValueOrDefault();
            dateBox.SelectedDate = bookDate.SelectedDate;

            displayDate = ((DateTime)bookDate.SelectedDate).Date;
        }

        private void todayButton_Click(object sender, RoutedEventArgs e)
        {
            monthCalendar.SelectedDate = DateTime.Now;
            dateBox.SelectedDate = DateTime.Now;
            bookDate.SelectedDate = DateTime.Now;

        }

        private void apptCheckIn_Click(object sender, RoutedEventArgs e)
        {
            checkInFunc();
        }

        public void cancelCheck()
        {
            confCancel.Close();
            confCancel = new cancelConfirmation();
            confCancel.Show();
        }

        private void apptCancel_Click(object sender, RoutedEventArgs e)
        {
            if (currentSlot != null)
            {
                cancelCheck();
            }
        }

        public void updateApptInfoBox()
        {
            if (timeslotNametoDoctor(currentSlot.Name) == 0)
                appInfoDoctorTB.Text = "Dr. Phillips";
            if (timeslotNametoDoctor(currentSlot.Name) == 1)
                appInfoDoctorTB.Text = "Dr. Strumpfer";
            if (timeslotNametoDoctor(currentSlot.Name) == 2)
                appInfoDoctorTB.Text = "Dr. Cole";
            if (timeslotNametoDoctor(currentSlot.Name) == 3)
                appInfoDoctorTB.Text = "Nurse Ratched";
            if (timeslotNametoDoctor(currentSlot.Name) == 4)
                appInfoDoctorTB.Text = "Dr. Honeydew";

            if (timeslotNametoIndex(currentSlot.Name) == 0)
                appInfoTimeTB.Text = "8:00 - 8:20";
            if (timeslotNametoIndex(currentSlot.Name) == 1)
                appInfoTimeTB.Text = "8:20 - 8:40";
            if (timeslotNametoIndex(currentSlot.Name) == 2)
                appInfoTimeTB.Text = "8:40 - 9:00";
            if (timeslotNametoIndex(currentSlot.Name) == 3)
                appInfoTimeTB.Text = "9:00 - 9:20";
            if (timeslotNametoIndex(currentSlot.Name) == 4)
                appInfoTimeTB.Text = "9:20 - 9:40";
            if (timeslotNametoIndex(currentSlot.Name) == 5)
                appInfoTimeTB.Text = "9:40 - 10:00";
            if (timeslotNametoIndex(currentSlot.Name) == 6)
                appInfoTimeTB.Text = "10:00 - 10:20";
            if (timeslotNametoIndex(currentSlot.Name) == 7)
                appInfoTimeTB.Text = "10:20 - 10:40";
            if (timeslotNametoIndex(currentSlot.Name) == 8)
                appInfoTimeTB.Text = "10:40 - 11:00";
            if (timeslotNametoIndex(currentSlot.Name) == 9)
                appInfoTimeTB.Text = "11:00 - 11:20";
            if (timeslotNametoIndex(currentSlot.Name) == 10)
                appInfoTimeTB.Text = "11:20 - 11:40";
            if (timeslotNametoIndex(currentSlot.Name) == 11)
                appInfoTimeTB.Text = "11:40 - 12:00";
            if (timeslotNametoIndex(currentSlot.Name) == 12)
                appInfoTimeTB.Text = "12:00 - 12:20";
            if (timeslotNametoIndex(currentSlot.Name) == 13)
                appInfoTimeTB.Text = "12:20 - 12:40";
            if (timeslotNametoIndex(currentSlot.Name) == 14)
                appInfoTimeTB.Text = "12:40 - 1:00";
            if (timeslotNametoIndex(currentSlot.Name) == 15)
                appInfoTimeTB.Text = "1:00 - 1:20";
            if (timeslotNametoIndex(currentSlot.Name) == 16)
                appInfoTimeTB.Text = "1:20 - 1:40";
            if (timeslotNametoIndex(currentSlot.Name) == 17)
                appInfoTimeTB.Text = "1:40 - 2:00";
            if (timeslotNametoIndex(currentSlot.Name) == 18)
                appInfoTimeTB.Text = "2:00 - 2:20";
            if (timeslotNametoIndex(currentSlot.Name) == 19)
                appInfoTimeTB.Text = "2:20 - 2:40";
            if (timeslotNametoIndex(currentSlot.Name) == 20)
                appInfoTimeTB.Text = "2:40 - 3:00";
            if (timeslotNametoIndex(currentSlot.Name) == 21)
                appInfoTimeTB.Text = "3:00 - 3:20";
            if (timeslotNametoIndex(currentSlot.Name) == 22)
                appInfoTimeTB.Text = "3:20 - 3:40";
        }

        public void updateBookBox()
        {
            //updates the doctor dropdown
            if (timeslotNametoDoctor(currentSlot.Name) == 0)
                bookDoctor.SelectedItem = d0Dropdown;
            if (timeslotNametoDoctor(currentSlot.Name) == 1)
                bookDoctor.SelectedItem = d1Dropdown;
            if (timeslotNametoDoctor(currentSlot.Name) == 2)
                bookDoctor.SelectedItem = d2Dropdown;
            if (timeslotNametoDoctor(currentSlot.Name) == 3)
                bookDoctor.SelectedItem = d3Dropdown;
            if (timeslotNametoDoctor(currentSlot.Name) == 4)
                bookDoctor.SelectedItem = d4Dropdown;


            //updates the time dropdown
            tBooked.Visibility = System.Windows.Visibility.Collapsed;
            if (((string)currentSlot.Content) != "")
            {
                tBooked.Visibility = System.Windows.Visibility.Visible;
                bookTime.SelectedItem = tBooked;
            }
            else
            {
                if (timeslotNametoIndex(currentSlot.Name) == 0)
                    bookTime.SelectedItem = t0Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 1)
                    bookTime.SelectedItem = t1Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 2)
                    bookTime.SelectedItem = t2Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 3)
                    bookTime.SelectedItem = t3Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 4)
                    bookTime.SelectedItem = t4Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 5)
                    bookTime.SelectedItem = t5Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 6)
                    bookTime.SelectedItem = t6Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 7)
                    bookTime.SelectedItem = t7Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 8)
                    bookTime.SelectedItem = t8Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 9)
                    bookTime.SelectedItem = t9Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 10)
                    bookTime.SelectedItem = t10Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 11)
                    bookTime.SelectedItem = t11Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 12)
                    bookTime.SelectedItem = t12Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 13)
                    bookTime.SelectedItem = t13Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 14)
                    bookTime.SelectedItem = t14Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 15)
                    bookTime.SelectedItem = t15Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 16)
                    bookTime.SelectedItem = t16Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 17)
                    bookTime.SelectedItem = t17Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 18)
                    bookTime.SelectedItem = t18Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 19)
                    bookTime.SelectedItem = t19Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 20)
                    bookTime.SelectedItem = t20Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 21)
                    bookTime.SelectedItem = t21Dropdown;
                if (timeslotNametoIndex(currentSlot.Name) == 22)
                    bookTime.SelectedItem = t22Dropdown;
            }

        }


        //this method updates the maincalandar appointments
        //@Param Array<String> - a list of the text data to update the calander
        public void updateMainCalander(string[] d0, string[] d1,string[] d2, string[] d3,string[] d4)
        {
            //CHECK FOR CORRECT DATA
            if ( d0.Length < 23)
            {
                var mb = MessageBox.Show("Error! the calandar is trying to update with wrong size data");
                return;
            }
            if ( d1.Length < 23)
            {
                var mb = MessageBox.Show("Error! the calandar is trying to update with wrong size data");
                return;
            }
            if ( d2.Length < 23)
            {
                var mb = MessageBox.Show("Error! the calandar is trying to update with wrong size data");
                return;
            }
            if ( d3.Length < 23)
            {
                var mb = MessageBox.Show("Error! the calandar is trying to update with wrong size data");
                return;
            }
            if ( d4.Length < 23)
            {
                var mb = MessageBox.Show("Error! the calandar is trying to update with wrong size data");
                return;
            }

            clearCalendarDisplay();
            //update all 23 timeslots form doctor 1
            if (d0[0] !=null)
                d0t0.Content = d0[0];
            if (d0[1] !=null)
                d0t1.Content = d0[1];
            if (d0[2] !=null)
                d0t2.Content = d0[2];
            if (d0[3] != null)
                d0t3.Content = d0[3];
            if (d0[4] != null)
                d0t4.Content = d0[4];
            if (d0[5] != null)
                d0t5.Content = d0[5];
            if (d0[6] != null)
                d0t5.Content = d0[6];
            if (d0[6] != null)
                d0t7.Content = d0[7];
            if (d0[8] != null)
                d0t8.Content = d0[8];
            if (d0[9] != null)
                d0t9.Content = d0[9];
            if (d0[10] != null)
                d0t10.Content = d0[10];
            if (d0[11] != null)
                d0t11.Content = d0[11];
            if (d0[12] != null)
                d0t12.Content = d0[12];
            if (d0[13] != null)
                d0t13.Content = d0[13];
            if (d0[14] != null)
                d0t14.Content = d0[14];
            if (d0[15] != null)
                d0t15.Content = d0[15];
            if (d0[16] != null)
                d0t16.Content = d0[16];
            if (d0[17] != null)
                d0t17.Content = d0[17];
            if (d0[18] != null)
                d0t18.Content = d0[18];
            if (d0[19] != null)
                d0t19.Content = d0[19];
            if (d0[20] != null)
                d0t20.Content = d0[20];
            if (d0[21] != null)
                d0t21.Content = d0[21];
            if (d0[22] != null)
                d0t22.Content = d0[22];

            //update all 23 timeslots form doctor 2
            if (d1[0] != null)
                d1t0.Content = d1[0];
            if (d1[1] != null)
                d1t1.Content = d1[1];
            if (d1[2] != null)
                d1t2.Content = d1[2];
            if (d1[3] != null)
                d1t3.Content = d1[3];
            if (d1[4] != null)
                d1t4.Content = d1[4];
            if (d1[5] != null)
                d1t5.Content = d1[5];
            if (d1[6] != null)
                d1t5.Content = d1[6];
            if (d1[6] != null)
                d1t7.Content = d1[7];
            if (d1[8] != null)
                d1t8.Content = d1[8];
            if (d1[9] != null)
                d1t9.Content = d1[9];
            if (d1[10] != null)
                d1t10.Content = d1[10];
            if (d1[11] != null)
                d1t11.Content = d1[11];
            if (d1[12] != null)
                d1t12.Content = d1[12];
            if (d1[13] != null)
                d1t13.Content = d1[13];
            if (d1[14] != null)
                d1t14.Content = d1[14];
            if (d1[15] != null)
                d1t15.Content = d1[15];
            if (d1[16] != null)
                d1t16.Content = d1[16];
            if (d1[17] != null)
                d1t17.Content = d1[17];
            if (d1[18] != null)
                d1t18.Content = d1[18];
            if (d1[19] != null)
                d1t19.Content = d1[19];
            if (d1[20] != null)
                d1t20.Content = d1[20];
            if (d1[21] != null)
                d1t21.Content = d1[21];
            if (d1[22] != null)
                d1t22.Content = d1[22];

            //update all 23 timeslots form doctor 3
            if (d2[0] != null)
                d2t0.Content = d2[0];
            if (d2[1] != null)
                d2t1.Content = d2[1];
            if (d2[2] != null)
                d2t2.Content = d2[2];
            if (d2[3] != null)
                d2t3.Content = d2[3];
            if (d2[4] != null)
                d2t4.Content = d2[4];
            if (d2[5] != null)
                d2t5.Content = d2[5];
            if (d2[6] != null)
                d2t5.Content = d2[6];
            if (d2[6] != null)
                d2t7.Content = d2[7];
            if (d2[8] != null)
                d2t8.Content = d2[8];
            if (d2[9] != null)
                d2t9.Content = d2[9];
            if (d2[10] != null)
                d2t10.Content = d2[10];
            if (d2[11] != null)
                d2t11.Content = d2[11];
            if (d2[12] != null)
                d2t12.Content = d2[12];
            if (d2[13] != null)
                d2t13.Content = d2[13];
            if (d2[14] != null)
                d2t14.Content = d2[14];
            if (d2[15] != null)
                d2t15.Content = d2[15];
            if (d2[16] != null)
                d2t16.Content = d2[16];
            if (d2[17] != null)
                d2t17.Content = d2[17];
            if (d2[18] != null)
                d2t18.Content = d2[18];
            if (d2[19] != null)
                d2t19.Content = d2[19];
            if (d2[20] != null)
                d2t20.Content = d2[20];
            if (d2[21] != null)
                d2t21.Content = d2[21];
            if (d2[22] != null)
                d2t22.Content = d2[22];

            //update all 23 timeslots form doctor 4
            if (d3[0] != null)
                d3t0.Content = d3[0];
            if (d3[1] != null)
                d3t1.Content = d3[1];
            if (d3[2] != null)
                d3t2.Content = d3[2];
            if (d3[3] != null)
                d3t3.Content = d3[3];
            if (d3[4] != null)
                d3t4.Content = d3[4];
            if (d3[5] != null)
                d3t5.Content = d3[5];
            if (d3[6] != null)
                d3t5.Content = d3[6];
            if (d3[6] != null)
                d3t7.Content = d3[7];
            if (d3[8] != null)
                d3t8.Content = d3[8];
            if (d3[9] != null)
                d3t9.Content = d3[9];
            if (d3[10] != null)
                d3t10.Content = d3[10];
            if (d3[11] != null)
                d3t11.Content = d3[11];
            if (d3[12] != null)
                d3t12.Content = d3[12];
            if (d3[13] != null)
                d3t13.Content = d3[13];
            if (d3[14] != null)
                d3t14.Content = d3[14];
            if (d3[15] != null)
                d3t15.Content = d3[15];
            if (d3[16] != null)
                d3t16.Content = d3[16];
            if (d3[17] != null)
                d3t17.Content = d3[17];
            if (d3[18] != null)
                d3t18.Content = d3[18];
            if (d3[19] != null)
                d3t19.Content = d3[19];
            if (d3[20] != null)
                d3t20.Content = d3[20];
            if (d3[21] != null)
                d3t21.Content = d3[21];
            if (d3[22] != null)
                d3t22.Content = d3[22];

            //update all 23 timeslots form doctor 5
            if (d4[0] != null)
                d4t0.Content = d4[0];
            if (d4[1] != null)
                d4t1.Content = d4[1];
            if (d4[2] != null)
                d4t2.Content = d4[2];
            if (d4[3] != null)
                d4t3.Content = d4[3];
            if (d4[4] != null)
                d4t4.Content = d4[4];
            if (d4[5] != null)
                d4t5.Content = d4[5];
            if (d4[6] != null)
                d4t5.Content = d4[6];
            if (d4[6] != null)
                d4t7.Content = d4[7];
            if (d4[8] != null)
                d4t8.Content = d4[8];
            if (d4[9] != null)
                d4t9.Content = d4[9];
            if (d4[10] != null)
                d4t10.Content = d4[10];
            if (d4[11] != null)
                d4t11.Content = d4[11];
            if (d4[12] != null)
                d4t12.Content = d4[12];
            if (d4[13] != null)
                d4t13.Content = d4[13];
            if (d4[14] != null)
                d4t14.Content = d4[14];
            if (d4[15] != null)
                d4t15.Content = d4[15];
            if (d4[16] != null)
                d4t16.Content = d4[16];
            if (d4[17] != null)
                d4t17.Content = d4[17];
            if (d4[18] != null)
                d4t18.Content = d4[18];
            if (d4[19] != null)
                d4t19.Content = d4[19];
            if (d4[20] != null)
                d4t20.Content = d4[20];
            if (d4[21] != null)
                d4t21.Content = d4[21];
            if (d4[22] != null)
                d4t22.Content = d4[22];


        }

    
        public void addRandomPatients()
        {
            Patient p1 = new Patient("Michael", "Hayden", "M", "112 Angel St.", 410, 6658734, "United States", "Maryland", "Baltimore", 07, 05, 1965, 223444111, "");
            Patient p2 = new Patient("Ray", "Person", "M", "65 Yorkshire Way", 654, 4133578, "UK", "London", "London", 04, 11, 1973, 1122113, "");
            Patient p3 = new Patient("Rupert", "Dennings", "M", "1 Broadmoor St.", 07, 55445567, "Australia", "Queensland", "Brisbane", 07, 09, 1990, 21114151, "");
            Patient p4 = new Patient("Ibrahim", "Sayeed", "M", "89 Sethi St.", 91, 4415454, "Pakistan", "NWFP", "Peshawar", 04, 05, 1985, 1212141, "");
            Patient p5 = new Patient("Laura", "Janssens", "F", "63 Louizastraat", 42, 5511615, "Belgium", "Flemish Region", "Antwerp", 07, 09, 1989, 12125156, "");
            Patient p6 = new Patient("Brian", "Omar", "M", "12 Dalgleish Bay", 403, 9351515, "Canada", "Alberta", "Calgary", 05, 03, 1991, 121513, "");
            Patient p7 = new Patient("Aman", "Gill", "F", "2114 35th Ave SW", 403, 4619305, "Canada", "Alberta", "Calgary", 03, 01, 1988, 12141251, "");
            Patient p8 = new Patient("Jennifer", "Talmage", "F", "141 Whyte Ave", 403, 4151551, "Canada", "Alberta", "Edmonton", 02, 01, 1991, 1211211, "");
            Patient p9 = new Patient("Jayant", "Patell", "M", "73 Royal Crest Way NW", 403, 4315133, "Canada", "Alberta", "Calgary", 01, 01, 1901, 121241, "");
            Patient p10 = new Patient("Hardeep", "Gar", "M", "10 Country Hills BLVD NE", 403, 1511611, "Canada", "Alberta", "Calgary", 02, 05, 1991, 4523512, "");
            patients.Add(p1);
            patients.Add(p2);
            patients.Add(p3);
            patients.Add(p4);
            patients.Add(p5);
            patients.Add(p6);
            patients.Add(p7);
            patients.Add(p8);
            patients.Add(p9);
            patients.Add(p10);
        }


        public int timeslotNametoIndex(String name)
        {
            string slot = name.Substring(2);
            
            switch (slot)
            {
                case "t0" : return 0;
                case "t1": return 1;
                case "t2": return 2;
                case "t3": return 3;
                case "t4": return 4;
                case "t5": return 5;
                case "t6": return 6;
                case "t7": return 7;
                case "t8": return 8;
                case "t9": return 9;
                case "t10": return 10;
                case "t11": return 11;
                case "t12": return 12;
                case "t13": return 13;
                case "t14": return 14;
                case "t15": return 15;
                case "t16": return 16;
                case "t17": return 17;
                case "t18": return 18;
                case "t19": return 19;
                case "t20": return 20;
                case "t21": return 21;
                case "t22": return 22;
            }
            return -1;
        }
        public int timeslotNametoDoctor(String name)
        {
            return Int32.Parse(name.Substring(1, 1));
        }

        public void displayApptInfo(Appointment appt)
        {
            appInfoDateTB.Text = appt.date.ToShortDateString();
            appInfoDoctorTB.Text = appt.DoctorName();
            appInfoPatientTB.Text = appt.patient.lastName + ", " + appt.patient.lastName;
            appInfoTimeTB.Text = appt.Timeslot();
        }

        public void bookAppointment()
        {
            //check if date selected
            if (bookDate.SelectedDate == null)
            {
                var mb = MessageBox.Show("Error: date is null");
                return;
            }

            DayOfAppointments dayOfAppt;
            DateTime date = ((DateTime)bookDate.SelectedDate).Date;
            Appointment appt=new Appointment(date);

            try
            {
                //search for existing date in dictionary
                if (allAppointmentsDictionary.ContainsKey(date.ToShortDateString()))
                {
                    bool containsDate= allAppointmentsDictionary.TryGetValue(date.ToShortDateString(),out dayOfAppt);
                    if (!containsDate)
                    {
                        var mb = MessageBox.Show("Error in lookup of dayOfAppt");
                        return; //quit the method on error
                    }

                }
                else
                {
                    dayOfAppt = new DayOfAppointments(date);
                    allAppointmentsDictionary.Add(date.ToShortDateString(), dayOfAppt);
                    appt.doubleBooked = false;
                }

                //get and checktimeslot
                int timeslot=-1;
                try
                {
                    timeslot = stringToTimeslotInt(((ComboBoxItem)bookTime.SelectedItem).Content.ToString());
                }
                catch (Exception err)
                {
                    var mb = MessageBox.Show("Error: No timeslot selected. Please select a timeslot.");
                    bookAddButton.IsEnabled = false;
                    return; //quit on error
                }
                //check if the stringto timeslot returned a valid index
                if (timeslot == -1)
                {
                    var mb = MessageBox.Show("Error: Timeslot is booked. To double book: manualy resect the timeslot and ensure \"double book\" is clicked.");
                    return; //quit on error
                }
                
                if (d0Dropdown.IsSelected)
                {
                    appt.doctor = 0;
                    appt.timeslot = timeslot;
                    appt.patient = this.selectedPatient;
                    appt.date = date;
                    //check if appointment already booked
                    if (dayOfAppt.d0[appt.timeslot] == null)
                    {
                        dayOfAppt.d0[appt.timeslot] = appt;
                    }
                    else
                    {
                        if ((dayOfAppt.d0[appt.timeslot].doubleBookedAppt == null) && (bookDouble.IsChecked == true))
                        {
                            dayOfAppt.d0[appt.timeslot].doubleBookedAppt = appt;
                            dayOfAppt.d0[appt.timeslot].doubleBooked = true;
                        }
                        else
                        {
                            var mb = MessageBox.Show("Error. Cannot book appointment because another appointment is already booked for this timeslot.");
                            return; //quit method on error
                        }
                    }

                }
                else if (d1Dropdown.IsSelected)
                {
                    appt.doctor = 1;
                    appt.timeslot = timeslot;
                    appt.patient = this.selectedPatient;
                    appt.date = date;
                    //check if appointment already booked
                    if (dayOfAppt.d1[appt.timeslot] == null)
                    {
                        dayOfAppt.d1[appt.timeslot] = appt;
                    }
                    else
                    {
                        if ((dayOfAppt.d1[appt.timeslot].doubleBookedAppt == null) && (bookDouble.IsChecked == true))
                        {
                            dayOfAppt.d1[appt.timeslot].doubleBookedAppt = appt;
                            dayOfAppt.d1[appt.timeslot].doubleBooked = true;
                        }
                        else
                        {
                            var mb = MessageBox.Show("Error. Cannot book appointment because another appointment is already booked for this timeslot.");
                            return; //quit method on error
                        }
                    }
                }
                else if (d2Dropdown.IsSelected)
                {
                    appt.doctor = 2;
                    appt.timeslot = timeslot;
                    appt.patient = this.selectedPatient;
                    appt.date = date;
                    //check if appointment already booked
                    if (dayOfAppt.d2[appt.timeslot] == null)
                    {
                        dayOfAppt.d2[appt.timeslot] = appt;
                    }
                    else
                    {
                        if ((dayOfAppt.d2[appt.timeslot].doubleBookedAppt == null) && (bookDouble.IsChecked == true))
                        {
                            dayOfAppt.d2[appt.timeslot].doubleBookedAppt = appt;
                            dayOfAppt.d2[appt.timeslot].doubleBooked = true;
                        }
                        else
                        {
                            var mb = MessageBox.Show("Error. Cannot book appointment because another appointment is already booked for this timeslot.");
                            return; //quit method on error
                        }
                    }
                }
                else if (d3Dropdown.IsSelected)
                {
                    appt.doctor = 3;
                    appt.timeslot = timeslot;
                    appt.patient = this.selectedPatient;
                    appt.date = date;
                    //check if appointment already booked
                    if (dayOfAppt.d3[appt.timeslot] == null)
                    {
                        dayOfAppt.d3[appt.timeslot] = appt;
                    }
                    else
                    {
                        if ((dayOfAppt.d3[appt.timeslot].doubleBookedAppt == null) && (bookDouble.IsChecked == true))
                        {
                            dayOfAppt.d3[appt.timeslot].doubleBookedAppt = appt;
                            dayOfAppt.d3[appt.timeslot].doubleBooked = true;
                        }
                        else
                        {
                            var mb = MessageBox.Show("Error. Cannot book appointment because another appointment is already booked for this timeslot.");
                            return; //quit method on error
                        }
                    }
                }
                else if (d4Dropdown.IsSelected)
                {
                    appt.doctor = 4;
                    appt.timeslot = timeslot;
                    appt.patient = this.selectedPatient;
                    appt.date = date;
                    //check if appointment already booked
                    if (dayOfAppt.d4[appt.timeslot] == null)
                    {
                        dayOfAppt.d4[appt.timeslot] = appt;
                    }
                    else
                    {
                        if ((dayOfAppt.d4[appt.timeslot].doubleBookedAppt == null) && (bookDouble.IsChecked == true))
                        {
                            dayOfAppt.d4[appt.timeslot].doubleBookedAppt = appt;
                            dayOfAppt.d4[appt.timeslot].doubleBooked = true;
                        }
                        else
                        {
                            var mb = MessageBox.Show("Error. Cannot book appointment because another appointment is already booked for this timeslot.");
                            return; //quit method on error
                        }
                    }
                }
                else
                {

                   var mb1 = MessageBox.Show("Error: No Doctor selected. Please select a doctor from the dropdown.");
                   bookAddButton.IsEnabled = false;

                }

                var mb2 = MessageBox.Show(appt.ToString());


            }
            catch (Exception err)
            {
                var mb = MessageBox.Show(err.ToString());
            }

            bool containsSelectedDate = allAppointmentsDictionary.TryGetValue(date.ToShortDateString(), out dayOfAppt);
            if (!containsSelectedDate)
            {
                //var mb = MessageBox.Show("No appointments for current day");
                return; //quit the method on error
            }
            else
            {
                displayAppointments(dayOfAppt);
            }
            

        }// end book appointment

        private int stringToTimeslotInt(string input)
        {
            switch (input)
            {
                case "8:00 - 8:20": return 0;
                case "8:20 - 8:40": return 1;
                case "8:40 - 9:00": return 2;
                case "9:00 - 9:20": return 3;
                case "9:20 - 9:40": return 4;
                case "9:40 - 10:00": return 5;
                case "10:00 - 10:20": return 6;
                case "10:20 - 10:40": return 7;
                case "10:40 - 11:00": return 8;
                case "11:00 - 11:20": return 9;
                case "11:20 - 11:40": return 10;
                case "11:40 - 12:00": return 11;
                case "12:00 - 12:20": return 12;
                case "12:20 - 12:40": return 13;
                case "12:40 - 1:00": return 14;
                case "1:00 - 1:20": return 15;
                case "1:20 - 1:40": return 16;
                case "1:40 - 2:00": return 17;
                case "2:00 - 2:20": return 18;
                case "2:20 - 2:40": return 19;
                case "2:40 - 3:00": return 20;
                case "3:00 - 3:20": return 21;
                case "3:20 - 3:40": return 22;
                default: return -1;
            }
             
        }

        private void bookAddButton_Click(object sender, RoutedEventArgs e)
        {
            bookAppointment();
            bookAddButton.IsEnabled = false;
            clearBookNewApptStuff();
        }

        public void displayAppointments(DayOfAppointments dayOfAppts)
        {
            string[] d0 = new string[23];
            string[] d1 = new string[23];
            string[] d2 = new string[23];
            string[] d3 = new string[23];
            string[] d4 = new string[23];

            //for loop the timeslots
            for (int i = 0; i<23; i++)
            {
                if (dayOfAppts.d0[i] != null)
                {
                    if (dayOfAppts.d0[i].doubleBooked)
                        d0[i] = "" + dayOfAppts.d0[i].patient.lastName + ", " + dayOfAppts.d0[i].patient.firstName
                               + "/" + dayOfAppts.d0[i].doubleBookedAppt.patient.lastName + ", " + dayOfAppts.d0[i].doubleBookedAppt.patient.firstName;
                    else
                        d0[i] = "" + dayOfAppts.d0[i].patient.lastName + ", " + dayOfAppts.d0[i].patient.firstName;
                }
                else d0[i] = "";

                if (dayOfAppts.d1[i] != null)
                {
                    if (dayOfAppts.d1[i].doubleBooked)
                        d1[i] = "" + dayOfAppts.d1[i].patient.lastName + ", " + dayOfAppts.d1[i].patient.firstName
                               + "/" + dayOfAppts.d1[i].doubleBookedAppt.patient.lastName + ", " + dayOfAppts.d1[i].doubleBookedAppt.patient.firstName;
                    else
                        d1[i] = "" + dayOfAppts.d1[i].patient.lastName + ", " + dayOfAppts.d1[i].patient.firstName;
                }
                else d1[i] = "";

                if (dayOfAppts.d2[i] != null)
                {
                    if (dayOfAppts.d2[i].doubleBooked)
                        d2[i] = "" + dayOfAppts.d2[i].patient.lastName + ", " + dayOfAppts.d2[i].patient.firstName
                               + "/" + dayOfAppts.d2[i].doubleBookedAppt.patient.lastName + ", " + dayOfAppts.d2[i].doubleBookedAppt.patient.firstName;
                    else
                        d2[i] = "" + dayOfAppts.d2[i].patient.lastName + ", " + dayOfAppts.d2[i].patient.firstName;
                }
                else d2[i] = "";

                if (dayOfAppts.d3[i] != null)
                {
                    if (dayOfAppts.d3[i].doubleBooked)
                        d3[i] = "" + dayOfAppts.d3[i].patient.lastName + ", " + dayOfAppts.d3[i].patient.firstName
                               + "/" + dayOfAppts.d3[i].doubleBookedAppt.patient.lastName + ", " + dayOfAppts.d3[i].doubleBookedAppt.patient.firstName;
                    else
                        d3[i] = "" + dayOfAppts.d3[i].patient.lastName + ", " + dayOfAppts.d3[i].patient.firstName;
                }
                else d3[i] = "";

                if (dayOfAppts.d4[i] != null)
                {
                    if (dayOfAppts.d4[i].doubleBooked)
                        d4[i] = "" + dayOfAppts.d4[i].patient.lastName + ", " + dayOfAppts.d4[i].patient.firstName
                               + "/" + dayOfAppts.d4[i].doubleBookedAppt.patient.lastName + ", " + dayOfAppts.d4[i].doubleBookedAppt.patient.firstName;
                    else
                        d4[i] = "" + dayOfAppts.d4[i].patient.lastName + ", " + dayOfAppts.d4[i].patient.firstName;
                }
                else d4[i] = "";
            
            }//end for loop (timeslots)

            //update the display
            this.updateMainCalander(d0, d1, d2, d3, d4);

        }

        public void refreshDisplayDate()
        {
            //update the display for current date
            DayOfAppointments dayOfAppt;
            bool containsSelectedDate = allAppointmentsDictionary.TryGetValue(displayDate.ToShortDateString(), out dayOfAppt);
            if (!containsSelectedDate)
            {
                clearCalendarDisplay();
            }
            else
            {
                displayAppointments(dayOfAppt);
            }
            return;
        }

        public void clearCalendarDisplay()
        {

            //clear all 23 timeslots form doctor 1
            d0t0.Content = "";
            d0t1.Content = "";
            d0t2.Content = "";
            d0t3.Content = "";
            d0t4.Content = "";
            d0t5.Content = "";
            d0t5.Content = "";
            d0t7.Content = "";
            d0t8.Content = "";
            d0t9.Content = "";
            d0t10.Content = "";
            d0t11.Content = "";
            d0t12.Content = "";
            d0t13.Content = "";
            d0t14.Content = "";
            d0t15.Content = "";
            d0t16.Content = "";
            d0t17.Content = "";
            d0t18.Content = "";
            d0t19.Content = "";
            d0t20.Content = "";
            d0t21.Content = "";
            d0t22.Content = "";
            //clear all 23 timeslots form doctor 2
            d1t0.Content = "";
            d1t1.Content = "";
            d1t2.Content = "";
            d1t3.Content = "";
            d1t4.Content = "";
            d1t5.Content = "";
            d1t5.Content = "";
            d1t7.Content = "";
            d1t8.Content = "";
            d1t9.Content = "";
            d1t10.Content = "";
            d1t11.Content = "";
            d1t12.Content = "";
            d1t13.Content = "";
            d1t14.Content = "";
            d1t15.Content = "";
            d1t16.Content = "";
            d1t17.Content = "";
            d1t18.Content = "";
            d1t19.Content = "";
            d1t20.Content = "";
            d1t21.Content = "";
            d1t22.Content = "";
            //clear all 23 timeslots form doctor 3
            d2t0.Content = "";
            d2t1.Content = "";
            d2t2.Content = "";
            d2t3.Content = "";
            d2t4.Content = "";
            d2t5.Content = "";
            d2t5.Content = "";
            d2t7.Content = "";
            d2t8.Content = "";
            d2t9.Content = "";
            d2t10.Content = "";
            d2t11.Content = "";
            d2t12.Content = "";
            d2t13.Content = "";
            d2t14.Content = "";
            d2t15.Content = "";
            d2t16.Content = "";
            d2t17.Content = "";
            d2t18.Content = "";
            d2t19.Content = "";
            d2t20.Content = "";
            d2t21.Content = "";
            d2t22.Content = "";
            //clear all 23 timeslots form doctor 4
            d3t0.Content = "";
            d3t1.Content = "";
            d3t2.Content = "";
            d3t3.Content = "";
            d3t4.Content = "";
            d3t5.Content = "";
            d3t5.Content = "";
            d3t7.Content = "";
            d3t8.Content = "";
            d3t9.Content = "";
            d3t10.Content = "";
            d3t11.Content = "";
            d3t12.Content = "";
            d3t13.Content = "";
            d3t14.Content = "";
            d3t15.Content = "";
            d3t16.Content = "";
            d3t17.Content = "";
            d3t18.Content = "";
            d3t19.Content = "";
            d3t20.Content = "";
            d3t21.Content = "";
            d3t22.Content = "";
            //clear all 23 timeslots form doctor 5
            d4t0.Content = "";
            d4t1.Content = "";
            d4t2.Content = "";
            d4t3.Content = "";
            d4t4.Content = "";
            d4t5.Content = "";
            d4t5.Content = "";
            d4t7.Content = "";
            d4t8.Content = "";
            d4t9.Content = "";
            d4t10.Content = "";
            d4t11.Content = "";
            d4t12.Content = "";
            d4t13.Content = "";
            d4t14.Content = "";
            d4t15.Content = "";
            d4t16.Content = "";
            d4t17.Content = "";
            d4t18.Content = "";
            d4t19.Content = "";
            d4t20.Content = "";
            d4t21.Content = "";
            d4t22.Content = "";
        }


    }

}
