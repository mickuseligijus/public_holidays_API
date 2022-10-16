using Holidays_WebAPI.Models;
using Holidays_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Holidays_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private IHolidayService _holidayService;
        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        // GET: api/<HolidayController>
        [HttpGet]
        public async Task<List<string>> GetCountries()
        {
            List<string> result = null;
            try
            {
                result = await _holidayService.GetCountriesAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while trying to connect to client server " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
        [HttpGet("{country}/{year}")]
        public async Task<List<IGrouping<string, Holiday>>> GetGroupedSpecificYearCountryHolidaysByMonth(string country, string year)
        {

            List<IGrouping<string, Holiday>> result = null;
            try
            {
                result = await _holidayService.GetHolidaysForSpecificCountryAsync(country, year);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while trying to connect to client server" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;

        }
        [HttpGet("dayType/{date}/{countryCode}")]
        public async Task<string> GetSpecificDayType (string date, string countryCode)
        {
            string result = null;
            try
            {
                result = await _holidayService.GetSpecificDayStatus(date, countryCode);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception while trying to connect to client server " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}
