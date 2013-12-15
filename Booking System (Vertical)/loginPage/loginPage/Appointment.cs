using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loginPage
{
    public class Appointment
    {
        public DateTime date;
        public int doctor;  //between 0-4
        public int timeslot; //between 0-22
        public string notes;
        public Patient patient;
        public bool doubleBooked;
        public Appointment doubleBookedAppt;


        public Appointment()
        {}
        public Appointment(DateTime date)
        {
            this.date = date;
        }

        public Appointment(Patient p, int doctor, int timeslot, string notes, DateTime date)
        {
            this.doctor=doctor;
            this.timeslot=timeslot;
            this.notes=notes;
            this.patient = p;
            this.date = date;
            this.doubleBooked = false;
        }


        public string Timeslot()
        {
            switch (timeslot)
            {
                case 0: return "8:00 - 8:20";
                case 1: return "8:20 - 8:40";
                case 2: return "8:40 - 9:00";
                case 3: return "9:00 - 9:20";
                case 4: return "9:20 - 9:40";
                case 5: return "9:40 - 10:00";
                case 6: return "10:00 - 10:20";
                case 7: return "10:20 - 10:40";
                case 8: return "10:40 - 11:00";
                case 9: return "11:00 - 11:20";
                case 10: return "11:20 - 11:40";
                case 11: return "11:40 - 12:00";
                case 12: return "12:00 - 12:20";
                case 13: return "12:20 - 12:40";
                case 14: return "12:40 - 1:00";
                case 15: return "1:00 - 1:20";
                case 16: return "1:20 - 1:40";
                case 17: return "1:40 - 2:00";
                case 18: return "2:00 - 2:20";
                case 19: return "2:20 - 2:40";
                case 20: return "2:40 - 3:00";
                case 21: return "3:00 - 3:20";
                case 22: return "3:20 - 3:40";
                default: return "unknown_Timeslot";
            }
        }

        public string DoctorName()
        {
            return docNumToName(doctor);
        }

        public string docNumToName(int num)
        {
            switch (num)
            {
                case 0: return "Dr. Phillips";
                case 1: return "Dr. Strumpfer";
                case 2: return "Dr. Cole";
                case 3: return "Nurse Ratched";
                case 4: return "Dr. Honeydew";
                default: return "unknown_MD";
            }
            
        }

        public override string ToString()
        {
            string output = "";
            output += date.ToShortDateString() + Timeslot() + docNumToName(doctor);
            return date.ToShortDateString().PadRight(20 - date.ToShortDateString().Length) + "\t" + Timeslot().PadRight(20 - Timeslot().Length) + "\t" + docNumToName(doctor);
       
        }


    }
}
