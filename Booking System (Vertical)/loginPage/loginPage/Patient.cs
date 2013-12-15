using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loginPage
{
      
    public class Patient
    {
        public string firstName;
        public string lastName;
        public string sex;
        public string address;
        public int areaCode;
        public int phoneNumber;
        public string country;
        public string province;
        public string city;
        public int dobMM;
        public int dobDD;
        public int dobYYYY;
        public int healthcare;
        public string notes;
        public List<Appointment> appointments = new List<Appointment>();

        

        public Patient(string firstName, string lastName, string sex,
                    string address, int areaCode, int phoneNumber, string country,
                    string province, string city, int dobMM, int dobDD, int dobYYYY, int healthcare, string notes)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.address = address;
            this.areaCode = areaCode;
            this.phoneNumber = phoneNumber;
            this.country = country;
            this.province = province;
            this.city = city;
            this.dobMM = dobMM;
            this.dobDD = dobDD;
            this.dobYYYY = dobYYYY;
            this.healthcare = healthcare;
            this.notes = notes;
        }

        override public string ToString()
        {
            return firstName.PadRight(20 - firstName.Length) + "\t" + lastName.PadRight(20 - lastName.Length) + "\t" + address;
        }

    }
}
