using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Schedule
    {
        public Days Day { get; set; }
        public bool IsWorkDay { get; set; }

        // Public Property - XmlIgnore as it doesn't serialize anyway
        [XmlIgnore]
        public TimeSpan StartHour { get; set; }
        // Public Property - XmlIgnore as it doesn't serialize anyway
        [XmlIgnore]
        public TimeSpan EndHour { get; set; }
        // Pretend property for serialization
        [XmlElement("StartHour")]
        public long StartHourTicks
        {
            get { return StartHour.Ticks; }
            set { StartHour = new TimeSpan(value); }
        }
        [XmlElement("EndHour")]
        public long EndHourTicks
        {
            get { return EndHour.Ticks; }
            set { EndHour = new TimeSpan(value); }
        }
        public Schedule()
        {

            StartHour = new TimeSpan(0, 0, 0);
         EndHour = new TimeSpan(0,0,0);
        }
    }
}
