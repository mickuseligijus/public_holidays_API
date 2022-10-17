using Holidays_WebAPI.Models;
using Holidays_WebAPI.Models.JsonModels;

namespace Holidays_WebAPI.Services
{
    public interface IHolidayService
    {
        public Task<List<string>> GetCountriesAsync();
        public Task<List<IGrouping<string, HolidayJson>>> GetHolidaysForSpecificCountryAsync(string countryCode, string year);
        public Task<string> GetSpecificDayStatusAsync(string countryCode, string date);
        public Task<int> GetMaximumFreeDaysInRow(string countryCode, string year);

    }
}
