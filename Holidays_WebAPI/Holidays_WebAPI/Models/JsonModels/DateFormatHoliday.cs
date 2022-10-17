
namespace Holidays_WebAPI.Models.JsonModels
{
    public class DateFormatHoliday : DateFormat

    {
        public string DayOfWeek { get; set; }

        public override string ToString()
        {
            return Day + "/" + Month + "/" + Year;
        }

    }
}
