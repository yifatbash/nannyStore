using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Nanny
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new Exception("Id can't be negative");
                if (value > 999999999)
                    throw new Exception("Id can't be more than 9 digits");
                if (value < 1000)
                    throw new Exception("Id is too short");
                else
                    id = value;
            }
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value < DateTime.Now)
                    birthDate = value;
                else
                    throw new Exception("birth date is not possible");
            }
        }
        public string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == false)
                    throw new Exception("Wrong phone number!! (Phone number should include just digits, no more then 10 digits)");
                else
                    phoneNumber = value;
            }
        }
        public string Address { get; set; } 
        public bool IsElevator { get; set; }
        public int Floor { get; set; }
        private int numberOfExperienceYears;
        public int NumberOfExperienceYears
        {
            get { return numberOfExperienceYears; }
            set
            {
                if (value >= 0)
                    numberOfExperienceYears = value;
                else
                    throw new Exception("number of experience years can't be negative");
            }
        }
        private int maxNumOfChildren;
        public int MaxNumOfChildren
        {
            get { return maxNumOfChildren; }
            set
            {
                if (value >= 0)
                    maxNumOfChildren = value;
                else
                    throw new Exception("maxNumOfChildren can't be negative");
            }
        }
        private int minAge;
        public int MinAge
        {
            get { return minAge; }
            set
            {
                if (value >= 0)
                    minAge = value;
                else
                    throw new Exception("minAge can't be negative");
            }
        }
        private int maxAge;
        public int MaxAge
        {
            get { return maxAge; }
            set
            {
                if (value >= 0)
                    maxAge = value;
                else
                    throw new Exception("maxAge can't be negative");
            }
        }
        public bool EnanblePayForHour { get; set; }
        private float? hourlyRate;
        public float? HourlyRate //incase mother pays for hour
        {
            get { return hourlyRate; }
            set
            {
                if (value >= 0 || value==null)
                    hourlyRate = value;
                else
                    throw new Exception("hourlyRate can't be negative");
            }
        }
        private float monthlyRate;
        public float MonthlyRate
        {
            get { return monthlyRate; }
            set
            {
                if (value >= 0)
                    monthlyRate = value;
                else
                    throw new Exception("monthlyRate can't be negative");
            }
        }
        public Schedule[] NannySchedule { get; set; }
        public bool IsVacationAsTMT { get; set; }
        public string Recommendations { get; set; }
        public string SpecialActivities { get; set; }
        public bool IsKosherFood { get; set; }
        public bool IsReligiousEducation { get; set; }
        public string Comments { get; set; }

        public Nanny()
        {
            NannySchedule = new Schedule[6];
            for(int i=0; i< NannySchedule.Length; i++)
            {
                NannySchedule[i] = new Schedule() { Day = (Days)i };
            }
            BirthDate = DateTime.Parse("1.1.1970");

       }

        public override string ToString()
        {
            string result = this.ToStringProperty();

            result += "\nSchedule:\n";
            for (int i = 0; i < NannySchedule.Length; i++)
            {
                if (NannySchedule[i].IsWorkDay)
                {
                    result += NannySchedule[i].Day + ": "; //print the days that nanny works
                    result += NannySchedule[i].StartHour.ToString() + " - " + NannySchedule[i].EndHour.ToString() + "\n";//print the hours per a day that nanny works
                }

            }

            return result;
        }

        public Nanny GetCopy()
        {
            Nanny n = new Nanny();

            n = this.GetCopy<Nanny>();

            for (int i = 0; i < NannySchedule.Length; i++)
            {
                n.NannySchedule[i].Day = this.NannySchedule[i].Day;
                n.NannySchedule[i].IsWorkDay = this.NannySchedule[i].IsWorkDay;
                n.NannySchedule[i].StartHour = this.NannySchedule[i].StartHour;
                n.NannySchedule[i].EndHour = this.NannySchedule[i].EndHour;
            }

            return n;
        }
    }
}
