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

        public async Task<string> GetSpecificDayStatusAsync(string date, string countryCode)
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

        public async Task<int> GetMaximumFreeDaysInRow(string countryCode, string year)
        {
         /*   DateTime x = new Date(); *//**/
            var holidays = GetHolidaysForSpecificCountryAsync(countryCode, year).Result;
            var freeDays = 0;
            foreach(var h in holidays)
            {
                foreach(var holiday in h)
                {

                    //If day is Friday
                    if (holiday.date.DayOfWeek.Equals("5"))
                    {

                        var tempFreeDays = 3;
                        var day = holiday.date.Day + "/" + holiday.date.Month + "/" + holiday.date.Year;

                        tempFreeDays = iterateThrougDays(day, -1, countryCode, tempFreeDays, false);
                        tempFreeDays = iterateThrougDays(day, 3, countryCode, tempFreeDays, true);

                        if(tempFreeDays > freeDays)
                        {
                            freeDays = tempFreeDays;
                        }

                    }
                    //If day is Monday
                    else if (holiday.date.DayOfWeek.Equals("1"))
                    {
                        var tempFreeDays = 3;
                        var day = holiday.date.Day + "/" + holiday.date.Month + "/" + holiday.date.Year;

                        tempFreeDays = iterateThrougDays(day, -3, countryCode, tempFreeDays, false);
                        tempFreeDays = iterateThrougDays(day, 1, countryCode, tempFreeDays, true);


                        if (tempFreeDays > freeDays)
                        {
                            freeDays = tempFreeDays;
                        }

                    }
                    //If day is Saturday
                    else if (holiday.date.DayOfWeek.Equals("6"))
                    {
                        var tempFreeDays = 2;
                        var day = holiday.date.Day + "/" + holiday.date.Month + "/" + holiday.date.Year;

                        tempFreeDays = iterateThrougDays(day, -1, countryCode, tempFreeDays, false);
                        tempFreeDays = iterateThrougDays(day, 2, countryCode, tempFreeDays, true);


                        if (tempFreeDays > freeDays)
                        {
                            freeDays = tempFreeDays;
                        }
                    }
                    //If day is Sunday
                    else if (holiday.date.DayOfWeek.Equals("7"))
                    {
                        var tempFreeDays = 2;
                        var day = holiday.date.Day + "/" + holiday.date.Month + "/" + holiday.date.Year;

                        tempFreeDays = iterateThrougDays(day, -2, countryCode, tempFreeDays, false);
                        tempFreeDays = iterateThrougDays(day, 1, countryCode, tempFreeDays, true);


                        if (tempFreeDays > freeDays)
                        {
                            freeDays = tempFreeDays;
                        }
                    }

                    //if day is Tuesday, Wednesday, Thursday
                    else
                    {
                        var tempFreeDays = 1;
                        var day = holiday.date.Day + "/" + holiday.date.Month + "/" + holiday.date.Year;

                        tempFreeDays = iterateThrougDays(day, -1, countryCode, tempFreeDays, false);
                        tempFreeDays = iterateThrougDays(day, 1, countryCode, tempFreeDays, true);


                        if (tempFreeDays > freeDays)
                        {
                            freeDays = tempFreeDays;
                        }
                    }

                }
            }

            
            async Task<bool> isWorkDay(string day, int step, string countryCode)
            {
                var parsedDate = DateTime.Parse(day);
                var dateToCheck = parsedDate.AddDays(step);
                var dateToCheckString = dateToCheck.ToString("dd-MM-yyyy");
                var uri = $"https://kayaposoft.com/enrico/json/v2.0?action=isWorkDay&date={dateToCheckString}=&country={countryCode}";
                var responseBody = await client.GetStringAsync(uri);
                var response = JsonConvert.DeserializeObject<IDictionary<string, bool>>(responseBody);

                return response["isWorkDay"];
            }
            int iterateThrougDays(string day, int step, string countryCode, int acc, bool increase)
            {

                var response = isWorkDay(day, step, countryCode).Result;
                while (!response)
                {
                    acc++;
                    if(increase)
                    {
                        step++;
                    }
                    else
                    {
                        step--;
                    }
                    response = isWorkDay(day, step, countryCode).Result;
                }
                return acc;
            }
            return freeDays;
        }
    }
}
