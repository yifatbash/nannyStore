using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static int contractIdCode = 10000001;
        private int contractId;
        public int ContractId
        {
            get { return contractId; }
            set
            {
                contractId = value;
                OnPropertyChange("ContractId");
            }
        }
        private int nannyId;
        public int NannyId
        {
            get { return nannyId; }
            set
            {
                nannyId = value;
                OnPropertyChange("NannyId");
            }
        }
        private int childId;
        public int ChildId
        {
            get { return childId; }
            set
            {
                childId = value;
                OnPropertyChange("ChildId");
            }
        }
        public bool IsIntroductoryMeeting { get; set; }
        public bool IsSignedContract { get; set; }
        public PaymentPer PayPerHourOrMonth { get; set; }//how the mother want to calculate the payment- per hour or per month
        private float weeklyHours; //How many weekly hours the nanny takes care about the child
        public float WeeklyHours
        {
            get { return weeklyHours; }
            set
            {
                if (value >= 0)
                {
                    weeklyHours = value;
                    OnPropertyChange("WeeklyHours");
                }
                else
                    throw new Exception("weeklyHours can't be negative");
            }
        }
        private float? hourlyRate;//if nanny doesn't allow pay per hour the HourlyRate is null
        public float? HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (value >= 0 || value==null)
                {
                    hourlyRate = value;
                    OnPropertyChange("HourlyRate");
                }
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
                {
                    monthlyRate = value;
                    OnPropertyChange("MonthlyRate");
                }
                else
                    throw new Exception("monthlyRate can't be negative");
            }
        }
        public DateTime StartContractDate { get; set; }
        public DateTime EndContractDate { get; set; }
        public WayOfPayment PaidBy { get; set; }//Cash,BankTransfer,CreditCard
        private float netoRate;
        public float NetoRate
        {
            get { return netoRate; }
            set
            {
                netoRate = value;
                OnPropertyChange("NetoRate");
            }
        }

        public Contract()
        {
            StartContractDate = DateTime.Now;
            EndContractDate = DateTime.Now.AddYears(1);
            
        }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

        public Contract GetCopy()
        {
            return this.GetCopy<Contract>();
        }

        private void OnPropertyChange(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
