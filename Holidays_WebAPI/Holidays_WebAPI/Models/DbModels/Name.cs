namespace Holidays_WebAPI.Models.DbModels
{
    public class Name
    {
        public int NameId { get; set; }
        public string Lang { get; set; }
        public string Text { get; set; }

        public int HolidayId { get; set; }
    }
}
