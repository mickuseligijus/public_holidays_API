using Holidays_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
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
        public string GetCountries()
        {
            string result = "";
            try
            {
                result = JsonSerializer.Serialize(_holidayService.getCountriesAsync().Result);
            }
            catch(HttpRequestException e) 
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}
