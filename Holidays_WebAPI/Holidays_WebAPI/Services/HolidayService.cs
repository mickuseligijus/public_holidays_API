using Holidays_WebAPI.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace Holidays_WebAPI.Services
{

    public class HolidayService : IHolidayService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<List<string>> getCountriesAsync()
        {
            string responseBody = await client.GetStringAsync("https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries");

            var countries = JsonConvert.DeserializeObject<List<Country>>(responseBody);

            var countriesList = new List<string>();

            foreach(Country country in countries)
            {
                countriesList.Add(country.FullName);
            }
            return countriesList;
        }

        public List<string> getHolidaysForSpecificCountry(string countryName, int year)
        {
            throw new NotImplementedException();
        }

        public int getMaximumFreeDaysInRow(string countryName, int year)
        {
            throw new NotImplementedException();
        }

        public string getSpecificDayStatus(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
