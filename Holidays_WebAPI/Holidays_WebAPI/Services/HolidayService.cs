using Holidays_WebAPI.Models;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Holidays_WebAPI.Services
{

    public class HolidayService : IHolidayService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<List<string>> GetCountriesAsync()
        {
            var responseBody = await client.GetStringAsync("https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries");

            var countries = JsonConvert.DeserializeObject<List<Country>>(responseBody);

            var countriesList = countries.Select(c => c.FullName).ToList();

            return countriesList;
        }

        public async Task<List<IGrouping<string, Holiday>>> GetHolidaysForSpecificCountryAsync(string countryCode, string year)
        {

            var uri = $"https://kayaposoft.com/enrico/json/v2.0/?action=getHolidaysForYear&year={year}&country={countryCode}&holidayType=all";

            var responseBody = await client.GetStringAsync(uri);

            var holidays = JsonConvert.DeserializeObject<List<Holiday>>(responseBody);

            var groupedHolidays = holidays.GroupBy(h => h.date.Month).ToList();

            return groupedHolidays;
        }

        public int GetMaximumFreeDaysInRow(string countryName, int year)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetSpecificDayStatus(string date, string countryCode)
        {
            var uriWorkDay = $"https://kayaposoft.com/enrico/json/v2.0?action=isWorkDay&date={date}&country={countryCode}";

            var uriPublicHoliday = $"https://kayaposoft.com/enrico/json/v2.0/?action=isPublicHoliday&date={date}&country={countryCode}"; //holiday
            
            
                var responseBody = await client.GetStringAsync(uriWorkDay);

            var response = JsonConvert.DeserializeObject<IDictionary<string, bool>>(responseBody);

            if (response["isWorkDay"])
            {
                return "workday";
            }

            responseBody = await client.GetStringAsync(uriPublicHoliday);

            response = JsonConvert.DeserializeObject<IDictionary<string, bool>>(responseBody);

            if (response["isPublicHoliday"])
            {
                return "holiday";
            }

            return "free day";

        }
    }
}
