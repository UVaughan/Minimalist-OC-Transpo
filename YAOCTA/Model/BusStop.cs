using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YAOCTA.Model
{
    public class BusStop
    {
        public string StopNo { get; set; }
        public string StopDescription { get; set; }
        public string Error { get; set; }
        public RouteWrapper Routes { get; set; }
    }
}
