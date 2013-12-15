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

        public override string ToString()
        {
            string output="";
            for (int i = 0; i < 23; i++)
            {
                if (d0[i] != null) output += d0[i] + "\t";
                else output += "\t\t";
                if (d1[i] != null) output += d0[i] + "\t";
                else output += "\t\t";
                if (d2[i] != null) output += d0[i] + "\t";
                else output += "\t\t";
                if (d3[i] != null) output += d0[i] + "\t";
                else output += "\t\t";
                if (d4[i] != null) output += d0[i] + "\t";
                else output += "\t\t";
                output += "\n";
            }
            return output;
        }

        

    }
}
