
using System.ComponentModel.DataAnnotations;

namespace Holidays_WebAPI.Models.DbModels
{
    public class HolidayType
    {
        [Key]
        public int HolidayId { get; set; }
        public string HolidayTypeName { get; set; }

        public int CountryId { get; set; }
    }
}
