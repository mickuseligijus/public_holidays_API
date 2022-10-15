namespace Holidays_WebAPI.Services
{
    public interface IHolidayService
    {
        public Task<List<string>> getCountriesAsync();
        public List<string> getHolidaysForSpecificCountry(string countryName, int year);
        public string getSpecificDayStatus(DateTime date);
        public int getMaximumFreeDaysInRow(string countryName, int year);

    }
}
