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
    /// Interaction logic for cancelConfirmation.xaml
    /// </summary>
    public partial class cancelConfirmation : Window
    {
        mainCalendarDisplayWindow caller;
        
        public cancelConfirmation()
        {
            InitializeComponent();
        }
        
        public cancelConfirmation(mainCalendarDisplayWindow caller)
        {
            this.caller = caller;
            InitializeComponent();
        }

        private void yesCancel_Click(object sender, RoutedEventArgs e)
        {
            mainCalendarDisplayWindow mainCal = (mainCalendarDisplayWindow)caller;
            DayOfAppointments dayOfAppt;
            bool containsDate = mainCal.allAppointmentsDictionary.TryGetValue(mainCal.displayDate.ToShortDateString(), out dayOfAppt);
            if (!containsDate)
            {
                var mb = MessageBox.Show("Error: No appointment on this date");
                return; //quit the method on error
            }
            Appointment appt;
            int doctor = mainCal.timeslotNametoDoctor(mainCal.currentSlot.Name);
            int timeslot = mainCal.timeslotNametoIndex(mainCal.currentSlot.Name);

            if (doctor == 0)
            {
                appt = dayOfAppt.d0[timeslot];
                dayOfAppt.d0[timeslot] = null;
                //delete appointment from patient

                int numAppt = 0;
                foreach (Appointment app in appt.patient.appointments)
                    numAppt++;
                for (int i = 0; i<numAppt;i++)
                {
                    Appointment app = appt.patient.appointments[i];
                    if (app.Equals(appt))
                        appt.patient.appointments.Remove(app);
                }

            
            }
            else if (doctor == 1)
            {
                appt = dayOfAppt.d1[timeslot];
                dayOfAppt.d1[timeslot] = null;
                //delete appointment from patient
                int numAppt = 0;
                foreach (Appointment app in appt.patient.appointments)
                    numAppt++;
                for (int i = 0; i < numAppt; i++)
                {
                    Appointment app = appt.patient.appointments[i];
                    if (app.Equals(appt))
                        appt.patient.appointments.Remove(app);
                }

            
            }
            else if (doctor == 2)
            {
                appt = dayOfAppt.d2[timeslot];
                dayOfAppt.d2[timeslot] = null;
                //delete appointment from patient
                int numAppt = 0;
                foreach (Appointment app in appt.patient.appointments)
                    numAppt++;
                for (int i = 0; i < numAppt; i++)
                {
                    Appointment app = appt.patient.appointments[i];
                    if (app.Equals(appt))
                        appt.patient.appointments.Remove(app);
                }

            
            }
            else if (doctor == 3)
            {
                appt = dayOfAppt.d3[timeslot];
                dayOfAppt.d3[timeslot] = null;
                //delete appointment from patient
                int numAppt = 0;
                foreach (Appointment app in appt.patient.appointments)
                    numAppt++;
                for (int i = 0; i < numAppt; i++)
                {
                    Appointment app = appt.patient.appointments[i];
                    if (app.Equals(appt))
                        appt.patient.appointments.Remove(app);
                }

            
            }
            else if (doctor == 4)
            {
                appt = dayOfAppt.d4[timeslot];
                dayOfAppt.d4[timeslot] = null;
                //delete appointment from patient
                int numAppt = 0;
                foreach (Appointment app in appt.patient.appointments)
                    numAppt++;
                for (int i = 0; i < numAppt; i++)
                {
                    Appointment app = appt.patient.appointments[i];
                    if (app.Equals(appt))
                        appt.patient.appointments.Remove(app);
                }

            
            }
            else
            {
                var mb = MessageBox.Show("Error: doctor timeslot in cancelConfirmation");//should never happen
            }

            //update display
            mainCal.refreshDisplayDate();
            mainCal.updateApptInfoBox();

            this.Close();

        }

        private void noCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
