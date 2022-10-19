using Holidays_WebAPI.Context;
using Holidays_WebAPI.Helper;
using Holidays_WebAPI.Models.DbModels;
using Holidays_WebAPI.Models.JsonModels;
using Holidays_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Name = Holidays_WebAPI.Models.DbModels.Name;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Holidays_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly HolidayDbContext _context;

        private IHolidayService _holidayService;
        public HolidayController(IHolidayService holidayService, HolidayDbContext context)
        {
            _holidayService = holidayService;
            _context = context;
        }

        // GET: api/<HolidayController>
        [HttpGet]
        public async Task<List<string>> GetCountries()
        {
            /*_context.Countries.Add(null);*/
            var countries = new List<CountryJson>();
            var countriesDb = new List<Country>();
            var result = new List<string>();
            try
            {
                if (_context.Countries.Any())
                {

                    countriesDb = _context.Countries.ToList();
                    result = countriesDb.Select(x => x.FullName).ToList();
                   
                }
                else
                {
                    countries = await _holidayService.GetCountriesAsync();
                    countriesDb = Converter.ConvertCountry(countries);

                    _context.Countries.AddRange(countriesDb);
                    _context.SaveChanges();

                }
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server, getSupportedCountries " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return result;
        }
        [HttpGet("{countryCode}/{year}")]
        public async Task<List<IGrouping<string, HolidayJson>>> GetGroupedSpecificYearCountryHolidaysByMonth(string countryCode, string year)
        {

            var holidaysJson = new List<HolidayJson>();
            var holidaysDb = new List<Holiday>();
            try
            {
                var holidayList = new List<Holiday>();
                if (_context.Holidays.Any())
                {
                    var holidays = _context.Holidays.ToList();

                    foreach (var h in holidays)
                    {
                        var index = h.Date.LastIndexOf("/");
                        var yearHoliday = h.Date.Substring(index + 1);
                        if(h.CountryCode.Equals(countryCode) && yearHoliday.Equals(year))
                        {
                            holidayList.Add(h);
                        }
                    }
                    foreach(var h in holidayList)
                    {
                        var names = new List<Name>();
                        foreach(var n in _context.Names)
                        {
                            if (h.HolidayId == n.HolidayId)
                            {
                                names.Add(new Name { Lang = n.Lang, Text = n.Text });
                            }
                        }
                        h.Names = names;
                    }
                    holidaysJson = Converter.ConvertHolidayJson(holidayList);

                }
                if(!holidayList.Any())
                {
                    holidaysJson = await _holidayService.GetHolidaysForSpecificCountryAsync(countryCode, year);
                    holidaysDb = Converter.ConvertHoliday(holidaysJson, countryCode);
                   
                    _context.Holidays.AddRange(holidaysDb);
                    _context.SaveChanges();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server, getHolidaysForYear " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            var result = holidaysJson.GroupBy(h => h.Date.Month).ToList();
            return  result;

        }
        [HttpGet("dayType/{countryCode}/{date}")]
        public async Task<string> GetSpecificDayTypeAsync(string countryCode, string date)
        {
            string result = "";
            try
            {
                result = await _holidayService.GetSpecificDayStatusAsync(countryCode, date);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server, isWorkDay, isPublicHoliday " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
        [HttpGet("maximumDays/{countryCode}/{year}")]
        public async Task<int> GetMaximumFreeDaysInRowAsync(string countryCode, string year)
        {
            int maxDayNumber = 0;
            try
            {
                if (_context.CountryMax.Where(c => c.Year.Equals(year) && c.CountryCode.Equals(countryCode)).Any())
                {
                    maxDayNumber = _context.CountryMax
                        .Where(c => c.Year.Equals(year) && c.CountryCode.Equals(countryCode))
                        .Select(c => c.MaxNumber)
                        .First();
                }
                else
                {
                    maxDayNumber = _holidayService.GetMaximumFreeDaysInRow(countryCode, year).Result;
                    _context.CountryMax.Add(new CountryMax { CountryCode=countryCode, Year=year, MaxNumber= maxDayNumber });
                    _context.SaveChanges();
                }
               
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server, isWorkday " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return maxDayNumber;
        }
    }
}
