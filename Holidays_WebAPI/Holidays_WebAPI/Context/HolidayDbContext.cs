
using Holidays_WebAPI.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Holidays_WebAPI.Context
{
    public class HolidayDbContext : DbContext
    {
        public HolidayDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<HolidayType> HolidayTypes { get; set; }
        public DbSet<CountryMax> CountryMax { get; set; }
    }
}
