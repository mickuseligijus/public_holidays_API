namespace Holidays_WebAPI.Models.JsonModels
{
    public class DateFormat
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public override string ToString()
        {
            return Day + "/" + Month + "/" + Year;
        }

    }
}
