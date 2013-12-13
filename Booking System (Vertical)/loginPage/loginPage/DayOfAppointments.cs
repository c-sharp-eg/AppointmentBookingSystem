using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loginPage
{
    public class DayOfAppointments
    {
        public DateTime date;
        public List<Appointment> d0 = new List<Appointment>(23);
        public List<Appointment> d1 = new List<Appointment>(23);
        public List<Appointment> d2 = new List<Appointment>(23);
        public List<Appointment> d3 = new List<Appointment>(23);
        public List<Appointment> d4 = new List<Appointment>(23);
        
        public DayOfAppointments(DateTime date)
        {
            this.date = date;
        }

        

    }
}
