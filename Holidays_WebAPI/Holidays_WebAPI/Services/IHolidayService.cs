using Holidays_WebAPI.Models;

namespace Holidays_WebAPI.Services
{
    public interface IHolidayService
    {
        public Task<List<string>> GetCountriesAsync();
        public Task<List<IGrouping<string, Holiday>>> GetHolidaysForSpecificCountryAsync(string countryName, string year);
        public string GetSpecificDayStatus(DateTime date);
        public int GetMaximumFreeDaysInRow(string countryName, int year);

    }
}
