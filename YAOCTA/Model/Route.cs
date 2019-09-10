using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YAOCTA.Utility;

namespace YAOCTA.Model
{
    public class Route
    {
        public string RouteNo { get; set; }
        public int DirectionID { get; set; }
        public string Direction { get; set; }
        public string RouteHeading { get; set; }

        [JsonConverter(typeof(TripConverter))]
        public List<Trip> Trips { get; set; }
    }
}
