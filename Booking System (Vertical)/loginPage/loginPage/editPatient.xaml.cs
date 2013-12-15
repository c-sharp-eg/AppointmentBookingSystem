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
        //variables
        public Patient selectedPatient;
        public Boolean patientSelected;
        searchPatient searchPat = new searchPatient();
        public mainCalendarDisplayWindow caller;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            searchPat.Close();
        }

        public editPatient(mainCalendarDisplayWindow caller)
        {
            this.caller = caller;
            InitializeComponent();
            patientSelected = false;
        }
        public editPatient()
        {
            InitializeComponent();
            patientSelected = false;
        }

        private void editCancelButton_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            this.Close();
        }

        private void editSelectButton_Click(object sender, RoutedEventArgs e)
        {
            searchPat.Close();
            searchPat = new searchPatient(this);
            searchPat.Show();
        }

        private void editSaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedPatient.firstName = this.editFirst.Text;
                selectedPatient.lastName = this.editLast.Text;
                selectedPatient.sex = this.editMale.IsSelected ? "M" : "F";
                selectedPatient.address = this.editAddress.Text;
                selectedPatient.areaCode = int.Parse(this.editArea.Text);
                selectedPatient.phoneNumber = int.Parse(this.editPhone.Text);
                selectedPatient.country= this.editCountry.Text;
                selectedPatient.province = this.editProvince.Text;
                selectedPatient.city = this.editCity.Text;
                selectedPatient.dobMM = int.Parse(this.editMonthBox.Text);
                selectedPatient.dobDD = int.Parse(this.editDayBox.Text);
                selectedPatient.dobYYYY = int.Parse(this.editYearBox.Text);
                selectedPatient.healthcare = int.Parse(this.editNo.Text);
                selectedPatient.notes = this.editNotes.Text;

            }
            catch(Exception err)
            {
                var mb = MessageBox.Show("TODO: add error message");
            }
            this.Close();
        }
        public void ShowPatient(Patient p)
        {
             
            this.editFirst.Text = p.firstName;
            this.editLast.Text = p.lastName;
            if (p.sex=="M") this.editMale.IsSelected=true;
            else if (p.sex=="F") this.editFemale.IsSelected=true;
            this.editAddress.Text = p.address;
            this.editArea.Text = p.areaCode.ToString("G");
            this.editPhone.Text = p.phoneNumber.ToString("G");
            this.editCountry.Text = p.country;
            this.editProvince.Text = p.province;
            this.editCity.Text = p.city;
            this.editMonthBox.Text = p.dobMM.ToString("G");
            this.editDayBox.Text = p.dobDD.ToString("G");
            this.editYearBox.Text = p.dobYYYY.ToString("G");
            this.editNo.Text = p.healthcare.ToString("G");
            this.editNotes.Text = p.notes;
              
        }


        public void errorMsgNoPatientSelected(object sender, EventArgs e)
        {
            if (!patientSelected)
            {
                var mb = MessageBox.Show("Please click \"Selected Patient\" first to select a patient to edit");
                editSelectButton.Focus();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                caller.Effect = null;
            }
            catch (Exception err)
            {
            }
        }



    }
}
