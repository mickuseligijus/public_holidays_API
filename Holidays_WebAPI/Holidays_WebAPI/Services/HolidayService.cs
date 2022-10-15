using Holidays_WebAPI.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace Holidays_WebAPI.Services
{

    public class HolidayService : IHolidayService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<List<string>> GetCountriesAsync()
        {
            string responseBody = await client.GetStringAsync("https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries");

            var countries = JsonConvert.DeserializeObject<List<Country>>(responseBody);

            var countriesList = countries.Select(c => c.FullName).ToList();

            return countriesList;
        }

        public async Task<List<IGrouping<string, Holiday>>> GetHolidaysForSpecificCountryAsync(string countryName, string year)
        {

            var uri = $"https://kayaposoft.com/enrico/json/v2.0/?action=getHolidaysForYear&year={year}&country={countryName}&holidayType=all";

            string responseBody = await client.GetStringAsync(uri);

            var holidays = JsonConvert.DeserializeObject<List<Holiday>>(responseBody);

            var groupedHolidays = holidays.GroupBy(h => h.date.Month).ToList();

            return groupedHolidays;
        }

        public int GetMaximumFreeDaysInRow(string countryName, int year)
        {
            throw new NotImplementedException();
        }

        public string GetSpecificDayStatus(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
