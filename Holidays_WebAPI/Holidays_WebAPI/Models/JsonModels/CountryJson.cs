namespace Holidays_WebAPI.Models.JsonModels
{
    public class CountryJson
    {

        public CountryJson(string countryCode, List<string> regions, List<string> holidayTypes, string fullName, DateFormat fromDate, DateFormat toDate)
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
