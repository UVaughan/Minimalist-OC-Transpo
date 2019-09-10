using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAOCTA.Utility
{
    public static class StringManipulation
    {
        /// <summary>
        /// Formats stop name
        /// </summary>
        /// <param name="stopName">Raw stop name from OC API</param>
        /// <returns>More readable stop name</returns>
        public static string FormatStopName(this string stopName)
        {
            var trimmed = stopName.Trim();
            var capitalized = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(trimmed.ToLower());
            var replacedSlashes = capitalized.Replace("/", "and");
            var removedQuotes = replacedSlashes.Replace("\"", "");
            return removedQuotes;
        }

        /// <summary>
        /// Extension method to remove french accents from string
        /// NOTE: Not suitable for other uses, only does lower case characters present in OC Transpo stop names
        /// </summary>
        /// <param name="input">String to modify</param>
        /// <returns>String with no accents</returns>
        public static string RemoveFrenchCharacters(this string input)
        {
            return input.Replace('é', 'e').Replace('ô', 'o').Replace('è', 'e');
        }
    }
}
