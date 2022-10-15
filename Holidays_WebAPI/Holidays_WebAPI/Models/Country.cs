using Microsoft.VisualBasic;
using System.Data;

namespace Holidays_WebAPI.Models
{
    public struct DateFormat
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public DateFormat(string day, string month, string year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }

    public class Country
    {

        public Country(string countryCode, List<string> regions, List<string> holidayTypes, string fullName, DateFormat fromDate, DateFormat toDate)
        {
            CountryCode = countryCode;
            Regions = regions;
            HolidayTypes = holidayTypes;
            FullName = fullName;
            FromDate = fromDate;
            ToDate = toDate;
        }


        public string CountryCode { get; set; }

        public List<string>? Regions { get; set; }
        public List<string>? HolidayTypes { get; set; }
        public string FullName { get; set; }

        public DateFormat FromDate { get; set; }

        public DateFormat ToDate { get; set; }

    }
}
