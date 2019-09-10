using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YAOCTA.Utility;

namespace YAOCTA.Model
{
    public class TripWrapper
    {
        [JsonConverter(typeof(TripConverter))]
        public List<Trip> Trip { get; set; }
    }
}
