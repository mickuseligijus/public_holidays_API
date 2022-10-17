using Holidays_WebAPI.Context;
using Holidays_WebAPI.Models.JsonModels;
using Holidays_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;


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
            List<CountryJson> countries = null;
            try
            {
                countries = await _holidayService.GetCountriesAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            var result = countries.Select(c => c.FullName).ToList();


            return result;
        }
        [HttpGet("{CountryJson}/{year}")]
        public async Task<List<IGrouping<string, HolidayJson>>> GetGroupedSpecificYearCountryHolidaysByMonth(string CountryJson, string year)
        {

            List<HolidayJson> holidays = null;
            try
            {
                holidays = await _holidayService.GetHolidaysForSpecificCountryAsync(CountryJson, year);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var result = holidays.GroupBy(h => h.Date.Month).ToList();
            return  result;

        }
        [HttpGet("dayType/{countryCode}/{date}")]
        public async Task<string> GetSpecificDayTypeAsync(string countryCode, string date)
        {
            string result = null;
            try
            {
                result = await _holidayService.GetSpecificDayStatusAsync(countryCode, date);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while retrieving data from client server " + e.Message);
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
            var x = _holidayService.GetMaximumFreeDaysInRow(countryCode, year).Result;
            return x;
        }
    }
}
