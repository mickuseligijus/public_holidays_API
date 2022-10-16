using Holidays_WebAPI.Models;

namespace Holidays_WebAPI.Services
{
    public interface IHolidayService
    {
        public Task<List<string>> GetCountriesAsync();
        public Task<List<IGrouping<string, Holiday>>> GetHolidaysForSpecificCountryAsync(string countryCode, string year);
        public Task<string> GetSpecificDayStatusAsync(string date, string countryCode);
        public Task<int> GetMaximumFreeDaysInRow(string countryCode, string year);

    }
}
