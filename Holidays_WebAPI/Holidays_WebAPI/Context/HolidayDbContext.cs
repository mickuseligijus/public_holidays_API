
using Holidays_WebAPI.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Holidays_WebAPI.Context
{
    public class HolidayDbContext : DbContext
    {
        public HolidayDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Country> Countries;
        public DbSet<Region> Regions;
        public DbSet<Name> Names;
        public DbSet<Holiday> Holidays;
    }
}
