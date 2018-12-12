using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Mother: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                if(value<1000)
                    throw new Exception("Id is too short");
                else
                    id = value;
            }
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
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
        public string HomeAddress { get; set; }
        public string WantedNannyAddress { get; set; }
        public Schedule[] WantedNannySchedule { get; set; }
        private string comments;
        public string Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                OnPropertyChange("Comments");
            }
        }

        public MotherStatus Status { get; set; }
        public bool IsSingleMother { get; set; }

        public Mother()
        {
            WantedNannySchedule = new Schedule[6];
            for (int i = 0; i < WantedNannySchedule.Length; i++)
            {
                WantedNannySchedule[i] = new Schedule() { Day = (Days)i };
            }
        }

        public override string ToString()
        {
            string result= this.ToStringProperty();

            result += "\nSchedule:\n";
            for (int i = 0; i < WantedNannySchedule.Length; i++)
            {
                if (WantedNannySchedule[i].IsWorkDay)
                {
                    result += WantedNannySchedule[i].Day + ": "; //print the days that nanny works
                    result += WantedNannySchedule[i].StartHour.ToString() + " - " + WantedNannySchedule[i].EndHour.ToString() + "\n";//print the hours per a day that nanny works
                }

            }

            return result;
        }

        public Mother GetCopy()
        {
            Mother m = new Mother();

            m= this.GetCopy<Mother>();

            for(int i=0; i<WantedNannySchedule.Length; i++)
            {
                m.WantedNannySchedule[i].Day = this.WantedNannySchedule[i].Day;
                m.WantedNannySchedule[i].IsWorkDay = this.WantedNannySchedule[i].IsWorkDay;
                m.WantedNannySchedule[i].StartHour = this.WantedNannySchedule[i].StartHour;
                m.WantedNannySchedule[i].EndHour = this.WantedNannySchedule[i].EndHour;
            }

            return m;
        }

        private void OnPropertyChange(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
