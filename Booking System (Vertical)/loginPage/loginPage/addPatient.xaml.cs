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
        public mainCalendarDisplayWindow caller;

        public addPatient(mainCalendarDisplayWindow caller)
        {
            this.caller = caller;
            InitializeComponent();
        }


        //this methode reads the feilds (checks they are valid) and creates a new patient
        //which it saves in mainCalanderDisplayWindow.Patients
        private void addSaveButton_Click(object sender, RoutedEventArgs e)
        {

            //TODO: ADD ERROR CHCEKING other wise crash;
            Patient p=null;
            try
            {
                p = new Patient(this.addFirst.Text, this.addLast.Text,
                     this.addMale.IsSelected ? "M" : "F", this.addAddress.Text, int.Parse(this.addArea.Text),
                     int.Parse(this.addPhone.Text), this.addCountry.Text, this.addProvince.Text,
                     this.addCity.Text, int.Parse(this.addMonthBox.Text), int.Parse(this.addDayBox.Text),
                     int.Parse(this.addYearBox.Text), int.Parse(this.addNo.Text), this.addNotes.Text);
            }
            catch (Exception err)
            {
                var mb2 = MessageBox.Show(err.ToString());
                return;
            }

            if ((caller != null) && (p != null))
                caller.patients.Add(p);
            else
            {
                var mb1 = MessageBox.Show("Caller was null");
            }

            string pats = "";
            foreach (Patient patient in caller.patients)
            {
                pats += "\n : " + patient.ToString();
            }
            var mb = MessageBox.Show(pats);
            this.Close();
        }

        private void addCancelButton_Click(object sender, RoutedEventArgs e)
        {
            closeActivate();
        }

        public void closeActivate()
        {
            this.Close();
        }
    }
}
