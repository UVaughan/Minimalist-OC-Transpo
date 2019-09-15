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

        public bool IsStation(int stopCode)
        {
            return GetStations().ContainsKey(stopCode);
        }

        public string GetStation(int stopCode)
        {
            return GetStations()[stopCode];
        }

        public IDictionary<int, string> GetStations() => new Dictionary<int, string>
        {
            {3039, "Airport"},
            {3004, "Albert and Bay"},
            {3002, "Albert and Bank"},
            {3003, "Albert and Kent"},
            {3001, "Albert and Metcalfe"},
            {3045, "Barrhaven Centre"},
            {3017, "Baseline"},
            {3050, "Bayshore"},
            {3060, "Bayview"},
            {3049, "Beatrice"},
            {3034, "Billings Bridge"},
            {3027, "Blair"},
            {3059, "Canadian Tire Centre"},
            {3061, "Carling"},
            {3062, "Carleton"},
            {3074, "Chapel Hill"},
            {3026, "Cyrville"},
            {3013, "Dominion"},
            {3055, "Eagleson"},
            {3043, "Fallowfield"},
            {3037, "Greenboro"},
            {3035, "Heron"},
            {3023, "Hurdman"},
            {3057, "Innovation"},
            {3016, "Iris"},
            {3070, "Jeanne d'Arc"},
            {3080, "Lansdowne TD Place"},
            {3020, "Laurier"},
            {3022, "Lees"},
            {3041, "Leitrim"},
            {3014, "Lincoln Fields"},
            {3046, "Longfields"},
            {3030, "Lycée Claudel"},
            {3051, "Lyon"},
            {3000, "Mackenzie King"},
            {3047, "Marketplace"},
            {3076, "Millennium"},
            {3042, "Moodie"},
            {3063, "Mooney's Bay"},
            {3048, "Nepean Woods"},
            {3028, "Orléans"},
            {3052, "Parliament"},
            {3010, "Pimisi (Lebreton)"},
            {3019, "Pinecrest"},
            {3075, "Place D'Orléans Park & Ride"},
            {3033, "Pleasant Park"},
            {3015, "Queensway"},
            {3009, "Rideau"},
            {3032, "Riverside"},
            {3040, "Riverview"},
            {3038, "South Keys"},
            {3005, "Slater and Bay"},
            {3007, "Slater and Bank"},
            {3006, "Slater and Kent"},
            {3008, "Slater and Metcalfe"},
            {3031, "Smyth"},
            {3025, "St-Laurent"},
            {3044, "Strandherd"},
            {3018, "Teron"},
            {3058, "Terry Fox"},
            {3024, "Tremblay (Via-Rail)"},
            {3029, "Trim"},
            {3011, "Tunney's Pasture"},
            {3021, "uOttawa"},
            {3036, "Walkley"},
            {3012, "Westboro"}
        };
    }
}
