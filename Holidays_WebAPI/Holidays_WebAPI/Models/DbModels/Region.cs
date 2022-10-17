namespace Holidays_WebAPI.Models.DbModels
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public int CountryId { get; set; }
    }
}
