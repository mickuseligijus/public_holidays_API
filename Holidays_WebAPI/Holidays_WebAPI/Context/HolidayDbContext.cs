
using Holidays_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Holidays_WebAPI.Context
{
    public class HolidayDbContext : DbContext
    {
        public HolidayDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
