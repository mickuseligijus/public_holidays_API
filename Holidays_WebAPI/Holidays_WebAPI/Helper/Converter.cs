using Holidays_WebAPI.Models.DbModels;
using Holidays_WebAPI.Models.JsonModels;
using Name = Holidays_WebAPI.Models.DbModels.Name;

namespace Holidays_WebAPI.Helper
{
    public static class Converter
    {

        public static List<Country> ConvertCountry(List<CountryJson> countriesJson)
        {
            var countriesDb = new List<Country>();

            foreach (var country in countriesJson)
            {
                var regions = new List<Region>();
                foreach (var region in country.Regions)
                {
                    var tempRegion = new Region { RegionName = region };
                    regions.Add(tempRegion);
                }

                var dateTypes = new List<HolidayType>();
                foreach (var holidayType in country.HolidayTypes)
                {
                    var tempHolidayType = new HolidayType { HolidayTypeName = holidayType };
                    dateTypes.Add(tempHolidayType);
                }

                var tempCountry = new Country { CountryCode = country.CountryCode, FullName = country.FullName, DateTo = country.ToDate.ToString(), DateFrom = country.FromDate.ToString(), Regions = regions, HolidayTypes = dateTypes };

                countriesDb.Add(tempCountry);

            }

            return countriesDb;
        }

        public static List<Holiday> ConvertHoliday(List<HolidayJson> holidaysJason)
        {
            var holidays = new List<Holiday>();
            foreach (var holiday in holidaysJason)
            {
                var names = new List<Name>();
                foreach (var name in holiday.Name)
                {
                    names.Add(new Name { Lang = name.Lang, Text = name.Text });
                }
                var tempHoliday = new Holiday { Date = holiday.Date.ToString(), HolidayType = holiday.HolidayType, Names =names};
                holidays.Add(tempHoliday);
            }
            return holidays;
        }
    }
}
