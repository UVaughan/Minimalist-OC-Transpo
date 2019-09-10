using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using YAOCTA.Model;
using YAOCTA.Utility;
namespace YAOCTA.Data
{
    public class BusStopService
    {

        private const string StopNumberParameter = "stopNo";
        private const string StopInfoEndpoint = "GetNextTripsForStopAllRoutes";

        public async Task<BusStop> GetStopInfo(int stopNumber)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add(StopNumberParameter, stopNumber.ToString());
            var result = await ApiClient.SendRequest<RouteSummaryForStopWrapper>(StopInfoEndpoint, parameters);
            return result.GetRouteSummaryForStopResult;
        }
        public IDictionary<int, string> GetAllStops()
        {
            // get hardcoded stations first, stop data is incorrect for them
            var dict = GetStations();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("stops.txt"));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                var lines = result.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
                );
                lines = lines.Skip(1).ToArray();
                foreach (var line in lines)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    var terms = line.Split(',');
                    try
                    {
                        var key = Int32.Parse(terms[1]);
                        var value = terms[2];

                        if (dict.ContainsKey(key))
                        {
                            continue;
                        }

                        dict.Add(key, value);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Could not extract integer from " + terms[1]);
                    }
                }
                return dict;
            }
        }

        public IDictionary<int, string> GetStations() => new Dictionary<int, string>
        {
            {3039, "Airport"},
            {3004, "Albert / Bay"},
            {3002, "Albert / Bank"},
            {3003, "Albert / Kent"},
            {3001, "Albert / Metcalfe"},
            {3045, "Barrhaven Centre"},
            {3017, "Baseline"},
            {3050, "Bayshore"},
            {3060, "Bayview"},
            {3034, "Billings Bridge"},
            {3027, "Blair"},
            {3061, "Carling"},
            {3062, "Carleton"},
            {3013, "Dominion"},
            {3055, "Eagleson"},
            {3043, "Fallowfield"},
            {3037, "Greenboro"},
            {3035, "Heron"},
            {3023, "Hurdman"},
            {3016, "Iris"},
            {3070, "Jeanne d'Arc"},
            {3020, "Laurier"},
            {3010, "LeBreton"},
            {3022, "Lees"},
            {3041, "Leitrim"},
            {3014, "Lincoln Fields"},
            {3046, "Longfields"},
            {3030, "Lycée Claudel"},
            {3000, "Mackenzie King"},
            {3047, "Marketplace"},
            {3076, "Millennium"},
            {3063, "Mooney's Bay"},
            {3048, "Nepean Woods"},
            {3028, "Orléans"},
            {3075, "Orléans Park & Ride"},
            {3019, "Pinecrest"},
            {3033, "Pleasant Park"},
            {3015, "Queensway"},
            {3009, "Rideau Centre"},
            {3032, "Riverside"},
            {3040, "Riverview"},
            {3059, "Canadian Tire Centre"},
            {3038, "South Keys"},
            {3005, "Slater / Bay"},
            {3007, "Slater / Bank"},
            {3006, "Slater / Kent"},
            {3008, "Slater / Metcalfe"},
            {3031, "Smyth"},
            {3025, "St. Laurent"},
            {3044, "Strandherd"},
            {3018, "Teron"},
            {3058, "Terry Fox"},
            {3024, "Train"},
            {3029, "Trim"},
            {3011, "Tunney's Pasture"},
            {3036, "Walkley"},
            {3012, "Westboro"}
        };
    }
}
