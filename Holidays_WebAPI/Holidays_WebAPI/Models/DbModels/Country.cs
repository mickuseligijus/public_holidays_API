using System.ComponentModel.DataAnnotations;

namespace Holidays_WebAPI.Models.DbModels
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string DateFrom { get; set; }
        [Required]
        public string DateTo { get; set; }
        public ICollection<Region> Regions { get; set; }
        public ICollection<HolidayType> HolidayTypes { get; set; }



    }
}
