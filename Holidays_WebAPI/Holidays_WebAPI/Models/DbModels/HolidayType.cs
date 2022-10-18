namespace Holidays_WebAPI.Models.DbModels
{
    public class HolidayType
    {
        public int HolidayId { get; set; }
        public string HolidayTypeName { get; set; }

        public int CountryId { get; set; }
    }
}
