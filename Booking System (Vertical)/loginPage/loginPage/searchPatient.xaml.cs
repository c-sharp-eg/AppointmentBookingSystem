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
    /// Interaction logic for searchPatient.xaml
    /// </summary>
    public partial class searchPatient : Window
    {
        Object caller;
        public Patient patient;

        public searchPatient()
        {
            InitializeComponent();
        }

        public searchPatient(editPatient input)
        {
            this.caller = new editPatient();
            this.caller = input;
            InitializeComponent();
        }

        public searchPatient(mainCalendarDisplayWindow input)
        {
            this.caller = new mainCalendarDisplayWindow();
            this.caller = input;
            InitializeComponent();
            dislpayPatients();
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
                    temp.ShowPatient(patient);
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

        public void dislpayPatients()
        {
            List<Patient> patients;
            try
            {
                mainCalendarDisplayWindow calandar;
                calandar = (mainCalendarDisplayWindow)caller;
                patients = calandar.patients;
                
                foreach (Patient p in patients)
                {
                    searchPatLB.Items.Add(p);
                }
                                
            }
            catch (Exception err)
            {
                var mb = MessageBox.Show(err.ToString());
            }
        }


         
    }
}
