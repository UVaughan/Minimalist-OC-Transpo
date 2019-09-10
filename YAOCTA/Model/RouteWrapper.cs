using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YAOCTA.Utility;

namespace YAOCTA.Model
{
    public class RouteWrapper
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Route>))]
        public List<Route> Route { get; set; }
    }
}
