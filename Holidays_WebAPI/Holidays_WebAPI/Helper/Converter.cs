using Holidays_WebAPI.Models.DbModels;
using Holidays_WebAPI.Models.JsonModels;
using Name = Holidays_WebAPI.Models.DbModels.Name;
using NameStruct = Holidays_WebAPI.Models.JsonModels.Name;

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
                if(country.Regions != null)
                {
                    foreach (var region in country.Regions)
                    {
                        var tempRegion = new Region { RegionName = region };
                        regions.Add(tempRegion);
                    }
                }


                var dateTypes = new List<HolidayType>();
                if(country.HolidayTypes != null)
                {
                    foreach (var holidayType in country.HolidayTypes)
                    {
                        var tempHolidayType = new HolidayType { HolidayTypeName = holidayType };
                        dateTypes.Add(tempHolidayType);
                    }
                }


                var tempCountry = new Country { CountryCode = country.CountryCode, FullName = country.FullName, DateTo = country.ToDate.ToString(), DateFrom = country.FromDate.ToString(), Regions = regions, HolidayTypes = dateTypes };

                countriesDb.Add(tempCountry);

            }

            return countriesDb;
        }

        public static List<CountryJson> ConvertCountryJson(List<Country> countries)
        {
            var countriesJson = new List<CountryJson>();

            foreach(var country in countries)
            {
                var regions = new List<string>();
                if(country.Regions != null)
                {
                    foreach (var region in country.Regions)
                    {
                        regions.Add(region.RegionName);
                    }
                }
                
                var holidayTypes = new List<string>();
                if (country.HolidayTypes != null)
                {
                    foreach (var holidayType in country.HolidayTypes)
                    {
                        holidayTypes.Add(holidayType.HolidayTypeName);
                    }

                }

                var tempCountry = new CountryJson { CountryCode = country.CountryCode, FullName = country.FullName, Regions = regions, HolidayTypes = holidayTypes, FromDate=ConvertDate(country.DateFrom),ToDate= ConvertDate(country.DateTo) };

                countriesJson.Add(tempCountry);
            }

            return countriesJson;

        }
        
        public static List<Holiday> ConvertHoliday(List<HolidayJson> holidaysJason, string countryCode)
        {
            var holidays = new List<Holiday>();
            foreach (var holiday in holidaysJason)
            {
                var names = new List<Name>();
                foreach (var name in holiday.Name)
                {
                    names.Add(new Name { Lang = name.Lang, Text = name.Text });
                }
                var tempHoliday = new Holiday { Date = holiday.Date.ToString(), DayOfWeek = holiday.Date.DayOfWeek, HolidayType = holiday.HolidayType, Names =names, CountryCode = countryCode};
                holidays.Add(tempHoliday);
            }
            return holidays;
        }

        public static List<HolidayJson> ConvertHolidayJson(List<Holiday> holidays)
        {
            var holidaysJson = new List<HolidayJson>();
            foreach(var holiday in holidays)
            {

                var holidayNames= new List<NameStruct>();
                foreach (var holidayName in holiday.Names)
                {
                    holidayNames.Add(new NameStruct { Lang = holidayName.Lang, Text = holidayName.Text });
                }
                var tempHoliday = new HolidayJson { Date = ConvertDateHoliday(holiday.Date, holiday.DayOfWeek), HolidayType = holiday.HolidayType, Name = holidayNames };
                holidaysJson.Add(tempHoliday);
            }
            return holidaysJson;
        }

        public static DateFormat ConvertDate(string date)
        {
            var indexSeparator1 = date.IndexOf("/");
            var day = date.Substring(0, indexSeparator1);
            date = date.Substring(indexSeparator1 + 1);
            var indexSeparator2 = date.IndexOf("/");
            var month = date.Substring(0, indexSeparator2);
            var year = date.Substring(indexSeparator2 + 1);

            return new DateFormat { Day=day, Month=month, Year=year };
        }

        public static DateFormatHoliday ConvertDateHoliday(string date, string dayOfWeek)
        {
            var indexSeparator1 = date.IndexOf("/");
            var day = date.Substring(0, indexSeparator1);
            date = date.Substring(indexSeparator1 + 1);
            var indexSeparator2 = date.IndexOf("/");
            var month = date.Substring(0, indexSeparator2);
            var year = date.Substring(indexSeparator2 + 1);

            return new DateFormatHoliday { Day = day, Month = month, Year = year, DayOfWeek = dayOfWeek };
        }

    }
}
