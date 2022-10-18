namespace Holidays_WebAPI.Models.JsonModels
{
    public class CountryJson
    {

        public string CountryCode { get; set; }

        public List<string>? Regions { get; set; }
        public List<string>? HolidayTypes { get; set; }
        public string FullName { get; set; }

        public DateFormat FromDate { get; set; }

        public DateFormat ToDate { get; set; }

    }
}
