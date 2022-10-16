using Holidays_WebAPI.Models;

namespace Holidays_WebAPI.Services
{
    public interface IHolidayService
    {
        public Task<List<string>> GetCountriesAsync();
        public Task<List<IGrouping<string, Holiday>>> GetHolidaysForSpecificCountryAsync(string countryCode, string year);
        public Task<string> GetSpecificDayStatus(string date, string countryCode);
        public int GetMaximumFreeDaysInRow(string countryName, int year);

    }
}
