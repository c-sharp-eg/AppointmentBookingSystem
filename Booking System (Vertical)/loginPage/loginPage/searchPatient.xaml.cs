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
    /// Interaction logic for searchPatient.xaml
    /// </summary>
    public partial class searchPatient : Window
    {
        Object caller;
        mainCalendarDisplayWindow mainCal;
        public Patient patient;

        public searchPatient()
        {
            InitializeComponent();
        }

        public searchPatient(editPatient input)
        {
            this.caller = new editPatient();
            this.caller = input;
            this.mainCal = ((editPatient)this.caller).caller;
            InitializeComponent();
            displayPatients();
        }

        public searchPatient(mainCalendarDisplayWindow input)
        {
            this.caller = new mainCalendarDisplayWindow();
            this.caller = input;
            this.mainCal = (mainCalendarDisplayWindow)this.caller;
            InitializeComponent();
            displayPatients();
        }

        public searchPatient(pastAppointments input)
        {
            this.caller = new pastAppointments();
            this.caller = input;
            InitializeComponent();
        }

        private void searchCancelButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void selectButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (caller != null)
            {
                if (caller.GetType() == typeof(editPatient))
                {
                    editPatient temp = new editPatient();
                    temp = (editPatient)caller;
                    if (searchPatLB.SelectedItem != null)
                    {
                        patient = (Patient)searchPatLB.SelectedItem;
                        temp.selectedPatient = patient;
                        temp.ShowPatient(patient);
                    }
                    temp.patientSelected = true;
                }
                else if (caller.GetType() == typeof(mainCalendarDisplayWindow))
                {
                    mainCalendarDisplayWindow temp = new mainCalendarDisplayWindow();
                    temp = (mainCalendarDisplayWindow)caller;
                    if (searchPatLB.SelectedItem!= null)
                        temp.selectedPatient = (Patient)searchPatLB.SelectedItem;
                    if (temp.selectedPatient!=null)
                        temp.bookNewName.Text = "<" + temp.selectedPatient.lastName + "," + temp.selectedPatient.firstName + ">";
                }
                else if (caller.GetType() == typeof(pastAppointments))
                {
                    pastAppointments temp = new pastAppointments();
                    temp = (pastAppointments)caller;
                    temp.pastPatientName.Text="<"+patient.lastName+","+patient.firstName+">";
                }
                
            }
            else
            {
                var mb = MessageBox.Show("caller.gettype is null");
            }
            this.Close();

        }

        public void displayPatients()
        {
            List<Patient> patients;
            try
            {
                mainCalendarDisplayWindow calandar;
                calandar = (mainCalendarDisplayWindow)mainCal;
                patients = calandar.patients;
                
                foreach (Patient p in patients)
                {
                    string pFirstName = p.firstName;
                    string pLastName = p.lastName;
                    string pAddress = p.address;
                    searchPatLB.Items.Add(pFirstName.PadRight(20-pFirstName.Length) + "\t" + pLastName.PadRight(20-pLastName.Length) + "\t" + pAddress);
                    
                }
                                
            }
            catch (Exception err)
            {
                var mb = MessageBox.Show(err.ToString());
            }
        }


         
    }
}
