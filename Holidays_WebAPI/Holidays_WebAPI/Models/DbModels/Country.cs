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
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public ICollection<Region> Regions { get; set; }
        public ICollection<Name> Names { get; set; }



    }
}
