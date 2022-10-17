namespace Holidays_WebAPI.Models.JsonModels
{
    public struct Name
    {
        public string Lang { get; set; }
        public string Text { get; set; }
    }
    public class HolidayJson
    {
        public DateFormatHoliday Date { get; set; }
        public List<Name> Name { get; set; }
        public string HolidayType { get; set; }
    }
}
