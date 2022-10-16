namespace Holidays_WebAPI.Models
{
    public struct DateFormatHoliday
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string DayOfWeek { get; set; }
    }

    public struct Name
    {
        public string Lang { get; set; }
        public string Text { get; set; }
    }
    public class Holiday
    {
        public DateFormatHoliday date { get; set; }
        public List<Name> Name { get; set; }
    }
}
