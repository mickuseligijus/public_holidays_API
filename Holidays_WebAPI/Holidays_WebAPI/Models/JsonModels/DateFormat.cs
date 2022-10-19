namespace Holidays_WebAPI.Models.JsonModels
{
    public class DateFormat
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public override string ToString()
        {
            if (Day.Length == 1)
            {
                Day = "0" + Day;
            }
            if (Month.Length == 1)
            {
                Month = "0" + Month;
            }
            return Day + "/" + Month + "/" + Year;
        }

    }
}
