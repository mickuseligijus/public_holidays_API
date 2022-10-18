namespace Holidays_WebAPI.Models.DbModels
{
    public class Holiday
    {
        public int HolidayId { get; set; }

        public string Date { get; set; } 

        public string HolidayType {get; set; }
        public List<Name> Names { get; set; }

        public int CountryID { get; set; }

     
    }
}
