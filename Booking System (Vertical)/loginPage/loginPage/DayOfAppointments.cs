using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loginPage
{
    public class DayOfAppointments
    {
        public DateTime date;
        public Appointment[] d0 = new Appointment[23];
        public Appointment[] d1 = new Appointment[23];
        public Appointment[] d2 = new Appointment[23];
        public Appointment[] d3 = new Appointment[23];
        public Appointment[] d4 = new Appointment[23];
        

        public DayOfAppointments(DateTime date)
        {
            this.date = date;
        }

        

    }
}
