namespace Holidays_WebAPI.Models.DbModels
{
    public class CountryMax
    {
        public int CountryMaxId { get; set; }
        public string Year { get; set; }
        public int MaxNumber { get; set; }

        public int CountryId { get; set; }
    }
}
