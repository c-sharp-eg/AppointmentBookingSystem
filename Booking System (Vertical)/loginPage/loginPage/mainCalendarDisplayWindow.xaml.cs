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

        //global variables
        public Dictionary<DateTime, DayOfAppointments> allAppointmentsDictionary;
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

            monthCalendar.SelectedDate = DateTime.Now;
            dateBox.SelectedDate = DateTime.Now;
            bookDate.SelectedDate = DateTime.Now;

            //just for display testing print random appointments
            displayRandomAppointments();
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
            bookNewName.Text = "<patient name>";
            bookDoctor.Text = "";
            bookDate.SelectedDate = DateTime.Now; 
            bookTime.Text = "";
            bookDouble.IsChecked = false;
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
            if (currentSlot.Content != "")
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

        public void displayRandomAppointments()
        {
            string[] d0 = new string[23];
            string[] d1 = new string[23];
            string[] d2 = new string[23];
            string[] d3 = new string[23];
            string[] d4 = new string[23];

            Random random = new Random();
            int randomNumber = random.Next(0, 23);

            d0[randomNumber] = "Patient X"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Jones, Bob"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Daffy, Duck"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Smith, John"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Presley, Elvis"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Frank, Ryan"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Goldberg, Suzan"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Burns, Montgomery"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Francis, Joanne"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Gorgon, Medussa"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Willson, Mark"; randomNumber = random.Next(0, 23);
            d2[randomNumber] = "Nguyn, Lee"; randomNumber = random.Next(0, 23);
            d2[randomNumber] = "Miller, Sarah"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Alverez, Roberto"; randomNumber = random.Next(0, 23);
            d4[randomNumber] = "Rolandson, Peter"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Asimov, Issac"; randomNumber = random.Next(0, 23);
            d3[randomNumber] = "Obama, Barac"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Clindon, Hillary"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Gabriels, Cristina"; randomNumber = random.Next(0, 23);
            d2[randomNumber] = "Yong, Nikodim"; randomNumber = random.Next(0, 23);
            d3[randomNumber] = "Mohana, Noah"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Taide, Margert"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Trans, Judy"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Von Trappe, Julie"; randomNumber = random.Next(0, 23);
            d2[randomNumber] = "Andrews, Danny"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "De vito, Arnold"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Schwartsnager, Maria"; randomNumber = random.Next(0, 23);
            d2[randomNumber] = "Ford, Rob"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Ford, Henry"; randomNumber = random.Next(0, 23);
            d3[randomNumber] = "Pascal, Blaise"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Kepler, Jon"; randomNumber = random.Next(0, 23);
            d2[randomNumber] = "Bosonova, Jim"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "McNight, Tim"; randomNumber = random.Next(0, 23);
            d3[randomNumber] = "Boston, Eminem"; randomNumber = random.Next(0, 23);
            d4[randomNumber] = "Cuttings, Sam"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Mercier, Piere"; randomNumber = random.Next(0, 23);
            d1[randomNumber] = "Nenchi, Nahed"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Iginla, Jerome"; randomNumber = random.Next(0, 23);
            d4[randomNumber] = "Nathanels, Jessica"; randomNumber = random.Next(0, 23);
            d0[randomNumber] = "Bianca, Charles"; randomNumber = random.Next(0, 23);

            updateMainCalander(d0, d1, d2, d3, d4);
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



    }

}
