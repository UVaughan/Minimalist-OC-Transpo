using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YAOCTA.Model
{
    public class Trip
    {
        public string TripDestination { get; set; }
        public string TripStartTime { get; set; }
        public string AdjustedScheduleTime { get; set; }
        public string AdjustmentAge { get; set; }
        public bool LastTripOfSchedule { get; set; }
        public string BusType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GPSSpeed { get; set; }
    }
}
